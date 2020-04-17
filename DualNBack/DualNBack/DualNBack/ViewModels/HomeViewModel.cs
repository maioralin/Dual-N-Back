using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DualNBack.ViewModels
{
    public class HomeViewModel: BaseViewModel
    {
        string welcomeMessage = string.Empty;
        public string WelcomeMessage
        {
            get { return welcomeMessage; }
            set { SetProperty(ref welcomeMessage, value); }
        }
        string currentLevel = string.Empty;
        public string CurrentLevel
        {
            get { return currentLevel; }
            set { SetProperty(ref currentLevel, value); }
        }
        public HomeViewModel()
        {
            GlobalVars.BestScore = Preferences.Get("max_score", GlobalVars.DEFAULT_SCORE);
            GlobalVars.CurrentLevel = Preferences.Get("current_level", GlobalVars.DEFAULT_SCORE);
            GlobalVars.RelegationStreak = Preferences.Get("relegation_streak", GlobalVars.DEFAULT_STREAK);
            Title = "Home";
            ResetScoreCommand = new Command(ResetScore);
            PlayCommand = new Command(Play);
            HomePageAppearingCommand = new Command(HomePageAppearing);
            ExitCommand = new Command(Exit);
        }

        public ICommand ResetScoreCommand { get; }
        public ICommand PlayCommand { get; }
        public ICommand HomePageAppearingCommand { get; }
        public ICommand ExitCommand { get; }

        private void ResetScore()
        {
            GlobalVars.BestScore = GlobalVars.DEFAULT_SCORE;
            GlobalVars.CurrentLevel = GlobalVars.DEFAULT_SCORE;
            GlobalVars.RelegationStreak = GlobalVars.DEFAULT_STREAK;
            Preferences.Set("max_score", GlobalVars.DEFAULT_SCORE);
            Preferences.Set("current_level", GlobalVars.DEFAULT_SCORE);
            Preferences.Set("relegation_streak", GlobalVars.DEFAULT_STREAK);
            WelcomeMessage = $"Your best level: {GlobalVars.BestScore}";
            CurrentLevel = $"Your current level: {GlobalVars.CurrentLevel}";
        }
        
        async private void Play()
        {
            await Shell.Current.GoToAsync($"play?level={GlobalVars.CurrentLevel}");
        }

        private void HomePageAppearing()
        {
            WelcomeMessage = $"Your best level: {GlobalVars.BestScore}";
            CurrentLevel = $"Your current level: {GlobalVars.CurrentLevel}";
        }

        private void Exit()
        {
            var closer = DependencyService.Get<IPlatformSpecificFunctions>();
            closer?.CloseApplication();
        }
    }
}
