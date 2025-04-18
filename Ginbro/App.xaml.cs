using Ginbro.Model;
using Ginbro.Shared;

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

    protected override async void OnStart()
    {
        var database = _connection.CreateConnection();

        await database.CreateTableAsync<ExerciseDto>();
        
        base.OnStart();
    }
}