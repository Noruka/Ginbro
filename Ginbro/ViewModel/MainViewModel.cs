﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ginbro.Model;
using Ginbro.Shared;
using SQLite;

namespace Ginbro.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private readonly SqliteConnectionFactory _connection;
    public MainViewModel(SqliteConnectionFactory connection)
    {
        Items = new ObservableCollection<Exercise>();
        _connection = connection;
        
        LoadExercisesCommand.Execute(null);
    }
    
    [ObservableProperty]
    private ObservableCollection<Exercise> _items;
    
    [ObservableProperty]
    private string _text = String.Empty;

    [RelayCommand]
    private async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;
        
        ISQLiteAsyncConnection database = _connection.CreateConnection();

        ExerciseDto ticketDto = new ExerciseDto()
        {
            Name = _text,
            Date = DateTime.Now
        };
        
        await database.InsertAsync(ticketDto);
        
        Items.Add(new Exercise()
        {
            Name = ticketDto.Name,
            Id = ticketDto.Id,
            Series = new List<Serie>(),
            Date = ticketDto.Date
        });
        
        Text = string.Empty;
    }

    [RelayCommand]
    private async Task Delete(Exercise exercise)
    {
        ISQLiteAsyncConnection database = _connection.CreateConnection();

        var exerciseDto = await database.Table<ExerciseDto>().FirstOrDefaultAsync(w => w.Id == exercise.Id);

        await database.DeleteAsync(exerciseDto);
        
        _items.Remove(exercise);
    }

    [RelayCommand]
    private async Task Tap(Exercise exercise)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Id={exercise.Id.ToString()}");
    }

    [RelayCommand]
    private async Task LoadExercises()
    {
        ISQLiteAsyncConnection database = _connection.CreateConnection();
        _items.Clear();
        
        var exercisesDtos = await database.Table<ExerciseDto>().ToListAsync();
            
        foreach (var exerciseDto in exercisesDtos)
        {
            _items.Add(new Exercise()
            {
                Id = exerciseDto.Id,
                Name = exerciseDto.Name ?? string.Empty,
                Date = exerciseDto.Date
            });
        }
    }
}