csharp
using Ginbro.Shared;
using Ginbro.ViewModel;
using Microsoft.Maui.Controls.Xaml;
using System.Diagnostics;

namespace Ginbro.View;

public partial class AIDetailPage : ContentPage, INavigationParameter
{
    private readonly SqliteConnectionFactory _connectionFactory;
    private readonly AIDetailViewModel _viewModel;

    public AIDetailPage(SqliteConnectionFactory connectionFactory)
    {
        InitializeComponent();
        _connectionFactory = connectionFactory;
        _viewModel = new AIDetailViewModel(_connectionFactory.GetConnectionSync());
        BindingContext = _viewModel;
        Loaded += AIDetailPage_Loaded;
    }

    public int Id { get; set; }

    private async void AIDetailPage_Loaded(object sender, EventArgs e)
    {
        await _viewModel.LoadExercise(Id);
    }

    private void StartButton_Clicked(object sender, EventArgs e)
    {
        _viewModel.StartTimer();
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void PauseButton_Clicked(object sender, EventArgs e)
    {
        _viewModel.PauseTimer();
    }

    private void StopButton_Clicked(object sender, EventArgs e)
    {
        _viewModel.StopTimer();
    }

    public interface INavigationParameter
    {
        public int Id { get; set; }
    }
}