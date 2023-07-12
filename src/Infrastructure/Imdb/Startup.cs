namespace ScreenDrafts.Api.Infrastructure.Imdb;
internal static class Startup
{
    internal static IServiceCollection AddImdb(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<ImdbSettings>()
            .Bind(config.GetSection(nameof(ImdbSettings)))
            .ValidateDataAnnotations();

        return services;
    }
}
