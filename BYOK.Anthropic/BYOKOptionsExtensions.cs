using BYOK.Core;

namespace BYOK.Anthropic;

public static class BYOKOptionsExtensions
{
    #region Anthropic-compatible generic provider registration
    public static void UseAnthropicCompatible(
        this BYOKOptions options,
        Func<string> apiKeyFactory,
        Action<AnthropicOptions> configure,
        string providerName)
    {
        options.UseProvider(providerName, () => new AnthropicChatClient(providerName, apiKeyFactory, configure));
    }

    public static void UseAnthropicCompatible(
        this BYOKOptions options,
        string apiKey,
        Action<AnthropicOptions> configure,
        string providerName)
    {
        options.UseAnthropicCompatible(() => apiKey, configure, providerName);
    }
    #endregion

    #region Anthropic
    public static void UseAnthropic(
        this BYOKOptions options,
        Func<string> apiKeyProvider,
        Action<AnthropicOptions>? configure = null,
        string providerName = "anthropic")
    {
        options.UseProvider(providerName, () => new AnthropicChatClient(providerName, apiKeyProvider, configure));
    }

    public static void UseAnthropic(
        this BYOKOptions options,
        string apiKey,
        Action<AnthropicOptions>? configure = null,
        string providerName = "anthropic")
    {
        options.UseAnthropic(() => apiKey, configure, providerName);
    }
    #endregion

    #region Ollama
    public static void UseOllamaAnthropic(
        this BYOKOptions options,
        Action<AnthropicOptions>? configure = null,
        string providerName = "ollama-anthropic")
    {
        options.UseProvider(providerName, () => new AnthropicChatClient(providerName, () => "ollamaDummyKey", config =>
        {
            config.BaseUrl = "http://localhost:11434";
            configure?.Invoke(config);
        }));
    }
    #endregion

    #region DeepSeek
    public static void UseDeepSeekAnthropic(
        this BYOKOptions options,
        Func<string> apiKeyProvider,
        Action<AnthropicOptions>? configure = null,
        string providerName = "deepseek-anthropic")
    {
        options.UseProvider(providerName, () => new AnthropicChatClient(providerName, apiKeyProvider, config =>
        {
            config.BaseUrl = "https://api.deepseek.com/anthropic";
            configure?.Invoke(config);
        }));
    }

    public static void UseDeepSeekAnthropic(
        this BYOKOptions options,
        string apiKey,
        Action<AnthropicOptions>? configure = null,
        string providerName = "deepseek-anthropic")
    {
        options.UseDeepSeekAnthropic(() => apiKey, configure, providerName);
    }
    #endregion
}