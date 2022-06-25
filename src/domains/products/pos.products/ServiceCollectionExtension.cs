using MediatR;
using Microsoft.Extensions.DependencyInjection;
using pos.products.Services;

namespace pos.products
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddProductServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtension));

            return services
                .AddScoped<IProductService, ProductService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IProductFakeService, ProductFakeService>()
                .AddScoped<IInventoryService, InventoryService>()
                ;
        }
    }
}
