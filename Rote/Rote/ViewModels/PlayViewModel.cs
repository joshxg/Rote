using Rg.Plugins.Popup.Contracts;
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
    public class PlayViewModel : INotifyPropertyChanged
    {
        public INavigation navigation;
        DeckDB DeckDatabase;
        CardDB CardDatabase;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Card> Cards { get; set; }
        public ICommand RightAnswer { get; set; }
        public ICommand WrongAnswer { get; set; }
        public ICommand Flip { get; set; }
        public Deck Deck;
        Random Random;
        public int HandSize { get; private set; }

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

        

        public PlayViewModel(Deck deck)
        {
            Deck = deck;
            CardDatabase = new CardDB(deck);
            GetHand();
            this.navigation = navigation;
            RightAnswer = new Command(Right);
            WrongAnswer = new Command(Wrong);
            Flip = new Command(FlipIt);
        }

        public PlayViewModel(int DeckID)
        {
            DeckDatabase = new DeckDB();
            Deck = DeckDatabase.GetDeck(DeckID);
            CardDatabase = new CardDB(Deck);
            GetHand();
            this.navigation = navigation;
            RightAnswer = new Command(Right);
            WrongAnswer = new Command(Wrong);
            Flip = new Command(FlipIt);
        }

        private void FlipIt(object obj)
        {
            IsFlipped = !IsFlipped;
        }

        private void Wrong(object obj)
        {
            var cardLabel = obj as CardLabel;
            foreach(Card card in Cards)
            {
                if(card.ID == cardLabel.ID)
                {
                    CardDatabase.WrongAnswer(card);
                    break;
                }
            }

            if (Position < Settings.HandSize) { Position++; }
            else { NavToPopUpAsync(); }
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

            if (Position < Settings.HandSize) { Position++; }
            else { NavToPopUpAsync(); }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NavToPopUpAsync()
        {
            PopupNavigation.PushAsync(new PlayPopUp(Deck, 1));
            //navigation.PopAsync();
            Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }

        public void GetHand()
        {
            Random = new Random();
            CardDatabase = new CardDB(Deck);
            Cards = new ObservableCollection<Card>();

            var RandomNumber = 0;
            var CardDict = new Dictionary<int, ObservableCollection<Card>>
            {
                { 0, new ObservableCollection<Card>(CardDatabase.GetCardsWithScore(0) )},
                { 1, new ObservableCollection<Card>(CardDatabase.GetCardsWithScore(1) )},
                { 2, new ObservableCollection<Card>(CardDatabase.GetCardsWithScore(2) )},
                { 3, new ObservableCollection<Card>(CardDatabase.GetCardsWithScore(3) )}
            };
            HandSize = Math.Min(Settings.HandSize, CardDict[0].Count + CardDict[1].Count + CardDict[2].Count + CardDict[3].Count);
            int Ones = CardDict[1].Count > 0 ? 5 : 0;
            int Twos = CardDict[2].Count > 0 ? 3 + Ones : 0;
            int Three = CardDict[3].Count > 0 ? Ones > 0 ? Twos > 0 ? 1 + Twos : Ones + 1 : Twos > 0 ? Twos + 1 : 1 : 0;

            var OnesRange = Enumerable.Range(0, Ones);
            var TwosRange = Enumerable.Range(Ones, Twos);
            var ThreesRange = Enumerable.Range(Twos, Three);

            while (CardDict[0].Count > 0 && Cards.Count < HandSize)
            {
                RandomNumber = Random.Next(0, CardDict[0].Count);
                Cards.Add(CardDict[0][RandomNumber]);
                CardDict[0].RemoveAt(RandomNumber);
            }

            while (Cards.Count < HandSize)
            {
                RandomNumber = Random.Next(0, Three + 1);
                if (OnesRange.Contains(RandomNumber) && CardDict[1].Count > 0)
                {
                    RandomNumber = Random.Next(0, CardDict[1].Count);
                    Cards.Add(CardDict[1][RandomNumber]);
                    CardDict[1].RemoveAt(RandomNumber);
                }
                else if (TwosRange.Contains(RandomNumber) && CardDict[2].Count > 0)
                {
                    RandomNumber = Random.Next(0, CardDict[2].Count);
                    Cards.Add(CardDict[2][RandomNumber]);
                    CardDict[2].RemoveAt(RandomNumber);
                }
                else if (ThreesRange.Contains(RandomNumber) && CardDict[3].Count > 0)
                {
                    RandomNumber = Random.Next(0, CardDict[3].Count);
                    Cards.Add(CardDict[3][RandomNumber]);
                    CardDict[3].RemoveAt(RandomNumber);
                }
            }
        }

    }
}
                
