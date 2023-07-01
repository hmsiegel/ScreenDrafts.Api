namespace ScreenDrafts.Api.Infrastructure;
public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);

        return services;
    }
}
