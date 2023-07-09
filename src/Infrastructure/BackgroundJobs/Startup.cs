﻿using ILogger = Serilog.ILogger;

namespace ScreenDrafts.Api.Persistence.BackgroundJobs;
internal static class Startup
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

    internal static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration config)
    {
        services.AddHangfireServer(options => config.GetSection("HangfireSettings:Server").Bind(options));

        services.AddHangfireConsoleExtensions();

        var storageSettings = config.GetSection("HangfireSettings:Storage").Get<HangfireStorageSettings>() ?? throw new Exception("Hangfire Storage Provider is not configured.");
        if (string.IsNullOrEmpty(storageSettings.StorageProvider))
        {
            throw new ("Hangfire Storage Provider is not configured.");
        }

        if (string.IsNullOrEmpty(storageSettings.ConnectionString))
        {
            throw new ("Hangfire Storage Provider ConnectionString is not configured.");
        }

        _logger.Information($"Hangfire: Current Storage Provider : {storageSettings.StorageProvider}");
        _logger.Information("For more Hangfire storage, visit https://www.hangfire.io/extensions.html");

        services.AddSingleton<JobActivator, ScreenDraftsJobActivator>();

        services.AddHangfire((provider, hangfireConfig) => hangfireConfig
            .UseDatabase(storageSettings.StorageProvider, storageSettings.ConnectionString, config)
            .UseFilter(new ScreenDraftsJobFilter(provider))
            .UseFilter(new LogJobFilter())
            .UseConsole());

        return services;
    }

    internal static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app, IConfiguration config)
    {
        var dashboardOptions = config.GetSection("HangfireSettings:Dashboard").Get<DashboardOptions>() ?? throw new Exception("Hangfire Dashboard is not configured.");
        dashboardOptions.Authorization = new[]
        {
           new HangfireCustomBasicAuthenticationFilter
           {
                User = config.GetSection("HangfireSettings:Credentials:User").Value!,
                Pass = config.GetSection("HangfireSettings:Credentials:Password").Value!,
           },
        };

        return app.UseHangfireDashboard(config["HangfireSettings:Route"], dashboardOptions);
    }

    private static IGlobalConfiguration UseDatabase(this IGlobalConfiguration hangfireConfig, string dbProvider, string connectionString, IConfiguration config) =>
        dbProvider.ToLowerInvariant() switch
        {
            DbProviderKeys.Npgsql =>
                hangfireConfig.UsePostgreSqlStorage(connectionString, config.GetSection("HangfireSettings:Storage:Options").Get<PostgreSqlStorageOptions>()),
            DbProviderKeys.SqlServer =>
                hangfireConfig.UseSqlServerStorage(connectionString, config.GetSection("HangfireSettings:Storage:Options").Get<SqlServerStorageOptions>()),
            DbProviderKeys.SqLite =>
                hangfireConfig.UseSQLiteStorage(connectionString, config.GetSection("HangfireSettings:Storage:Options").Get<SQLiteStorageOptions>()),
            DbProviderKeys.MySql =>
                    hangfireConfig.UseStorage(new MySqlStorage(connectionString, config.GetSection("HangfireSettings:Storage:Options").Get<MySqlStorageOptions>())),
            _ => throw new Exception($"Hangfire Storage Provider {dbProvider} is not supported.")
        };
}
