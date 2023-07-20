using ILogger = Serilog.ILogger;

namespace ScreenDrafts.Api.Persistence;
public static class Startup
{
    public static readonly Assembly AssemblyReference = typeof(Startup).Assembly;
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(nameof(DatabaseSettings))
            .PostConfigure(dbSettings =>
                _logger.Information("Current Database Provider: {dbProvider}", dbSettings.DBProvider))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<AdminSettings>()
            .BindConfiguration(nameof(AdminSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddIdentity()
            .AddDbContext<ApplicationDbContext>((p, m) =>
            {
                var dbSettings = p.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                m.UseDatabase(dbSettings.DBProvider, dbSettings.ConnectionString);
            })
            .AddTransient<IDatabaseInitializer, DatabaseInitializer>()
            .AddTransient<ApplicationDbSeeder>()
            .AddServices(typeof(ICustomSeeder), ServiceLifetime.Transient)
            .AddTransient<CustomSeederRunner>()
            .AddTransient<IConnectionStringSecurer, ConnectionStringSecurer>()
            .AddTransient<IConnectionStringValidator, ConnectionStringValidator>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

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
                    .UseCamelCaseNamingConvention(),
            DbProviderKeys.SqlServer => builder.UseSqlServer(connectionString, e =>
                     e.MigrationsAssembly("Migrators.MSSQL"))
                    .UseCamelCaseNamingConvention(),
            DbProviderKeys.MySql => builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), e =>
                     e.MigrationsAssembly("Migrators.MySQL")
                      .SchemaBehavior(MySqlSchemaBehavior.Ignore))
                    .UseCamelCaseNamingConvention(),
            DbProviderKeys.Oracle => builder.UseOracle(connectionString, e =>
                     e.MigrationsAssembly("Migrators.Oracle"))
                    .UseCamelCaseNamingConvention(),
            DbProviderKeys.SqLite => builder.UseSqlite(connectionString, e =>
                     e.MigrationsAssembly("Migrators.SqLite"))
                    .UseCamelCaseNamingConvention(),
            _ => throw new InvalidOperationException($"Database Provider {dbProvider} is not supported."),
        };
    }
}
