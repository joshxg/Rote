using Rote.Models;
using Rote.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rote.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        INavigation navigation;
        public ICommand Select { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Setting { get; set; }
        public ICommand Reset { get; set; }
        public int Test
        {
            get => Settings.Test;
            set
            {
                if (Settings.Test == value)
                {
                    return;
                }
                Settings.Test = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            Select = new Command(NavToSelect);
            Edit = new Command(NavToEdit);
            Setting = new Command(NavToSettings);
            Reset = new Command(ResetDatabase);
        }

        private void ResetDatabase(object obj)
        {
            DeckDB.ClearDatabase();
            DeckDB.SetupTestDatabase();
        }

        void NavToSelect()
        {
            Application.Current.MainPage.Navigation.PushAsync(new SelectGamePage());
        }

        void NavToEdit()
        {
            Application.Current.MainPage.Navigation.PushAsync(new EditDeckPage());
        }

        void NavToSettings()
        {
            Application.Current.MainPage.Navigation.PushAsync(new SettingsPage());
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


        

