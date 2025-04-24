using Ginbro.Shared;
using Ginbro.ViewModel;
using Microsoft.Maui.Controls;

namespace Ginbro.View; 

public partial class AIConfigPage : ContentPage
{
    private readonly AIConfigViewModel _viewModel;
    public AIConfigPage(AIConfigViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _viewModel.LoadAITemplates();
    }
}