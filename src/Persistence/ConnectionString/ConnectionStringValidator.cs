﻿namespace ScreenDrafts.Api.Persistence.ConnectionString;
internal sealed class ConnectionStringValidator : IConnectionStringValidator
{
    private readonly DatabaseSettings _dbSettings;
    private readonly ILogger<ConnectionStringValidator> _logger;

    public ConnectionStringValidator(IOptions<DatabaseSettings> dbSettings, ILogger<ConnectionStringValidator> logger)
    {
        _dbSettings = dbSettings.Value;
        _logger = logger;
    }

    public bool TryValidate(string connectionString, string? dbProvider = null)
    {
        if (string.IsNullOrWhiteSpace(dbProvider))
        {
            dbProvider = _dbSettings.DBProvider;
        }

        try
        {
            switch (dbProvider?.ToLowerInvariant())
            {
                case DbProviderKeys.Npgsql:
                    var postgresqlcs = new NpgsqlConnectionStringBuilder(connectionString);
                    break;

                case DbProviderKeys.MySql:
                    var mysqlcs = new MySqlConnectionStringBuilder(connectionString);
                    break;

                case DbProviderKeys.SqlServer:
                    var mssqlcs = new SqlConnectionStringBuilder(connectionString);
                    break;

                case DbProviderKeys.SqLite:
                    var sqlite = new SqliteConnection(connectionString);
                    break;
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Connection String Validation Exception : {message}", ex.Message);
            return false;
        }
    }
}
