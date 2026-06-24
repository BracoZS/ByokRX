using Anthropic;
using Microsoft.Extensions.AI;

namespace BYOK.Anthropic;

public sealed class AnthropicOptions
{
    public string? BaseUrl { get; set; }
    public Dictionary<string, string> DefaultRequestHeaders { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Action<FunctionInvokingChatClient>? ConfigureFunctionInvocation { get; set; }

    /// <summary>
    /// Optional delegate to configure the native <see cref="AnthropicClient"/> after creation.
    /// Allows setting <see cref="AnthropicClient.MaxRetries"/>, <see cref="AnthropicClient.Timeout"/>,
    /// <see cref="AnthropicClient.ExtraHeaders"/>, <see cref="AnthropicClient.HttpClient"/>, etc.
    /// </summary>
    public Action<AnthropicClient>? ConfigureNative { get; set; }
}