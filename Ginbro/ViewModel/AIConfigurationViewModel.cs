using System.Collections.ObjectModel;
using Ginbro.AI_Data;
using Ginbro.AI_Model;

namespace Ginbro.ViewModel;

public class AIConfigurationViewModel
{
    private readonly AIExerciseTemplateDao _exerciseTemplateDao;

    public AIConfigurationViewModel(AIExerciseTemplateDao exerciseTemplateDao)
    {
        _exerciseTemplateDao = exerciseTemplateDao;
    }

    public ObservableCollection<AIExerciseTemplate> ExerciseTemplates { get; set; } = new();

    public async Task LoadExerciseTemplates()
    {
        var templates = await _exerciseTemplateDao.GetAll();
        ExerciseTemplates.Clear();
        foreach (var template in templates) ExerciseTemplates.Add(template);
    }
}