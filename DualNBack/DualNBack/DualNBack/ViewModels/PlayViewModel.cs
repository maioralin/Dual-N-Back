using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DualNBack.ViewModels
{
    [QueryProperty("Level", "level")]
    public class PlayViewModel: BaseViewModel
    {
        private Color[] colors = new Color[9];
        public Color[] Colors
        {
            get { return colors; }
            set { SetProperty(ref colors, value); }
        }

        private string level;
        public string Level
        {
            get { return level; }
            set { SetProperty(ref level, value); }
        }

        private string nBackText;
        public string NBackText
        {
            get { return nBackText; }
            set { SetProperty(ref nBackText, value); }
        }

        private bool waiting;
        public bool Waiting
        {
            get { return waiting; }
            set { SetProperty(ref waiting, value); }
        }

        private bool playStarted;
        public bool PlayStarted
        {
            get { return playStarted; }
            set { SetProperty(ref playStarted, value); }
        }

        private Color[] possibleColors = new Color[9];
        private bool positionPressed = false;
        private bool colorPressed = false;

        public PlayViewModel()
        {
            Title = "Play";
            NBackText = $"{GlobalVars.CurrentLevel}-back";
            Waiting = true;
            PlayStarted = false;
            PlayCommand = new Command(Play);
            PositionCommand = new Command(PositionFunc);
            ColorCommand = new Command(ColorFunc);
            for (int i = 0; i < Colors.Length; i++)
            {
                Colors[i] = Color.LightBlue;
            }
            possibleColors[0] = Color.FromHex("FF0000");
            possibleColors[1] = Color.FromHex("FF8000");
            possibleColors[2] = Color.FromHex("FFFF00");
            possibleColors[3] = Color.FromHex("00FF00");
            possibleColors[4] = Color.FromHex("0000FF");
            possibleColors[5] = Color.FromHex("FF00FF");
            possibleColors[6] = Color.FromHex("4C0099");
            possibleColors[7] = Color.FromHex("000000");
            possibleColors[8] = Color.FromHex("666666");
        }

        public ICommand PlayCommand { get; }
        public ICommand PositionCommand { get; }
        public ICommand ColorCommand { get; }

        private async void Play()
        {
            Waiting = false;
            PlayStarted = true;
            await Task.Delay(1000);
            int totalAnswers = 0;
            int correctAnswers = 0;
            int currentLevel = int.Parse(Level);
            int[] positionsArr = new int[22 + currentLevel];
            int[] colorsArr = new int[22 + currentLevel];
            Random rng = new Random();
            for (int i = 0; i < 22 + currentLevel; i++)
            {
                int position = 0;
                int color = 0;
                if (i >= currentLevel)
                {
                    int positionChance = rng.Next(0, 7);
                    if (positionChance == 0)
                    {
                        position = positionsArr[i - currentLevel];
                    }
                    else
                    {
                        position = rng.Next(0, 9);
                    }
                    int colorChance = rng.Next(0, 7);
                    if (colorChance == 0)
                    {
                        color = colorsArr[i - currentLevel];
                    }
                    else
                    {
                        color = rng.Next(0, 9);
                    }
                }
                else
                {
                    position = rng.Next(0, 9);
                    color = rng.Next(0, 9);
                }
                positionsArr[i] = position;
                colorsArr[i] = color;
                colors[position] = possibleColors[color];
                Colors = colors;
                await Task.Delay(2000);
                colors[position] = Color.LightBlue;
                Colors = colors;
                await Task.Delay(1000);
                if(i < currentLevel)
                {
                    if (positionPressed)
                        totalAnswers++;
                    if (colorPressed)
                        totalAnswers++;
                }
                else
                {
                    bool positionMatch = positionsArr[i - currentLevel] == positionsArr[i];
                    bool colorMatch = colorsArr[i - currentLevel] == colorsArr[i];
                    if(positionMatch)
                    {
                        totalAnswers++;
                        if (positionPressed)
                            correctAnswers++;
                    }
                    else
                    {
                        if (positionPressed)
                            totalAnswers++;
                    }
                    if(colorMatch)
                    {
                        totalAnswers++;
                        if (colorPressed)
                            correctAnswers++;
                    }
                    else
                    {
                        if (colorPressed)
                            totalAnswers++;
                    }
                }
                positionPressed = false;
                colorPressed = false;
            }
            double percentage = correctAnswers * 1.0 / totalAnswers * 100;
            string message = $"{percentage:N2}% correct answers.";
            if(percentage > 80)
            {
                GlobalVars.CurrentLevel++;
                message += $" You advanced to level {GlobalVars.CurrentLevel}.";
                NBackText = $"{GlobalVars.CurrentLevel}-back";
                if (GlobalVars.CurrentLevel > GlobalVars.BestScore)
                {
                    GlobalVars.BestScore = GlobalVars.CurrentLevel;
                    Preferences.Set("max_score", GlobalVars.BestScore);
                }
                Preferences.Set("current_level", GlobalVars.CurrentLevel);
                if (GlobalVars.RelegationStreak > 0)
                {
                    GlobalVars.RelegationStreak = 0;
                    Preferences.Set("relegation_streak", GlobalVars.RelegationStreak);
                }
            }
            else
            {
                if(percentage < 25)
                {
                    if(GlobalVars.CurrentLevel > GlobalVars.DEFAULT_SCORE)
                    {
                        if(GlobalVars.RelegationStreak == GlobalVars.MAX_STREAK)
                        {
                            GlobalVars.CurrentLevel--;
                            GlobalVars.RelegationStreak = 0;
                            message += $" You regressed to level {GlobalVars.CurrentLevel}.";
                            Preferences.Set("current_level", GlobalVars.CurrentLevel);
                        }
                        else
                        {
                            GlobalVars.RelegationStreak++;
                        }
                        Preferences.Set("relegation_streak", GlobalVars.RelegationStreak);
                    }
                }
                else
                {
                    if(GlobalVars.RelegationStreak > 0)
                    {
                        GlobalVars.RelegationStreak = 0;
                        Preferences.Set("relegation_streak", GlobalVars.RelegationStreak);
                    }
                }
            }
            await Shell.Current.DisplayAlert("Result", message, "Ok");
            Waiting = true;
            PlayStarted = false;
        }

        private void PositionFunc()
        {
            positionPressed = true;
        }

        private void ColorFunc()
        {
            colorPressed = true;
        }
    }
}
