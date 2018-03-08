using Rg.Plugins.Popup.Services;
using Rote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rote.ViewModels
{
    public class RenameViewModel : INotifyPropertyChanged
    {
        DeckDB DeckDatabase;
        CardDB CardDatabase;
        public event PropertyChangedEventHandler PropertyChanged;
        private Deck _Deck;
        public Deck Deck
        {
            get { return _Deck; }
            set { _Deck = value; OnPropertyChanged("_Deck"); }
        }
        public ICommand Rename { get; set; }
        public ICommand Cancel { get; set; }

        public RenameViewModel(Deck Deck)
        {
            DeckDatabase = new DeckDB();
            this.Deck = Deck;
            Rename = new Command(RenameDeck);
            Cancel = new Command(GoBack);
            
        }

        private void GoBack(object obj)
        {
            PopupNavigation.PopAsync();
        }

        void RenameDeck(object s)
        {
            var NewName = s as string;
            CardDatabase = new CardDB(Deck);
            CardDatabase.MigrateCards(Deck, NewName);
            Deck.Name = NewName;
            DeckDatabase.UpdateDeck(Deck);

            PopupNavigation.PopAsync();
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
