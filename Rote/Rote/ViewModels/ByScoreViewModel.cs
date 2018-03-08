using Rg.Plugins.Popup.Services;
using Rote.Controls;
using Rote.Models;
using Rote.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rote.ViewModels
{
    public class ByScoreViewModel : INotifyPropertyChanged
    {
        DeckDB DeckDatabase;
        CardDB CardDatabase;
        Deck Deck;
        Random Random;
        int HandSize;
        public int ScheduledScore { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Card> Cards { get; set; }
        public ICommand RightAnswer { get; set; }
        public ICommand WrongAnswer { get; set; }
        public ICommand Flip { get; set; }
        private int _Position;
        public int Position
        {
            get { return _Position; }
            set { _Position = value; OnPropertyChanged("Position"); }
        }

        private bool _IsFlipped;
        public bool IsFlipped
        {
            get { return _IsFlipped; }
            set { _IsFlipped = value; OnPropertyChanged("IsFlipped"); }
        }

        public ByScoreViewModel(Deck Deck)
        {
            this.Deck = Deck;
            CardDatabase = new CardDB(Deck);
            DeckDatabase = new DeckDB();
            HandSize = Settings.HandSize;
            RightAnswer = new Command(Right);
            WrongAnswer = new Command(Wrong);
            Flip = new Command(FlipIt);
            Random = new Random();
            GetCards();
        }

        public void GetCards()
        {
            ScheduledScore = DeckDatabase.GetScheduled(Deck);
            Cards = CardDatabase.GetCardsWithScore(0);
            if(Cards.Count < 5) { Cards = new ObservableCollection<Card>(Cards.Concat(CardDatabase.GetCardsWithScore(ScheduledScore))); }
            if (Cards.Count >= 5) { Cards = new ObservableCollection<Card>(Cards.Take(5));  }
            
            var x = CardDatabase.GetCards();
            if(Cards.Count < HandSize)
            {
                FillDeck();
            }

        }

        public void FillDeck()
        {
            int[] Scores = { 1, 2, 3 };

            for (var i = 0; i < Scores.Length; i++)
            {

                if (Scores[i] == ScheduledScore) { continue; }

                var TempCards = CardDatabase.GetCardsWithScore(Scores[i]);

                while (TempCards.Count > 0 && Cards.Count < HandSize)
                {
                    var RandomNumber = Random.Next(0, TempCards.Count);
                    {
                        Cards.Add(TempCards[RandomNumber]);
                        TempCards.RemoveAt(RandomNumber);
                    }
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NavToPopUpAsync()
        {
            DeckDatabase.ScheduledRunDone(Deck);
            PopupNavigation.PushAsync(new PlayPopUp(Deck, 3));
            Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }

        public void Right(object obj)
        {
            var cardLabel = obj as CardLabel;
            foreach (Card card in Cards)
            {
                if (card.ID == cardLabel.ID)
                {
                    CardDatabase.RightAnswer(card);
                    break;
                }
            }

            if (Position < Settings.HandSize - 1) { Position++; }
            else { NavToPopUpAsync(); }
        }

        private void Wrong(object obj)
        {
            var cardLabel = obj as CardLabel;
            foreach (Card card in Cards)
            {
                if (card.ID == cardLabel.ID)
                {
                    CardDatabase.WrongAnswer(card);
                    break;
                }
            }

            if (Position < Settings.HandSize -1) { Position++; }
            else { NavToPopUpAsync(); }
        }

        private void FlipIt(object obj)
        {
            IsFlipped = !IsFlipped;
        }
    }
}
