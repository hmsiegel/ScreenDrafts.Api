namespace ScreenDrafts.Api.Persistence.ConnectionString;
public sealed class ConnectionStringSecurer : IConnectionStringSecurer
{
    private const string _hiddenValueDefault = "*******";
    private readonly DatabaseSettings _settings;

    public ConnectionStringSecurer(IOptions<DatabaseSettings> settings)
    {
        _settings = settings.Value;
    }

    public string? MakeSecure(string? connectionString, string? dbProvider)
    {
        if (connectionString is null || string.IsNullOrEmpty(connectionString))
        {
            return connectionString;
        }

        if (string.IsNullOrEmpty(dbProvider))
        {
            dbProvider = _settings.DBProvider;
        }

        return dbProvider switch
        {
            DbProviderKeys.Npgsql => MakeSecureNpgsqlConnectionstring(connectionString),
            DbProviderKeys.SqlServer => MakeSecureSqlConnectionstring(connectionString),
            DbProviderKeys.MySql => MakeSecureMySqlConnectionstring(connectionString),
            DbProviderKeys.SqLite => MakeSecureSqLiteConnectionstring(connectionString),
            DbProviderKeys.Oracle => MakeSecureOracleConnectionstring(connectionString),
            _ => connectionString
        };
    }

    private static string MakeSecureNpgsqlConnectionstring(string connectionString)
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString);

        if (!string.IsNullOrEmpty(builder.Password) || !builder.IntegratedSecurity)
        {
            builder.Password = _hiddenValueDefault;
        }

        if (!string.IsNullOrEmpty(builder.Username) || !builder.IntegratedSecurity)
        {
            builder.Username = _hiddenValueDefault;
        }

        return builder.ToString();
    }

    private static string MakeSecureSqlConnectionstring(string connectionString)
    {
        var builder = new SqlConnectionStringBuilder(connectionString);

        if (!string.IsNullOrEmpty(builder.Password) || !builder.IntegratedSecurity)
        {
            builder.Password = _hiddenValueDefault;
        }

        if (!string.IsNullOrEmpty(builder.UserID) || !builder.IntegratedSecurity)
        {
            builder.UserID = _hiddenValueDefault;
        }

        return builder.ToString();
    }

    private static string MakeSecureMySqlConnectionstring(string connectionString)
    {
        var builder = new MySqlConnectionStringBuilder(connectionString);

        if (!string.IsNullOrEmpty(builder.Password))
        {
            builder.Password = _hiddenValueDefault;
        }

        if (!string.IsNullOrEmpty(builder.UserID))
        {
            builder.UserID = _hiddenValueDefault;
        }

        return builder.ToString();
    }

    private static string MakeSecureSqLiteConnectionstring(string connectionString)
    {
        var builder = new SqliteConnection(connectionString);

        return builder.ToString();
    }

    private static string MakeSecureOracleConnectionstring(string connectionString)
    {
        var builder = new OracleConnectionStringBuilder(connectionString);

        if (!string.IsNullOrEmpty(builder.Password))
        {
            builder.Password = _hiddenValueDefault;
        }

        if (!string.IsNullOrEmpty(builder.UserID))
        {
            builder.UserID = _hiddenValueDefault;
        }

        return builder.ToString();
    }
}
