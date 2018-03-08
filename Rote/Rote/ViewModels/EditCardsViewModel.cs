using Rote.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rote.ViewModels
{
    public class EditCardsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        CardDB CardDatabase;
        DeckDB DeckDatabase;
        Deck Deck;
        public ObservableCollection<Card> Cards { get; set; }
        public ICommand Add { get; set; }
        public ICommand Done { get; set; }
        public ICommand PageDisappearingCommand { get; set; }
        private int _Position;
        public int Position
        {
            get { return _Position; }
            set { _Position = value; OnPropertyChanged("Position"); }
        }

        public EditCardsViewModel(Deck deck)
        {
            this.Deck = deck;
            CardDatabase = new CardDB(deck);
            
            Add = new Command(AddCard);
            Done = new Command(DoneEdit);
            PageDisappearingCommand = new Command(PageDisappearing);

            Cards = CardDatabase.GetCards();
            if (Cards.Count == 0) { EmptyDeck(); }
        }

        private void PageDisappearing(object obj)
        {
            foreach(Card card in Cards)
            {
                CardDatabase.UpdateCard(card);
            }
        }

        private void EmptyDeck()
        {
            var Card = new Card(string.Empty, string.Empty, Deck.Name);
            CardDatabase.AddCard(Card);
            Cards.Add(Card);
        }

        private void DoneEdit(object obj)
        {
            Console.WriteLine(obj.ToString());
        }

        private void AddCard(object obj)
        {
            var card = new Card(string.Empty, string.Empty, Deck.Name);
            Cards.Add(card);
            Position = Cards.Count -1;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
