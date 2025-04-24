csharp
using System.Collections.ObjectModel;
using Ginbro.AI_Data;
using Ginbro.AI_Model;

namespace Ginbro.ViewModel
{
    public class AIAddExerciseViewModel
    {
        private readonly AIExerciseTemplateDao _exerciseTemplateDao;
        private readonly AIExerciseDao _exerciseDao;

        public ObservableCollection<AIExerciseTemplate> ExerciseTemplates { get; set; } = new ObservableCollection<AIExerciseTemplate>();
        public AIExercise Exercise { get; set; } = new AIExercise();

        public AIAddExerciseViewModel(AIExerciseTemplateDao exerciseTemplateDao, AIExerciseDao exerciseDao)
        {
            _exerciseTemplateDao = exerciseTemplateDao;
            _exerciseDao = exerciseDao;
        }

        public async Task LoadExerciseTemplates()
        {
            ExerciseTemplates.Clear();
            var templates = await _exerciseTemplateDao.GetAll();
            foreach (var template in templates)
            {
                ExerciseTemplates.Add(template);
            }
        }

        public async Task SaveExercise()
        {
            await _exerciseDao.Add(Exercise);
        }
    }
}