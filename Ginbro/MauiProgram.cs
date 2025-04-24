using Ginbro.AI_Data;
using Ginbro.Shared;
using Ginbro.View;
using Ginbro.ViewModel;
using Ginbro.Shared;
using Ginbro.View;
using Ginbro.ViewModel;
using Ginbro.ViewModel;
using Microsoft.Extensions.Logging;

namespace Ginbro;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<SqliteConnectionFactory>();
        builder.Services.AddSingleton(s => s.GetRequiredService<SqliteConnectionFactory>().CreateConnection());

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddTransient<DetailPage>();
        builder.Services.AddTransient<DetailViewModel>();

        //AI Features
        //DAOS
        builder.Services.AddTransient<AIExerciseDao>();
        builder.Services.AddTransient<AISerieDao>();
        builder.Services.AddTransient<AITemplateDao>();
        builder.Services.AddTransient<AISerieTemplateDao>();

        //ViewModels
        builder.Services.AddTransient<AIConfigViewModel>();
        builder.Services.AddTransient<AIHomeViewModel>();
        builder.Services.AddTransient<AIDetailViewModel>();

        //Views
        builder.Services.AddTransient<AIConfigPage>();
        builder.Services.AddTransient<AIHomePage>();
        builder.Services.AddTransient<AIDetailPage>();




        return builder.Build();
    }
}