using SQLite;

namespace Ginbro.Shared;

public class SqliteConnectionFactory
{
    public ISQLiteAsyncConnection CreateConnection()
    {
        return new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "ginbro.db3"), SQLiteOpenFlags.ReadWrite |SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
    }
}