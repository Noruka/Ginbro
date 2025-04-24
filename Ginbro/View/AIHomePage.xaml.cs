csharp
﻿using Ginbro.Shared;
using Ginbro.ViewModel;

namespace Ginbro.View
{
    public partial class AIHomePage : ContentPage
    {
        private readonly AIHomeViewModel _viewModel;
        private readonly SqliteConnectionFactory _connectionFactory;

        public AIHomePage(SqliteConnectionFactory connectionFactory)
        {
            InitializeComponent();
            _connectionFactory = connectionFactory;
            _viewModel = new AIHomeViewModel(_connectionFactory.GetConnectionSync());
            BindingContext = _viewModel;
            _viewModel.LoadExercises();

            _viewModel.NavigateToConfigCommand = new Command(async () => await Navigation.PushAsync(new AIConfigPage(_connectionFactory)));

            _viewModel.NavigateToDetailCommand = new Command<int>(async (exerciseId) => await Navigation.PushAsync(new AIDetailPage(_connectionFactory,exerciseId)));

            _viewModel.AddExerciseCommand = new Command(async() => await Navigation.PushAsync(new AIAddExercisePage(_connectionFactory)));

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadExercises();
        }
    }
}