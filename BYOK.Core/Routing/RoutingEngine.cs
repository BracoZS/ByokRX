namespace BYOK.Core.Routing;

internal class RoutingEngine
{
    private readonly RouteConfiguration _config;

    public RoutingEngine(RouteConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Returns a sequence of routes to try (Primary -> Fallback 1 -> Fallback N)
    /// </summary>
    public IEnumerable<string> GetExecutionPlan(string requestedPath)
    {
        string primaryPath = _config.Aliases.TryGetValue(requestedPath, out var aliasTarget)
            ? aliasTarget
            : requestedPath;

        yield return primaryPath;

        if(_config.Fallbacks.TryGetValue(requestedPath, out var directFallbacks))
        {
            foreach(var fallback in directFallbacks) yield return fallback;
        }
        else if(primaryPath != requestedPath && _config.Fallbacks.TryGetValue(primaryPath, out var primaryFallbacks))
        {
            foreach(var fallback in primaryFallbacks) yield return fallback;
        }
    }
}