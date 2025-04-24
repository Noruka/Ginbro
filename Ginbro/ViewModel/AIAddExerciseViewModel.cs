
using System.Collections.ObjectModel;
using Ginbro.AI_Data;
using Ginbro.AI_Model;
using Ginbro.AIModel;

namespace Ginbro.ViewModel
{
    public class AIAddExerciseViewModel
    {
        private readonly AIExerciseDao _exerciseDao;
        private readonly AIExerciseTemplateDao _exerciseTemplateDao;

        public AIAddExerciseViewModel(AIExerciseTemplateDao exerciseTemplateDao, AIExerciseDao exerciseDao)
        {
            _exerciseTemplateDao = exerciseTemplateDao;
            _exerciseDao = exerciseDao;
        }

        public ObservableCollection<AIExerciseTemplate> ExerciseTemplates { get; set; } = new();
        public AIExercise Exercise { get; set; } = new AIExercise();

        public async Task LoadExerciseTemplates()
        {
            ExerciseTemplates.Clear();
            var templates = await _exerciseTemplateDao.GetAll();
            foreach (var template in templates) ExerciseTemplates.Add(template);
        }

        public async Task SaveExercise()
        {
            await _exerciseDao.Add(Exercise);
        }
    }
}