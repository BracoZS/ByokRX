namespace BYOK.Core.Routing;

/// <summary>Fluent API for configuring model routing, aliases, and fallbacks.</summary>
public class RouteBuilder
{
    internal RouteConfiguration Configuration { get; } = new();

    /// <summary>Starts a fallback chain for a specific model path (e.g. "openai:gpt-4o").</summary>
    public FallbackBuilder ForModelId(string modelPath)
    {
        if (!Configuration.Fallbacks.ContainsKey(modelPath))
        {
            Configuration.Fallbacks[modelPath] = new List<string>();
        }
        return new FallbackBuilder(Configuration.Fallbacks[modelPath]);
    }

    /// <summary>Creates an alias that points to a real model path.</summary>
    public AliasBuilder ForAlias(string alias)
    {
        return new AliasBuilder(Configuration, alias);
    }
}

/// <summary>Fluent builder for adding fallback routes.</summary>
public class FallbackBuilder
{
    private readonly List<string> _fallbacks;

    internal FallbackBuilder(List<string> fallbacks) => _fallbacks = fallbacks;

    /// <summary>Adds a fallback model path to try if the primary fails.</summary>
    public FallbackBuilder FallbackTo(string modelPath)
    {
        _fallbacks.Add(modelPath);
        return this;
    }
}

/// <summary>Fluent builder for defining alias-to-model mappings.</summary>
public class AliasBuilder
{
    private readonly RouteConfiguration _config;
    private readonly string _alias;

    internal AliasBuilder(RouteConfiguration config, string alias)
    {
        _config = config;
        _alias = alias;
    }

    /// <summary>Maps the alias to a target model path.</summary>
    public FallbackBuilder Try(string modelPath)
    {
        _config.Aliases[_alias] = modelPath;
        if (!_config.Fallbacks.ContainsKey(_alias))
        {
            _config.Fallbacks[_alias] = new List<string>();
        }
        return new FallbackBuilder(_config.Fallbacks[_alias]);
    }
}