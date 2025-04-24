using Ginbro.Shared;
using Ginbro.AIModel;
using Ginbro.AIData;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Ginbro.ViewModel;

public class AIConfigViewModel : INotifyPropertyChanged, IDisposable
{
    private readonly SqliteConnectionFactory _connectionFactory;
    private readonly AISerieTemplateDao _aiSerieTemplateDao;
    private readonly AITemplateDao _aiTemplateDao;

    public ObservableCollection<AITemplate> AITemplates { get; private set; } = new ObservableCollection<AITemplate>();
    public ICommand AddAITemplateCommand { get; private set; }
    public ICommand DeleteAITemplateCommand { get; private set; }
    public ICommand UpdateAITemplateCommand { get; private set; }


    public AIConfigViewModel(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _aiSerieTemplateDao = new AISerieTemplateDao(_connectionFactory);
        _aiTemplateDao = new AITemplateDao(_connectionFactory);

        AddAITemplateCommand = new Command(async () => await AddAITemplate());
        DeleteAITemplateCommand = new Command<AITemplate>(async (template) => await DeleteAITemplate(template));
        UpdateAITemplateCommand = new Command<AITemplate>(async (template) => await UpdateAITemplate(template));
        
        LoadAITemplates();
    }

    public void Dispose()
    {
        _aiTemplateDao.Dispose();
        _aiSerieTemplateDao.Dispose();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private async Task AddAITemplate() => await _aiTemplateDao.Create(new AITemplate { Name = "New Template" });
    private async Task DeleteAITemplate(AITemplate template)
    {    
        if (template is null) return;
        await _aiTemplateDao.Delete(template.Id);
        AITemplates.Remove(template);
    }

    private async Task UpdateAITemplate(AITemplate template)
    {
        // Implement logic to update a template
        if (template is null) return;

        await _aiTemplateDao.Update(template);
        OnPropertyChanged(nameof(AITemplates));
    }

    public async Task LoadAITemplates()
    {
        var templates = await _aiTemplateDao.ReadAll();
        foreach (var template in templates) { AITemplates.Add(template);}
    }
}