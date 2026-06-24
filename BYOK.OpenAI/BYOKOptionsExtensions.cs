using BYOK.Core;

namespace BYOK.OpenAI;

public static class BYOKOptionsExtensions
{
    #region OpenAi compatible generic provider registration
    public static void UseOpenAICompatible(this BYOKOptions options,
        Func<string> apiKeyFactory,
        Action<OpenAICompatibleOptions> configure,
        string providerName)
    {
        options.UseProvider(providerName, () => new OpenAICompatibleClient(providerName, apiKeyFactory, configure));
    }

    public static void UseOpenAICompatible(this BYOKOptions options,
        string apiKey,
        Action<OpenAICompatibleOptions> configure,
        string providerName)
    {
        options.UseOpenAICompatible(() => apiKey, configure, providerName);
    }
    #endregion

    #region OpenAI
    public static void UseOpenAI(this BYOKOptions options,
        Func<string> apiKeyFactory,
        Action<OpenAICompatibleOptions>? configure = null,
        string providerName = "openai")
    {
       options.UseProvider(providerName, () => new OpenAICompatibleClient(providerName, apiKeyFactory, config =>
        {
            config.BaseUrl = "https://api.openai.com/v1";
            configure?.Invoke(config);
        }));
    }

    public static void UseOpenAI(this BYOKOptions options,
        string apiKey,
        Action<OpenAICompatibleOptions>? configure = null,
        string providerName = "openai")
        => UseOpenAI(options, () => apiKey, configure, providerName);
    #endregion

    #region Groq
    public static void UseGroq(this BYOKOptions options, 
        Func<string> apiKeyFactory, 
        Action<OpenAICompatibleOptions>? configure = null,
        string providerName = "groq")
    {
        options.UseProvider(providerName, () => new OpenAICompatibleClient(providerName, apiKeyFactory, config =>
        {
            config.BaseUrl = "https://api.groq.com/openai/v1";
            configure?.Invoke(config);
        }));
    }

    public static void UseGroq(this BYOKOptions options, 
        string apiKey, 
        Action<OpenAICompatibleOptions>? configure = null,
        string providerName = "groq")
        => UseGroq(options, () => apiKey, configure, providerName);

    #endregion

    #region OpenRouter
    public static void UseOpenRouter(
        this BYOKOptions options, 
        Func<string> apiKeyFactory, 
        Action<OpenAICompatibleOptions>? configure = null,
        string? referer = null,
        string? title = null,
        string providerName = "openrouter")
    {
        options.UseProvider(providerName, new OpenAICompatibleClient(providerName, apiKeyFactory, config =>
        {
            config.BaseUrl = "https://openrouter.ai/api/v1";

            if(!string.IsNullOrWhiteSpace(referer))
                config.DefaultRequestHeaders["HTTP-Referer"] = referer;

            if (!string.IsNullOrWhiteSpace(title))
                config.DefaultRequestHeaders["X-Title"] = title;

            configure?.Invoke(config);
        }));
    }

    public static void UseOpenRouter(
        this BYOKOptions options,
        string apiKey,
        Action<OpenAICompatibleOptions>? configure = null,
        string? referer = null,
        string? title = null,
        string providerName = "openrouter")
        => UseOpenRouter(options, () => apiKey, configure, referer, title, providerName);

    #endregion

    #region Deepseek
    public static void UseDeepSeek(
        this BYOKOptions options, 
        Func<string> apiKeyFactory, 
        Action<OpenAICompatibleOptions>? configure = null,
        string providerName = "deepseek")
    {
        options.UseProvider(providerName, new OpenAICompatibleClient(providerName, apiKeyFactory, config =>
        {
            config.BaseUrl = "https://api.deepseek.com/v1";
            configure?.Invoke(config);
        }));
    }

    public static void UseDeepSeek(this BYOKOptions options, 
    string apiKey, 
    Action<OpenAICompatibleOptions>? configure = null, 
    string providerName = "deepseek")
        => options.UseDeepSeek(() => apiKey, configure, providerName);
    #endregion

    #region Ollama
    public static void UseOllama(this BYOKOptions options, 
        Action<OpenAICompatibleOptions>? configure = null,
        string providerName = "ollama")
    {
        options.UseProvider(providerName, new OpenAICompatibleClient(providerName, () => "ollamaDummyKey", config =>
        {
            config.BaseUrl = "http://localhost:11434/v1";
            configure?.Invoke(config);
        }));
    }
    #endregion

}