using Microsoft.Extensions.DependencyInjection;
using pos.core.Services;
using pos.infrastructure.Storages;

namespace pos.infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IStorageService, LocalStorage>()
            .AddScoped<IPathResolver, PathResolver>()
            ;
    }

}
