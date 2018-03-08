using Rg.Plugins.Popup.Services;
using Rote.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rote.ViewModels
{
    public class ViewModelNewDeck
    {
        DeckDB DeckDatabase;
        CardDB CardDatabase;
        ObservableCollection<Deck> Decks;
        public ICommand Save { get; set; }
        public ICommand Cancel { get; set; }

        public ViewModelNewDeck(ObservableCollection<Deck> Decks)
        {
            this.Decks = Decks;
            Save = new Command(SaveName);
            Cancel = new Command(CancelAdd);
            DeckDatabase = new DeckDB();
        }

        private void CancelAdd(object obj)
        {
            PopupNavigation.PopAsync();
        }

        private void SaveName(object obj)
        {
            var DeckName = obj as string;
            Deck NewDeck = new Deck(DeckName);
            DeckDatabase.AddDeck(NewDeck);
            Decks.Add(NewDeck);
            PopupNavigation.PopAsync();
        }   
    }
}
