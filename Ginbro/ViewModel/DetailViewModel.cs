using CommunityToolkit.Mvvm.ComponentModel;

namespace Ginbro.ViewModel;

[QueryProperty("Text", "Text")]
public partial class DetailViewModel : ObservableObject
{
    [ObservableProperty]
    private string text;
    
    
}