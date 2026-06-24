using Microsoft.Extensions.AI;
using BYOK.Core;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Extension methods for registering BYOK with dependency injection.</summary>
public static class ServiceCollectionExtensions
{
    /// <summary>Registers BYOKClient as a singleton IChatClient with the given configuration.</summary>
    public static IServiceCollection AddBYOKClient(this IServiceCollection services, Action<BYOKOptions> configure)
    {
        var options = new BYOKOptions();
        configure(options);
        
        services.AddSingleton(options);
        services.AddSingleton<IChatClient>(sp => new BYOKClient(options));
        
        return services;
    }
}