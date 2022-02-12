using Microsoft.Extensions.DependencyInjection;
using pos.core.Data;

namespace pos.common.testing
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTestingDatabase(this IServiceCollection services)
        {
            return services
                .AddScoped<ITenantDbContextFactory, InMemoryTenantDbContextFactory>()
                ;
        }
    }
}
