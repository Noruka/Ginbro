csharp
using Ginbro.ViewModel;
using Microsoft.Maui.Controls;
using System;

namespace Ginbro.View
{
    public partial class AIAddExerciseTemplatePage : ContentPage
    {
        private readonly AiAddExerciseTemplateViewModel _viewModel;

        public AIAddExerciseTemplatePage(AiAddExerciseTemplateViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        private void AddSerieButton_Clicked(object sender, EventArgs e)
        {
            _viewModel.AddSerie();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            await _viewModel.SaveExerciseTemplate();
            await Navigation.PopAsync();
        }
    }
}