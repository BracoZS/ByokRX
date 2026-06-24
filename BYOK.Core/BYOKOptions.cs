using BYOK.Core.Routing;
using Microsoft.Extensions.AI;

namespace BYOK.Core;

/// <summary>Options for configuring BYOK client routing and providers.</summary>
public class BYOKOptions
{
    /// <summary>Gets the route builder for defining model aliases and fallbacks.</summary>
    public RouteBuilder Routing { get; } = new();
    /// <summary>Gets or sets the default timeout for all requests.</summary>
    public TimeSpan? DefaultTimeout { get; set; }

    internal Dictionary<string, Func<IChatClient>> ProviderFactories { get; } = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Registers a provider with a factory for deferred client creation.
    /// The factory is invoked the first time a route matching <paramref name="providerName"/> is resolved.
    /// </summary>
    /// <param name="providerName">The provider key used in route paths (e.g. "openai", "groq").</param>
    /// <param name="clientFactory">A delegate that creates the <see cref="IChatClient"/> instance.</param>
    public void UseProvider(string providerName, Func<IChatClient> clientFactory) 
        => ProviderFactories[providerName] = clientFactory;

    /// <summary>
    /// Registers a provider with an already-instantiated client.
    /// </summary>
    /// <param name="providerName">The provider key used in route paths (e.g. "openai", "groq").</param>
    /// <param name="client">The <see cref="IChatClient"/> instance to use for this provider.</param>
    public void UseProvider(string providerName, IChatClient client)
        => UseProvider(providerName, () => client);

}