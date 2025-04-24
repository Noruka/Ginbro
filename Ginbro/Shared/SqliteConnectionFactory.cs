using Ginbro.AI_Model;
using SQLite;

namespace Ginbro.Shared;

public class SqliteConnectionFactory
{
    private readonly string _databasePath;

    public SqliteConnectionFactory()
    {
        _databasePath = Path.Combine(FileSystem.AppDataDirectory, "ginbro.db3");
    }
    
    public async Task<ISQLiteAsyncConnection> CreateConnectionAsync()
    {
        var connection = new SQLiteAsyncConnection(_databasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        await CreateTablesAsync(connection);
        return connection;
    }

    public SQLiteConnection CreateConnection()
    {
        var connection = new SQLiteConnection(_databasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        CreateTables(connection);
        return connection;
    }

    private async Task CreateTablesAsync(ISQLiteAsyncConnection connection)
    {
        await connection.CreateTableAsync<AIExercise>();
        await connection.CreateTableAsync<AISerie>();
        await connection.CreateTableAsync<AITemplate>();
        await connection.CreateTableAsync<AISerieTemplate>();
    }

    private void CreateTables(SQLiteConnection connection)
    {
        connection.CreateTable<AIExercise>();
        connection.CreateTable<AISerie>();
        connection.CreateTable<AITemplate>();
        connection.CreateTable<AISerieTemplate>();
    }
}
