csharp
namespace Ginbro.View;

using Ginbro.Shared;
using Ginbro.ViewModel;

public partial class AIConfigPage : ContentPage 
{
    public AIConfigPage(SqliteConnectionFactory connectionFactory) 
    {
        InitializeComponent();
        var connection = connectionFactory.GetConnectionSync();
        BindingContext = new AIConfigViewModel(connection);
    }
}
