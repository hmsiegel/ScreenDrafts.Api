using ILogger = Serilog.ILogger;

namespace ScreenDrafts.Api.Persistence;
public static class Startup
{
    public static readonly Assembly AssemblyReference = typeof(Startup).Assembly;
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(nameof(DatabaseSettings))
            .PostConfigure(dbSettings =>
                _logger.Information("Current Database Provider: {dbProvider}", dbSettings.DBProvider))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddDbContext<ApplicationDbContext>((p, m) =>
            {
                var dbSettings = p.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                m.UseDatabase(dbSettings.DBProvider, dbSettings.ConnectionString);
            })
            .AddTransient<IDatabaseInitializer, DatabaseInitializer>();

        return services;
    }

    public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default!)
    {
        using var scope = services.CreateScope();

        await scope.ServiceProvider
            .GetRequiredService<IDatabaseInitializer>()
            .InitializeDatabaseAsync(cancellationToken);
    }

    internal static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string dbProvider, string connectionString)
    {
        return dbProvider.ToLowerInvariant() switch
        {
            DbProviderKeys.Npgsql => builder.UseNpgsql(connectionString, e =>
                     e.MigrationsAssembly("Migrators.PostgreSQL"))
                    .UseSnakeCaseNamingConvention(),
            DbProviderKeys.SqlServer => builder.UseSqlServer(connectionString, e =>
                     e.MigrationsAssembly("Migrators.MSSQL"))
                    .UseSnakeCaseNamingConvention(),
            DbProviderKeys.MySql => builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), e =>
                     e.MigrationsAssembly("Migrators.MySQL")
                      .SchemaBehavior(MySqlSchemaBehavior.Ignore))
                    .UseSnakeCaseNamingConvention(),
            DbProviderKeys.Oracle => builder.UseOracle(connectionString, e =>
                     e.MigrationsAssembly("Migrators.Oracle"))
                    .UseSnakeCaseNamingConvention(),
            DbProviderKeys.SqLite => builder.UseSqlite(connectionString, e =>
                     e.MigrationsAssembly("Migrators.SqLite"))
                    .UseSnakeCaseNamingConvention(),
            _ => throw new InvalidOperationException($"DB Provider {dbProvider} is not supported."),
        };
    }
}
