using Anthropic;
using BYOK.Core;
using BYOK.Core.Utils;
using Microsoft.Extensions.AI;

namespace BYOK.Anthropic;

public sealed class AnthropicChatClient : IChatClient
{
    private readonly string _providerName;
    private readonly Func<string> _apiKeyProvider;
    private readonly AnthropicOptions _options;
    private readonly Lock _lock = new();
    private string? _lastApiKey;
    private IChatClient? _client;

    public AnthropicChatClient(
        string providerName,
        Func<string> apiKeyProvider,
        Action<AnthropicOptions>? configure = null)
    {
        _providerName = Throw.IfNullOrWhiteSpace(providerName);
        _apiKeyProvider = Throw.IfNull(apiKeyProvider);

        _options = new AnthropicOptions();
        configure?.Invoke(_options);
    }

    private IChatClient GetOrCreateClient(ChatOptions? chatOptions)
    {
        string modelId = BYOKGuard.IfModelIdMissing(chatOptions?.ModelId);
        var currentKey = BYOKGuard.IfApiKeyMissing(_apiKeyProvider());

        if (_client is null || _lastApiKey != currentKey)
        {
            lock (_lock)
            {
                if (_client is null || _lastApiKey != currentKey)
                {
                    (_client as IDisposable)?.Dispose();

                    _client = CreateInnerClient(currentKey);
                    _lastApiKey = currentKey;
                }
            }
        }

        return _client.WithMetadata(
            _providerName,
            modelId,
            _options.BaseUrl is not null ? new Uri(_options.BaseUrl) : null);
    }
    
    private IChatClient CreateInnerClient(string apiKey)
    {
        var nativeClient = new AnthropicClient 
        { 
            ApiKey = apiKey,
            BaseUrl = _options.BaseUrl ?? "https://api.anthropic.com"
        };
            
        _options.ConfigureNative?.Invoke(nativeClient);

        var builder = new ChatClientBuilder(
            nativeClient.AsIChatClient());

        builder.UseFunctionInvocation(
            configure: _options.ConfigureFunctionInvocation);

        return builder.Build();
    }

    #region IChatClient
    public Task<ChatResponse> GetResponseAsync(
        IEnumerable<ChatMessage> chatMessages,
        ChatOptions? options = null,
        CancellationToken cancellationToken = default)
        => GetOrCreateClient(options)
            .GetResponseAsync(chatMessages, options, cancellationToken);

    public IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponseAsync(
        IEnumerable<ChatMessage> chatMessages,
        ChatOptions? options = null,
        CancellationToken cancellationToken = default)
        => GetOrCreateClient(options)
            .GetStreamingResponseAsync(chatMessages, options, cancellationToken);

    public object? GetService(System.Type serviceType, object? serviceKey = null)
        => serviceType.IsInstanceOfType(this) ? this : null;

    public void Dispose()
    {
        if (_client is IDisposable dc)
            dc.Dispose();
    }
    #endregion
}