csharp
using Ginbro.ViewModel;
using Microsoft.Maui.Controls;

namespace Ginbro.View;

public partial class AIAddExercisePage : ContentPage
{
    private readonly AiAddExerciseViewModel _viewModel;

    public AIAddExercisePage(AiAddExerciseViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _viewModel.LoadExerciseTemplates();
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        await _viewModel.SaveExercise();
        await Navigation.PopAsync();
    }
}