using Rote.Models;
using Rote.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rote.ViewModels
{
    public class MainViewModel
    {
        INavigation navigation;
        public ICommand Select { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Settings { get; set; }
        public ICommand Reset { get; set; }

        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            Select = new Command(NavToSelect);
            Edit = new Command(NavToEdit);
            Settings = new Command(NavToSettings);
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
    }
}


        

