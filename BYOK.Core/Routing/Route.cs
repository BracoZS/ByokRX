using BYOK.Core.Exceptions;

namespace BYOK.Core.Routing;

internal record Route(string ProviderName, string ModelId)
{
    private const char SEPARATOR = ':';

    public static Route Parse(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new BYOKRoutingException("Model route cannot be empty.");

        var parts = path.Split(SEPARATOR, 2);
        if (parts.Length != 2)
            throw new BYOKRoutingException($"Invalid route format: '{path}'. Expected format 'providername{SEPARATOR}model'.");

        return new Route(parts[0].ToLowerInvariant(), parts[1]);
    }
}