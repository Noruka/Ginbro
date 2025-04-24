using Ginbro.AIModel;
using SQLite;

namespace Ginbro.Shared;

public class SqliteConnectionFactory
{
    private readonly string _databasePath;

    public SqliteConnectionFactory()
    {
        _databasePath = Path.Combine(FileSystem.AppDataDirectory, "ginbro.db3");
    }

    public async Task<ISQLiteAsyncConnection> CreateAsyncConnectionAsync()
    {
        var connection = new SQLiteAsyncConnection(_databasePath,
            SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        await CreateTablesAsync(connection);
        return connection;
    }

    public ISQLiteAsyncConnection CreateAsyncConnection()
    {
        return new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "ginbro.db3"),
            SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
    }

    public ISQLiteConnection CreateConnection()
    {
        return new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, "ginbro.db3"),
            SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
    }

    public async Task CreateTablesAsync(ISQLiteAsyncConnection connection)
    {
        await connection.CreateTableAsync<AIExercise>();
        await connection.CreateTableAsync<AISerie>();
        await connection.CreateTableAsync<AITemplate>();
        await connection.CreateTableAsync<AISerieTemplate>();
    }

    public void CreateTables(SQLiteConnection connection)
    {
        connection.CreateTable<AIExercise>();
        connection.CreateTable<AISerie>();
        connection.CreateTable<AITemplate>();
        connection.CreateTable<AISerieTemplate>();
    }
}