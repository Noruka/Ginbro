using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Ginbro.AIData;
using Ginbro.AIModel;
using Ginbro.Shared;

namespace Ginbro.ViewModel;

public class AiDetailViewModel : INotifyPropertyChanged, IDisposable
{
    private readonly AiExerciseDao _aiExerciseDao;
    private readonly AISerieDao _serieDao;
    private readonly Stopwatch _stopwatch;
    private readonly AITemplateDao _templateDao;
    private CancellationTokenSource _cancellationTokenSource;
    private readonly SqliteConnectionFactory _connectionFactory;


    private AIExercise _exercise;

    private TimeSpan _timer;

    private bool _isTimerRunning;


    public AiDetailViewModel(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _aiExerciseDao = new AiExerciseDao(_connectionFactory);
        _serieDao = new AISerieDao(_connectionFactory);
        _templateDao = new AITemplateDao(_connectionFactory);

        _stopwatch = new Stopwatch();
        _cancellationTokenSource = new CancellationTokenSource();
        StartTimerCommand = new Command(StartTimer);
        StopTimerCommand = new Command(StopTimer);
        PauseTimerCommand = new Command(PauseTimer);
        GoBackCommand = new Command(GoBack);
        AddAISerieCommand = new Command(AddAISerie);

    }

    public event PropertyChangedEventHandler PropertyChanged;

    public AIExercise Exercise
    {
        get => _exercise;
        set
        {
            _exercise = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<AISerie> Series { get; } = new();

    public TimeSpan Timer
    {
        get => _timer;
        set
        {
            _timer = value;
            OnPropertyChanged();
        }
    }

    public bool IsTimerRunning
    {
        get => _isTimerRunning;
        set
        {
            _isTimerRunning = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddAISerieCommand { get; }
    public ICommand DeleteAISerieCommand { get; private set; }
    public ICommand UpdateAISerieCommand { get; private set; }
    public ICommand StartTimerCommand { get; }
    public ICommand StopTimerCommand { get; }
    public ICommand PauseTimerCommand { get; }
    public ICommand GoBackCommand { get; }


    protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public async Task LoadExercise(int exerciseId)
    {
        Exercise = await _aiExerciseDao.ReadByIdAsync(exerciseId);
        await LoadSeries(exerciseId);
    }
    
    public async Task LoadSeries(int exerciseId)
    {
        if (Exercise != null)
        {
            Series.Clear();
            var series = await _serieDao.ReadByExerciseIdAsync(exerciseId);
            foreach (var serie in series) Series.Add(serie);
        }
    }

    private async Task StartTimer()
    {
        _stopwatch.Start();
        IsTimerRunning = true;
        _cancellationTokenSource = new CancellationTokenSource();
        var token = _cancellationTokenSource.Token;
        await Task.Run(async () =>
        {
            while (!token.IsCancellationRequested)
            {
                Timer = _stopwatch.Elapsed;
                await Task.Delay(100);
            }
        }, token);
    }

    public async Task StopTimer()
    {
        _stopwatch.Stop();
        IsTimerRunning = false;
        Exercise.TimeElapsed = Timer;
        await _exerciseDao.UpdateAsync(Exercise);
    }

    private void PauseTimer()
    {
        _cancellationTokenSource.Cancel();
        _stopwatch.Stop();
        IsTimerRunning = false;
    }

    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}