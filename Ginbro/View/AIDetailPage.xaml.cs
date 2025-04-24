csharp
namespace Ginbro.View;

public interface INavigationParameter
{
    public int Id { get; set; }
}

public partial class AIDetailPage : ContentPage, INavigationParameter
{
    private readonly AiDetailViewModel _viewModel;

    public int Id { get; set; }

    public AIDetailPage(AiDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        Loaded += AIDetailPage_Loaded;
    }

    private async void AIDetailPage_Loaded(object sender, EventArgs e)
    {
        await _viewModel.LoadExercise(Id);
    }
}