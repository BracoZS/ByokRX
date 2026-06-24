using Microsoft.Extensions.AI;
using OpenAI;

namespace BYOK.OpenAI;

/// <summary>
/// Provides configuration options for clients using the OpenAI-compatible API.
/// </summary>
public sealed class OpenAICompatibleOptions
{
    /// <summary>
    /// Gets or sets the base URL for the OpenAI-compatible endpoint.
    /// </summary>
    public string? BaseUrl
    {
        get => NativeOptions.Endpoint?.AbsoluteUri;
        set => NativeOptions.Endpoint = string.IsNullOrWhiteSpace(value) ? null : new Uri(value);
    }

    /// <summary>
    /// Dictionary of HTTP headers to inject into every request made to the provider.
    /// </summary>
    public Dictionary<string, string> DefaultRequestHeaders { get; } = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Optional delegate to configure <see cref="FunctionInvokingChatClient"/>.
    /// Allows setting MaximumIterationsPerRequest, AllowConcurrentInvocation, etc.
    /// The middleware is always present but only activates when tools are sent in the request.
    /// </summary>
    public Action<FunctionInvokingChatClient>? ConfigureFunctionInvocation { get; set; }    
    
    /// <summary>
    /// Gets the native OpenAI SDK options (Endpoint, Timeouts, etc.).
    /// </summary>
    /// <remarks>
    /// Use <see cref="NativeOptions"/> to configure retries and transport-specific timeouts.
    /// </remarks>
    public OpenAIClientOptions NativeOptions { get; } = new();
}

public static class OpenAICompatibleOptionsExtensions
{
    public static void AddHeader(this OpenAICompatibleOptions options, string key, string value)
    {
        options.DefaultRequestHeaders[key] = value;
    }
}