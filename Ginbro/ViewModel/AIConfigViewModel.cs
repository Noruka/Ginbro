using System.Collections.ObjectModel;
using System.Windows.Input;
using Ginbro.AI_Data;
using Ginbro.AIData;
using Ginbro.AIModel;
using SQLite;

namespace Ginbro.ViewModel;

public class AiConfigViewModel(SQLiteConnection connection) : IDisposable
{
    private readonly AISerieTemplateDao _aiSerieTemplateDao = new AISerieTemplateDao(connection);
    private readonly AITemplateDao _aiTemplateDao = new AITemplateDao(connection);

    public ObservableCollection<AITemplate> AITemplates { get; set; } = new();

    public ICommand AddAITemplateCommand { get; private set; } = new Command(async () => await AddAITemplate());

    public ICommand DeleteAITemplateCommand { get; private set; } =
        new Command<AITemplate>(async template => await DeleteAITemplate(template));

    public ICommand UpdateAITemplateCommand { get; private set; } =
        new Command<AITemplate>(async template => await UpdateAITemplate(template));


    public void Dispose()
    {
        _aiTemplateDao.Dispose();
        _aiSerieTemplateDao.Dispose();
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
        if (template is null) return;

        await _aiTemplateDao.Delete(template.Id);
        AITemplates.Remove(template);
    }

    private async Task UpdateAITemplate(AITemplate template)
    {
        // Implement logic to update a template
        if (template is null) return;

        await _aiTemplateDao.Update(template);
    }

    public async Task LoadAITemplates()
    {
        var templates = await _aiTemplateDao.ReadAll();
        foreach (var template in templates) AITemplates.Add(template);
    }
}