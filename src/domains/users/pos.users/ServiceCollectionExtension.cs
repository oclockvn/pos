using Microsoft.Extensions.DependencyInjection;
using pos.users.Services;

namespace pos.users
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserService, UserService>()
                ;
        }
    }
}
