using System.IO;
using Microsoft.Data.Sqlite;

namespace FactoryGuardian.Database;

public class FactoryDbContext
{
    private readonly string _connectionString;

    public FactoryDbContext()
    {
        var dbPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "FactoryGuardian.db");

        _connectionString = $"Data Source={dbPath}";
    }

    public SqliteConnection CreateConnection()
    {
        var connection = new SqliteConnection(_connectionString);

        connection.Open();

        return connection;
    }
}