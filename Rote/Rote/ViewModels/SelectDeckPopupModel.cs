using Rg.Plugins.Popup.Services;
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
    public class SelectDeckPopupModel
    {
        DeckDB DeckDatabase;
        public ObservableCollection<Deck> Decks { get; set; }
        public ICommand DeckSettings { get; set; }
        public int SelectedDeck { get; set; }

        public SelectDeckPopupModel()
        {
            DeckDatabase = new DeckDB();
            Decks = DeckDatabase.GetDecks();
            for (var i = 0; i < 2; i++)
            {
                Decks.Add(Decks[i]);
            }
            for (var i = 0; i < 5; i++)
            {
                Decks.Add(Decks[i]);
            }
            DeckSettings = new Command(NavToDeckSettings);
        }

        private void NavToDeckSettings(object obj)
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NotificationSettingsPage(Decks[SelectedDeck]));
            PopupNavigation.PopAsync();
        }
    }
}
