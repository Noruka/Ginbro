using Ginbro.Model;
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
        ISQLiteConnection database = _connection.CreateConnection();
        database.CreateTable<ExerciseDto>();

        base.OnStart();
    }
}