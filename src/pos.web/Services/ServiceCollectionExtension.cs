namespace pos.web.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<TokenSetting>(configuration.GetSection("JwtToken"))
                .AddScoped<ITokenService, TokenService>()
                ;
        }
    }
}
