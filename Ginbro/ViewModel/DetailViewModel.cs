using CommunityToolkit.Mvvm.ComponentModel;

namespace Ginbro.ViewModel;

[QueryProperty("Id", "Id")]
public partial class DetailViewModel : ObservableObject
{
    [ObservableProperty]
    private int id;
    
    
}