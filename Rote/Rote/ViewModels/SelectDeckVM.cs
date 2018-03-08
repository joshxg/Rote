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
    public class SelectDeckVM : INotifyPropertyChanged
    {
        public int Type;
        public INavigation navigation;
        public ICommand ToPlayPage { get; set; }
        private ObservableCollection<Deck> _Decks;
        public ObservableCollection<Deck> Decks
        {
            get { return _Decks; }
            set { _Decks = value; OnPropertyChanged(); }
        }
        public DeckDB DeckDatabase;
        public event PropertyChangedEventHandler PropertyChanged;

        public SelectDeckVM(int Type)
        {
            this.Type = Type;
            DeckDatabase = new DeckDB();
            Decks = DeckDatabase.GetDecks();
            ToPlayPage = new Command(NavToPlayPage);
        }
            

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void NavToPlayPage(object s)
        {
            var deckLabel = s as DeckLabel;
            var deck = DeckDatabase.GetDeck(deckLabel.ID);
            if (Type == 1)
            {
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PlayPage(deck));
            }
            else if (Type == 2)
            {
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new MultiChoicePage(deck));
            }
            else if(Type == 3)
            {
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ByScorePage(deck));
            }
        }
    }
}