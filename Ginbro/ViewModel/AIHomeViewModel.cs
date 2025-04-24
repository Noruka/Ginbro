using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Ginbro.AIData;
using Ginbro.AIModel;
using Ginbro.Shared;
using Ginbro.View;

namespace Ginbro.ViewModel;

public class AiHomeViewModel : INotifyPropertyChanged
{
    private readonly SqliteConnectionFactory _connectionFactory;
    private readonly AIExerciseDao _exerciseDao;

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public AiHomeViewModel(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _exerciseDao = new AIExerciseDao(_connectionFactory);
        AddExerciseCommand = new Command(async () => await AddExercise());
        DeleteExerciseCommand = new Command<AIExercise>(async exercise => await DeleteExercise(exercise));
        GoToConfigCommand = new Command(async () => await GoToConfig());
        GoToDetailCommand = new Command<int>(async id => await GoToDetail(id));
    }

    public ObservableCollection<AIExercise> Exercises { get; private set; } = new();
    public ICommand AddExerciseCommand { get; private set; }
    public ICommand DeleteExerciseCommand { get; private set; }
    public ICommand GoToConfigCommand { get; private set; }
    public ICommand GoToDetailCommand { get; private set; }

    public async void LoadExercises()
    {
        Exercises.Clear();
        var exercises = await _exerciseDao.ReadAllAsync();
        foreach (var exercise in exercises.OrderByDescending(x => x.Date)) Exercises.Add(exercise);
    }

    private async Task AddExercise()
    {
    }

    private async Task DeleteExercise(AIExercise exercise) {
        await _exerciseDao.DeleteAsync(exercise);
        await LoadExercises();
    }

    private async Task GoToConfig()
    {
        await Shell.Current.GoToAsync(nameof(AIConfigPage));
    }

    private async Task GoToDetail(int id)
    {
        await Shell.Current.GoToAsync($"{nameof(AIDetailPage)}?ExerciseId={id}");
    }
}