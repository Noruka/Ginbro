csharp
﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Ginbro.AI_Data;
using Ginbro.AI_Model;
using Ginbro.Shared;
using Ginbro.View;
using SQLite;

namespace Ginbro.ViewModel;

public class AIHomeViewModel
{
    private readonly AIExerciseDao _exerciseDao;
    private readonly SqliteConnectionFactory _connectionFactory;
    public ObservableCollection<AIExercise> Exercises { get; private set; } = new ObservableCollection<AIExercise>();
    public ICommand AddExerciseCommand { get; private set; }
    public ICommand DeleteExerciseCommand { get; private set; }
    public ICommand GoToConfigCommand { get; private set; }
    public ICommand GoToDetailCommand { get; private set; }

    public AIHomeViewModel(SqliteConnection connectionFactory)
    {
        _connectionFactory = new SqliteConnectionFactory();
        _exerciseDao = new AIExerciseDao(_connectionFactory.GetConnectionSync());
        AddExerciseCommand = new Command(async () => await AddExercise());
        DeleteExerciseCommand = new Command<AIExercise>(async (exercise) => await DeleteExercise(exercise));
        GoToConfigCommand = new Command(async () => await GoToConfig());
        GoToDetailCommand = new Command<int>(async (id) => await GoToDetail(id));

    }

    public async Task LoadExercises()
    {
        Exercises.Clear();
        var exercises = await _exerciseDao.ReadAll();
        foreach (var exercise in exercises.OrderByDescending(x => x.Date))
        {
            Exercises.Add(exercise);
        }
    }
    private async Task AddExercise() { }
    private async Task DeleteExercise(AIExercise exercise) { await _exerciseDao.Delete(exercise); await LoadExercises(); }
    private async Task GoToConfig() { await Shell.Current.GoToAsync(nameof(AIConfigPage)); }
    private async Task GoToDetail(int id) { await Shell.Current.GoToAsync($"{nameof(AIDetailPage)}?ExerciseId={id}"); }
    
    }