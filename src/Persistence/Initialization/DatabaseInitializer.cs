namespace ScreenDrafts.Api.Persistence.Initialization;
internal sealed class DatabaseInitializer : IDatabaseInitializer
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly ApplicationDbSeeder _dbSeeder;

    public DatabaseInitializer(
        ApplicationDbContext dbContext,
        ILogger<DatabaseInitializer> logger,
        ApplicationDbSeeder dbSeeder)
    {
        _dbContext = dbContext;
        _logger = logger;
        _dbSeeder = dbSeeder;
    }

    public async Task InitializeDatabaseAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Database.GetPendingMigrations().Any())
        {
            _logger.LogInformation("Applying pending migrations to the database.");
            await _dbContext.Database.MigrateAsync(cancellationToken);
        }

        if (await _dbContext.Database.CanConnectAsync(cancellationToken))
        {
            _logger.LogInformation("Database connection established.");
            await _dbSeeder.SeedDatabaseAsync(_dbContext, cancellationToken);
        }
    }
}
