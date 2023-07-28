namespace ScreenDrafts.Api.Host.ConfigurationSettings;
internal static class Startup
{
    internal static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        const string configDirectory = "ConfigurationSettings";
        var environment = builder.Environment;

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/logger.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/logger.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/openapi.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/openapi.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/hangfire.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/hangfire.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/cache.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/cache.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/cors.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/cors.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/database.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/database.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/mail.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/mail.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/middleware.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/middleware.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/security.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/security.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/signalr.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/signalr.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/securityheaders.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/securityheaders.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/imdb.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/imdb.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/admin.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configDirectory}/admin.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder;
    }
}
