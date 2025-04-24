using Ginbro.Shared;
using SQLite;

namespace Ginbro;

public partial class App : Application
{
    private readonly SqliteConnectionFactory _connection;

    public App(SqliteConnectionFactory connection)
    {
        InitializeComponent();

        MainPage = new AppShell();

        _connection = connection;
    }

    protected override void OnStart()
    {
        var database = _connection.CreateConnection();
        _connection.CreateTables((SQLiteConnection)database);
        base.OnStart();
    }
}