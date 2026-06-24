using System.Collections.Concurrent;
using Microsoft.Extensions.AI;
using BYOK.Core.Exceptions;
using BYOK.Core.Routing;
using BYOK.Core.Utils;
using System.Runtime.CompilerServices;

namespace BYOK.Core;

/// <summary>Routes AI requests to registered providers with automatic fallback support.</summary>
public sealed class BYOKClient : IChatClient
{
    private readonly BYOKOptions _options;
    private readonly RoutingEngine _routingEngine;
    private readonly ConcurrentDictionary<string, IChatClient> _resolvedClients = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>Creates a new BYOKClient with the given options.</summary>
    public BYOKClient(BYOKOptions options)
    {
        _options = Throw.IfNull(options);
        _routingEngine = new RoutingEngine(options.Routing.Configuration);
    }

    /// <summary>Creates a new BYOKClient using a configure delegate.</summary>
    public static BYOKClient Create(Action<BYOKOptions> configure)
    {
        var options = new BYOKOptions();
        configure(options);
        return new BYOKClient(options);
    }

    private string GetRequestedPath(ChatOptions? options)
    {
        return options?.ModelId
            ?? throw new BYOKConfigurationException("ModelId is required in ChatOptions for BYOK routing.");
    }

    private (IChatClient Client, ChatOptions Options) PrepareClientCall(string path, ChatOptions? originalOptions)
    {
        var route = Route.Parse(path);

        if (!_options.ProviderFactories.TryGetValue(route.ProviderName, out var factory))
            throw new BYOKConfigurationException($"Provider '{route.ProviderName}' is not registered in BYOK.");

        var client = _resolvedClients.GetOrAdd(route.ProviderName, _ => factory());
        
        // Clone original options (tools, temperature, etc.) but override the ModelId
        var providerOptions = originalOptions?.Clone() ?? new ChatOptions();
        providerOptions.ModelId = route.ModelId;

        return (client, providerOptions);
    }

    #region IChatClient
    /// <summary>Sends a chat request with automatic fallback across registered providers.</summary>
    public async Task<ChatResponse> GetResponseAsync(
        IEnumerable<ChatMessage> chatMessages, 
        ChatOptions? options = null, 
        CancellationToken cancellationToken = default)
    {
        using var timeoutCts = _options.DefaultTimeout is { } timeout
            ? CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken, new CancellationTokenSource(timeout).Token)
            : null;
        var effectiveCt = timeoutCts?.Token ?? cancellationToken;

        // --- Per-request timing (enable by uncommenting the using above) ---
        // var byokSw = Stopwatch.StartNew();

        string requestedPath = GetRequestedPath(options);
        var plan = _routingEngine.GetExecutionPlan(requestedPath);
        var exceptions = new List<Exception>();

        var messagesList = chatMessages.ToList();

        foreach (var path in plan)
        {
            try
            {
                var (client, providerOptions) = PrepareClientCall(path, options);

                // byokSw.Stop();
                // var overhead = byokSw.Elapsed.TotalMilliseconds;
                // var apiSw = Stopwatch.StartNew();

                var result = await client.GetResponseAsync(messagesList, providerOptions, effectiveCt);

                // apiSw.Stop();
                // Debug.WriteLine($"[BYOK] Overhead: {overhead:F1}ms | API: {apiSw.Elapsed.TotalMilliseconds:F1}ms | Route: {path}");
                
                return result;
            }
            catch (OperationCanceledException)
            {
                throw; // User-initiated cancellation should not trigger fallbacks
            }
            catch (Exception ex) when (ex is not BYOKRoutingException and not BYOKConfigurationException)
            {
                exceptions.Add(new BYOKRoutingException($"Provider '{path}' failed: {ex.Message}", ex));
            }
        }

        var lastError = exceptions.Count > 0 ? exceptions[^1].Message : "Unknown error";
        throw new BYOKRoutingException(
            $"All routes failed for the requested model '{requestedPath}'. Last error: {lastError}",
            new AggregateException(exceptions));
    }

    /// <summary>Sends a streaming chat request with automatic fallback across registered providers.</summary>
    public async IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponseAsync(
        IEnumerable<ChatMessage> chatMessages, 
        ChatOptions? options = null, 
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var timeoutCts = _options.DefaultTimeout is { } timeout
            ? CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken, new CancellationTokenSource(timeout).Token)
            : null;
        var effectiveCt = timeoutCts?.Token ?? cancellationToken;

        string requestedPath = GetRequestedPath(options);
        var plan = _routingEngine.GetExecutionPlan(requestedPath);
        var exceptions = new List<Exception>();

        foreach (var path in plan)
        {
            IAsyncEnumerable<ChatResponseUpdate> stream;
            
            try
            {
                var (client, providerOptions) = PrepareClientCall(path, options);
                stream = client.GetStreamingResponseAsync(chatMessages.ToList(), providerOptions, effectiveCt);
            }
            catch(OperationCanceledException)
            {
                throw; // User-initiated cancellation should not trigger fallbacks
            }
            catch (Exception ex) when (ex is not BYOKRoutingException and not BYOKConfigurationException)
            {
                exceptions.Add(new BYOKRoutingException($"Provider '{path}' failed: {ex.Message}", ex));
                continue; // Failed to create/start the stream
            }

            // Consume the stream. If it fails mid-way, MEAI usually doesn't allow clean fallbacks,
            // but if it fails on the first chunk, the catch will trigger and skip to the next provider.
            bool streamStarted = false;
            await using var enumerator = stream.GetAsyncEnumerator(cancellationToken);

            while (true)
            {
                try
                {
                    if (!await enumerator.MoveNextAsync()) break;
                }
                catch(OperationCanceledException)
                {
                    throw; // User-initiated cancellation should not trigger fallbacks
                }
                catch (Exception ex)
                {
                    if (ex is OperationCanceledException) throw;

                    exceptions.Add(new BYOKRoutingException($"Provider '{path}' failed mid-stream: {ex.Message}", ex));
                    if (streamStarted) throw; // If we already sent data to the user, we cannot silently fallback
                    break; // Exit while to try the next provider
                }

                streamStarted = true;
                yield return enumerator.Current;
            }

            if (streamStarted) yield break; 
        }

        throw new BYOKRoutingException(
            $"All routes failed for the requested model '{requestedPath}' (Streaming).", 
            new AggregateException(exceptions));
    }

    /// <summary>Resolves a service type, returning the client itself if the type matches.</summary>
    public object? GetService(Type serviceType, object? serviceKey = null)
        => serviceType.IsInstanceOfType(this) ? this : null;
    
    /// <summary>Disposes all cached provider clients.</summary>
    public void Dispose()
    {
        foreach (var client in _resolvedClients.Values)
        {
            if (client is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
        _resolvedClients.Clear();
    }
    #endregion
}