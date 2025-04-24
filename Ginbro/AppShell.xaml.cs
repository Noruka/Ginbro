using Ginbro.View;

namespace Ginbro;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AIHomePage), typeof(AIHomePage));
        Routing.RegisterRoute(nameof(AIConfigPage), typeof(AIConfigPage));
        Routing.RegisterRoute(nameof(AIDetailPage), typeof(AIDetailPage));
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
    }
}