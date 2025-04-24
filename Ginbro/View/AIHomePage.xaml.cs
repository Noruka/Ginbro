csharp
﻿using Ginbro.Shared;
using Ginbro.ViewModel;

namespace Ginbro.View;

public partial class AIHomePage : ContentPage
{
    private readonly AiHomeViewModel _viewModel; 

    public AIHomePage(AiHomeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _viewModel.LoadExercises();

    }

    private async void GoToConfig_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AIConfigPage));
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadExercises();
    }
}