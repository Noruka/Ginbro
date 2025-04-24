csharp
using Ginbro.AI_Data;
using Ginbro.AI_Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Ginbro.ViewModel
{
    public class AIDetailViewModel : INotifyPropertyChanged
    {
        private readonly AIExerciseDao _exerciseDao;
        private readonly AISerieDao _serieDao;
        private readonly AITemplateDao _templateDao;
        private Stopwatch _stopwatch;
        private CancellationTokenSource _cancellationTokenSource;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private AIExercise _exercise;
        public AIExercise Exercise
        {
            get => _exercise;
            set
            {
                _exercise = value;
                OnPropertyChanged(nameof(Exercise));
            }
        }
        public ObservableCollection<AISerie> Series { get; set; } = new ObservableCollection<AISerie>();
        
        private TimeSpan _timer;
        public TimeSpan Timer
        {
            get => _timer;
            set
            {
                _timer = value;
                OnPropertyChanged(nameof(Timer));
            }
        }

        public bool IsTimerRunning { get; set; }

        public ICommand AddAISerieCommand { get; private set; }
        public ICommand DeleteAISerieCommand { get; private set; }
        public ICommand UpdateAISerieCommand { get; private set; }
        public ICommand StartTimerCommand { get; private set; }
        public ICommand StopTimerCommand { get; private set; }
        public ICommand PauseTimerCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }


        public AIDetailViewModel(AIExerciseDao exerciseDao, AISerieDao serieDao, AITemplateDao templateDao)
        {
            _exerciseDao = exerciseDao;
            _serieDao = serieDao;
            _templateDao = templateDao;
            _stopwatch = new Stopwatch();
            _cancellationTokenSource = new CancellationTokenSource();
            StartTimerCommand = new Command(async () => await StartTimer());
            StopTimerCommand = new Command(async () => await StopTimer());
            PauseTimerCommand = new Command(() => PauseTimer());
            GoBackCommand = new Command(async () => await GoBack());
        }

        public async Task LoadExercise(int exerciseId)
        {
            Exercise = await _exerciseDao.GetById(exerciseId);
            await LoadSeries(exerciseId);
        }

        public async Task LoadSeries(int exerciseId)
        {
            if (Exercise != null)
            {
                Series.Clear();
                var series = await _serieDao.GetAllByExerciseId(exerciseId);
                foreach (var serie in series)
                {
                    Series.Add(serie);
                }
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
            await _exerciseDao.Update(Exercise);
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
}