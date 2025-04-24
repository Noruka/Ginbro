csharp
using Ginbro.AI_Data;
using Ginbro.AI_Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SQLite;

namespace Ginbro.ViewModel;

public class AIConfigViewModel : IDisposable
{
    private readonly AITemplateDao _aiTemplateDao;
    private readonly AISerieTemplateDao _aiSerieTemplateDao;

    public ObservableCollection<AITemplate> AITemplates { get; set; } = new ObservableCollection<AITemplate>();

    public ICommand AddAITemplateCommand { get; private set; }
    public ICommand DeleteAITemplateCommand { get; private set; }
    public ICommand UpdateAITemplateCommand { get; private set; }

    public AIConfigViewModel(SQLiteConnection connection)
    {
        _aiTemplateDao = new AITemplateDao(connection);
        _aiSerieTemplateDao = new AISerieTemplateDao(connection);

        AddAITemplateCommand = new Command(async () => await AddAITemplate());
        DeleteAITemplateCommand = new Command<AITemplate>(async (template) => await DeleteAITemplate(template));
        UpdateAITemplateCommand = new Command<AITemplate>(async (template) => await UpdateAITemplate(template));

        LoadAITemplates();
    }

    private async Task AddAITemplate()
    {
        // Implement logic to add a new template
        var newTemplate = new AITemplate { Name = "New Template" }; 
        await _aiTemplateDao.Create(newTemplate);
        AITemplates.Add(newTemplate);
    }

    private async Task DeleteAITemplate(AITemplate template)
    {
        // Implement logic to delete a template
        if (template != null)
        {
            await _aiTemplateDao.Delete(template.Id);
            AITemplates.Remove(template);
        }
    }

    private async Task UpdateAITemplate(AITemplate template)
    {
        // Implement logic to update a template
        if (template != null)
        {
            await _aiTemplateDao.Update(template);
        }
    }

    public async Task LoadAITemplates()
    {
        var templates = await _aiTemplateDao.ReadAll();
        foreach (var template in templates)
        {
            AITemplates.Add(template);
        }
    }

     public void Dispose()
    {
        _aiTemplateDao.Dispose();
        _aiSerieTemplateDao.Dispose();
    }
}