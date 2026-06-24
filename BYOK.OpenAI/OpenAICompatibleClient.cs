using BYOK.Core;
using BYOK.Core.Exceptions;
using BYOK.Core.Utils;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;
using System.ClientModel.Primitives;
using System.Diagnostics;

namespace BYOK.OpenAI;

public sealed class OpenAICompatibleClient : IChatClient
{
    private readonly string _providerName;
    private readonly Func<string> _apiKeyProvider;
    private readonly OpenAICompatibleOptions _options;
    private readonly Lock _lock = new();
    private string? _lastApiKey;
    private IChatClient? _client;

    public OpenAICompatibleClient(
        string providerName,
        Func<string> apiKeyProvider,
        Action<OpenAICompatibleOptions>? configure = null)
    {
        _providerName = Throw.IfNullOrWhiteSpace(providerName);
        _apiKeyProvider = Throw.IfNull(apiKeyProvider);

        _options = new OpenAICompatibleOptions();
        configure?.Invoke(_options);

        if (_options.NativeOptions.Endpoint is null)
        {
            throw new BYOKConfigurationException("The Endpoint (BaseUrl) must be configured for an OpenAI-compatible provider.");
        }

        if (_options.DefaultRequestHeaders.Any())
        {
            _options.NativeOptions.AddPolicy(new AddHeadersPolicy(_options.DefaultRequestHeaders), PipelinePosition.PerCall);
        }

        // Debug.WriteLine($"[BYOK] Created {nameof(OpenAICompatibleClient)} for provider '{_providerName}'");
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

        return _client.WithMetadata(    // superlight
            _providerName,
            modelId,
            _options.NativeOptions.Endpoint);
    }

    private IChatClient CreateInnerClient(string apiKey)
    {
        // var stopwatch = Stopwatch.StartNew();

        var openAiClient = new OpenAIClient(new ApiKeyCredential(apiKey), _options.NativeOptions);
        
        var builder = new ChatClientBuilder(openAiClient
            .GetChatClient("default")
            .AsIChatClient());

        builder.UseFunctionInvocation(  //acepta nulls
            configure: _options.ConfigureFunctionInvocation);

        var client = builder.Build();

        // Console.WriteLine($"[BYOK] Pipeline built for provider '{_providerName}': {stopwatch.Elapsed}");

        return client;
    }

    #region IChatClient
    public Task<ChatResponse> GetResponseAsync(
        IEnumerable<ChatMessage> chatMessages,
        ChatOptions? chatOptions = null,
        CancellationToken cancellationToken = default)
        => GetOrCreateClient(chatOptions)
            .GetResponseAsync(chatMessages, chatOptions, cancellationToken);

    public IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponseAsync(
        IEnumerable<ChatMessage> chatMessages,
        ChatOptions? chatOptions = null,
        CancellationToken cancellationToken = default)
        => GetOrCreateClient(chatOptions)
            .GetStreamingResponseAsync(chatMessages, chatOptions, cancellationToken);

    public object? GetService(Type serviceType, object? serviceKey = null)
        => serviceType.IsInstanceOfType(this) ? this : null;

    public void Dispose()
    {
        if (_client is IDisposable dc)
            dc.Dispose();
    }
    #endregion

    private sealed class AddHeadersPolicy : PipelinePolicy
    {
        private readonly Dictionary<string, string> _headers;

        public AddHeadersPolicy(Dictionary<string, string> headers)
        {
            _headers = headers;
        }

        public override void Process(PipelineMessage message, IReadOnlyList<PipelinePolicy> pipeline, int currentIndex)
        {
            InjectHeaders(message);
            ProcessNext(message, pipeline, currentIndex);
        }

        public override async ValueTask ProcessAsync(PipelineMessage message, IReadOnlyList<PipelinePolicy> pipeline, int currentIndex)
        {
            InjectHeaders(message);
            await ProcessNextAsync(message, pipeline, currentIndex).ConfigureAwait(false);
        }

        private void InjectHeaders(PipelineMessage message)
        {
            foreach (var header in _headers)
            {
                message.Request.Headers.Set(header.Key, header.Value);
            }
        }
    }
}
