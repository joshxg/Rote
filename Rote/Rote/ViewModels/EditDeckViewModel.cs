using Rg.Plugins.Popup.Services;
using Rote.Controls;
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
    public class EditDeckViewModel : INotifyPropertyChanged
    {
        INavigation navigation;
        DeckDB DeckDatabase;
        public ObservableCollection<Deck> Decks { get; set; }
        public ICommand Remove { get; set; }
        public ICommand Rename { get; set; }
        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }

        private bool _Visible;
        public Deck temp;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; OnPropertyChanged("Visible"); }
        }

        public EditDeckViewModel(INavigation navigation)
        {
            DeckDatabase = new DeckDB();
            Decks = DeckDatabase.GetDecks();
            Remove = new Command(RemoveDeck);
            Rename = new Command(RenameDeck);
            Add = new Command(AddDeck);
            Edit = new Command(EditCards);
            Visible = true;
            this.navigation = navigation;
        }


        private void EditCards(object obj)
        {
            var deck = obj as Deck;
            navigation.PushAsync(new EditCardsPage(deck));
        }

        private void AddDeck(object obj)
        {
            PopupNavigation.PushAsync(new PopupNewDeck(Decks));
        }

        private void RemoveDeck(object obj)
        {
            var deck = obj as Deck;
            Decks.Remove(deck);
            DeckDatabase.DeleteDeck(deck);
        }
            
        private void RenameDeck(object obj)
        {
            var Deck = obj as Deck;
            foreach (Deck deck in Decks)
            {
                if(deck == Deck)
                {
                    PopupNavigation.PushAsync(new RenamePopup(deck));
                    break;
                }
            }
            
        }
            
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
            




