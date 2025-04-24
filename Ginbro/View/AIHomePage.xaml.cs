csharp
﻿using Ginbro.AI_Model;
using Ginbro.Shared;
using Ginbro.ViewModel;

namespace Ginbro.View;

public partial class AIHomePage : ContentPage
{
    private readonly SqliteConnectionFactory _connectionFactory;
    private readonly AiHomeViewModel _viewModel;

    public AIHomePage(SqliteConnectionFactory connectionFactory)
    {
        InitializeComponent();
        _connectionFactory = connectionFactory;
        _viewModel = new AiHomeViewModel(_connectionFactory.GetConnectionSync());
        BindingContext = _viewModel;
        _viewModel.LoadExercises();

        _viewModel.GoToConfigCommand =
            new Command(async () => await Navigation.PushAsync(new AIConfigPage(_connectionFactory)));

        _viewModel.GoToDetailCommand = new Command<int>(async exerciseId =>
            await Navigation.PushAsync(new AIDetailPage(_connectionFactory, exerciseId)));

        _viewModel.AddExerciseCommand =
            new Command(async () => await Navigation.PushAsync(new AIAddExercisePage(_connectionFactory)));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadExercises();
    }
}