namespace ScreenDrafts.Api.Persistence.Initialization;
internal sealed class DatabaseInitializer : IDatabaseInitializer
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(
        ApplicationDbContext dbContext,
        ILogger<DatabaseInitializer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task InitializeDatabaseAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Database.GetPendingMigrations().Any())
        {
            _logger.LogInformation("Applying pending migrations to the database.");
            await _dbContext.Database.MigrateAsync(cancellationToken);
        }
    }
}
