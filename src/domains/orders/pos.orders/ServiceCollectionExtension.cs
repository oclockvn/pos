using Microsoft.Extensions.DependencyInjection;
using pos.orders.Services;

namespace pos.orders
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddOrderServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IOrderService, OrderService>()
                ;
        }
    }
}
