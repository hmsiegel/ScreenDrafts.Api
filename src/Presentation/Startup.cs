namespace ScreenDrafts.Api.Presentation;
public static class Startup
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddApplicationPart(typeof(Startup).Assembly);

        return services;
    }
}
