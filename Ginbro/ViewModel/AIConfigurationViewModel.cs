csharp
using Ginbro.AI_Data;
using Ginbro.AI_Model;
using System.Collections.ObjectModel;

namespace Ginbro.ViewModel
{
    public class AIConfigurationViewModel
    {
        private readonly AIExerciseTemplateDao _exerciseTemplateDao;
        public ObservableCollection<AIExerciseTemplate> ExerciseTemplates { get; set; } = new ObservableCollection<AIExerciseTemplate>();

        public AIConfigurationViewModel(AIExerciseTemplateDao exerciseTemplateDao)
        {
            _exerciseTemplateDao = exerciseTemplateDao;
        }

        public async Task LoadExerciseTemplates()
        {
            var templates = await _exerciseTemplateDao.GetAll();
            ExerciseTemplates.Clear();
            foreach (var template in templates)
            {
                ExerciseTemplates.Add(template);
            }
        }
    }
}