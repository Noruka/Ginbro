using System.Collections.ObjectModel;
using Ginbro.AI_Data;
using Ginbro.AI_Model;
using Ginbro.AIData;
using Ginbro.AIModel;

namespace Ginbro.ViewModel;

public abstract class AiAddExerciseTemplateViewModel
{
    private readonly AIExerciseTemplateDao _exerciseTemplateDao;
    private readonly AISerieDao _serieDao;

    public AiAddExerciseTemplateViewModel(AIExerciseTemplateDao exerciseTemplateDao, AISerieDao serieDao)
    {
        _exerciseTemplateDao = exerciseTemplateDao;
        _serieDao = serieDao;
    }

    public AIExerciseTemplate ExerciseTemplate { get; set; } = new();
    public ObservableCollection<AISerie> Series { get; set; } = new();

    public void AddSerie()
    {
        Series.Add(new AISerie());
    }

    public async Task SaveExerciseTemplate()
    {
        if (ExerciseTemplate.Id == 0)
        {
            await _exerciseTemplateDao.AddExerciseTemplate(ExerciseTemplate);
            foreach (var serie in Series)
            {
                serie.ExerciseTemplateId = ExerciseTemplate.Id;
                await _serieDao.AddSerie(serie);
            }
        }
        else
        {
            await _exerciseTemplateDao.UpdateExerciseTemplate(ExerciseTemplate);
            foreach (var serie in Series)
                if (serie.Id == 0)
                {
                    serie.ExerciseTemplateId = ExerciseTemplate.Id;
                    await _serieDao.AddSerie(serie);
                }
                else
                {
                    await _serieDao.UpdateSerie(serie);
                }
        }
    }
}