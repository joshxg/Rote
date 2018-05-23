using Rg.Plugins.Popup.Services;
using Rote.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rote.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ICommand IncreaseHandSize;
        ICommand DecreaseHandSize;
        ICommand Set;
        public ICommand SelectDeck { get; set; }

        public int StartTime
        {
            get => Settings.NotificationStartTime;
            set
            {
                SetStartTime(value);
                OnPropertyChanged();
            }
        }

        public int EndTime
        {
            get => Settings.NotificationEndTime;
            set
            {
                SetEndTime(value);
                OnPropertyChanged();
            }
        }

        public string StartTimeString
        {
            get
            {
                return TimeConverter(StartTime);
            }
        }

        public string EndTimeString
        {
            get
            {
                return TimeConverter(EndTime);
            }
        }

        public int HandSize
        {
            get => Settings.HandSize;
            set
            {
                if (Settings.HandSize == value)
                {
                    return;
                }

                Settings.HandSize = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            SelectDeck = new Command(NavToSelectDeck);
        }

        private void NavToSelectDeck(object obj)
        {
            //Application.Current.MainPage.Navigation.PushAsync(new Views.SelectDeckPage(4));
            PopupNavigation.PushAsync(new SelectDeckPopup());
        }

        private string TimeConverter(int Time)
        {
            if(Time > 12)
            {
                return String.Format("{0}:00 pm", Time - 12);
            }
            
            else if(Time == 0 || Time == 24)
            {
                return "12:00 am";
            }

            else if(Time == 12)
            {
                return "12:00 pm";
            }
            else
            {
                return String.Format("{0}:00 am", Time);
            }


        }

        private void SetStartTime(int Time)
        {
            if(Time < EndTime & Time > 0)
            {
                Settings.NotificationStartTime = Time;
            }

            else if(Time == 0)
            {
                Settings.NotificationStartTime = 0;
            }

            else
            {
                Settings.NotificationStartTime = Settings.NotificationEndTime - 1;
            }
        }

        private void SetEndTime(int Time)
        {
            if (Time > StartTime & Time < 25)
            {
                Settings.NotificationEndTime = Time;
            }

            else
            {
                Settings.NotificationEndTime = Settings.NotificationStartTime + 1;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
