using Ginbro.Shared;
using Ginbro.ViewModel;

csharp

namespace Ginbro.View;

public partial class AIConfigPage : ContentPage
{
    public AIConfigPage(SqliteConnectionFactory connectionFactory)
    {
        InitializeComponent();
        var connection = connectionFactory.GetConnectionSync();
        BindingContext = new AIConfigViewModel(connection);
    }
}