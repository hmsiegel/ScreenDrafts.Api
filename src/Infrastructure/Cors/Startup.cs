namespace ScreenDrafts.Api.Infrastructure.Cors;
internal static class Startup
{
    private const string _corsPolicy = nameof(_corsPolicy);

    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
    {
        var corsSettings = config.GetSection(nameof(CorsSettings)).Get<CorsSettings>();
        if (corsSettings is null)
        {
            return services;
        }

        var origins = new List<string>();

        if (corsSettings.Angular is not null)
        {
            origins.AddRange(corsSettings.Angular.Split(';', StringSplitOptions.RemoveEmptyEntries));
        }

        if (corsSettings.Blazor is not null)
        {
            origins.AddRange(corsSettings.Blazor.Split(';', StringSplitOptions.RemoveEmptyEntries));
        }

        if (corsSettings.React is not null)
        {
            origins.AddRange(corsSettings.React.Split(';', StringSplitOptions.RemoveEmptyEntries));
        }

        services.AddCors(options =>
        {
            options.AddPolicy(_corsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .WithOrigins(origins.ToArray());
            });
        });

        return services;
    }
}
