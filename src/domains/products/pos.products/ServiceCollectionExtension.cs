using Microsoft.Extensions.DependencyInjection;
using pos.products.Services;

namespace pos.products
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddProductServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IProductService, ProductService>()
                ;
        }
    }
}
