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
        ICommand Increase;
        ICommand Decrease;
        ICommand Set;
        ICommand NotificationSettings;
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
            NotificationSettings = new Command(NavToNotificationSettings);
            Increase = new Command(IncreaseSize);
            Decrease = new Command(DecreaseSize);
            Set = new Command(SetSize);
        }

        private void NavToNotificationSettings(object obj)
        {
            throw new NotImplementedException();
        }

        private void SetSize(object obj)
        {
            throw new NotImplementedException();
        }

        private void IncreaseSize(object obj)
        {
            HandSize++;
        }

        private void DecreaseSize(object obj)
        {
            HandSize--;
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
