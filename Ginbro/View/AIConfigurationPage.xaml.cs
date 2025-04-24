csharp
using Ginbro.ViewModel;
using Microsoft.Maui.Controls;

namespace Ginbro.View;

public partial class AIConfigurationPage : ContentPage
{
    private readonly AiConfigurationViewModel _viewModel;

    public AIConfigurationPage(AiConfigurationViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _viewModel.LoadExerciseTemplates();

        AddButton.Clicked += OnAddButtonClicked;
    }

    private async void OnAddButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AIAddExerciseTemplatePage));
    }
}