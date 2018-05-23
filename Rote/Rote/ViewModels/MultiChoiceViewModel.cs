using Rg.Plugins.Popup.Services;
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
    public class MultiChoiceViewModel : INotifyPropertyChanged
    {
        Deck Deck;
        public event PropertyChangedEventHandler PropertyChanged;
        DeckDB DeckDatabase;
        CardDB CardDatabase;
        public ObservableCollection<Card> Cards { get; set; }
        public ObservableCollection<Card> Hand { get; set; } 
        public ObservableCollection<AnswerCard> Answers { get; set; }
        public ICommand Pick { get; set; }
        public ICommand Pick2 { get; set; }
        Random Random;
        int RandomNumber;
        int index;
        int HandSize;
        private int _Position;
        public int Position
        {
            get { return _Position; }
            set { _Position = value; OnPropertyChanged("Position"); }
        }
        public MultiChoiceViewModel(Deck Deck)
        {
            this.Deck = Deck;
            CardDatabase = new CardDB(Deck);
            Cards = CardDatabase.GetCards();
            GetHand();
            Answers = new ObservableCollection<AnswerCard>();
            Pick = new Command(Picked);
            Position = 0;
            index = 0;
            Setup();
        }

        public MultiChoiceViewModel(int DeckID)
        {
            DeckDatabase = new DeckDB();
            this.Deck = DeckDatabase.GetDeck(DeckID);
            CardDatabase = new CardDB(Deck);
            Cards = CardDatabase.GetCards();
            GetHand();
            Answers = new ObservableCollection<AnswerCard>();
            Pick = new Command(Picked);
            Position = 0;
            index = 0;
            Setup();
        }
        /*Create the Answers list of AnswerCards, which the listview binds to. Generates four AnswerCards, with a Card and Correct properties, indicating if
        this is the right answer. Each loop checks if the random card from the full list of cards of the deck is the correct choice, from the randomly
        chosen card in the hand shown in the carousel view (The GetHand method is generalized so each game can use it, making it a little harder to 
        then also generate AnswerCards). Then that card is removed from Cards so there are no duplicates (This means we have to call the DB for a full 
        list of cards each time..). Then finally, checks to see if the actual answer has been one of the randomly selected, if not it adds it in a random
        Position. Maybe it would be better to add the actual answer first, but it needs to be in a random position, so the Answers list needs to be populated
        before that.*/
        private void Setup()
        {
            Answers.Clear();
            RandomNumber = Random.Next(0, Cards.Count);
            Cards = CardDatabase.GetCards();
            var NotYet = true;
            for (var i = 0; i < 4; i++)
            {
                RandomNumber = Random.Next(0, Cards.Count);
                var Card = Cards[RandomNumber];
                if (!Card.Answer.Equals(Hand[index].Answer))
                {
                    Answers.Add(new AnswerCard(Card, false));
                    Cards.Remove(Card);
                }
                else
                {
                    Answers.Add(new AnswerCard(Card, true));
                    Cards.Remove(Card);
                    NotYet = false;
                }
            }

            if (NotYet)
            {
                RandomNumber = Random.Next(0, Answers.Count);
                Answers[RandomNumber] = new AnswerCard(Hand[index], true);
            }
        }

        /*Checking if the answer picked in the listview is the correct one. If so, alter score accordingly, increment which card we are using
         and either setup the new answers or nav to the popup*/
        private void Picked(object obj)
        {
            var AnswerCard = obj as AnswerCard;
            if (AnswerCard.Correct)
            {
                CardDatabase.RightAnswer(AnswerCard.Card);
            }
            else
            {
                CardDatabase.WrongAnswer(AnswerCard.Card);
            }

            index++;

            if (Position < HandSize - 1)
            {
                Position++;
                Setup();
            }

            else
            {
                Nav(Deck, 2);
            }
        }

        /*Generate a random hand from the deck selected. Hand size is altered in the settings. If there are any new cards added, 
         *populate the hand with these first, then, 1/2 chance of a card with score = 1, 1/3 chance of a card with score = 2 and 
         *a 1/6 chance of a card with score = 3.*/
        public void GetHand()
        {
            Random = new Random();
            CardDatabase = new CardDB(Deck);
            Hand = new ObservableCollection<Card>();

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

            while (CardDict[0].Count > 0 && Hand.Count < HandSize)
            {
                RandomNumber = Random.Next(0, CardDict[0].Count);
                Hand.Add(CardDict[0][RandomNumber]);
                CardDict[0].RemoveAt(RandomNumber);
            }

            while (Hand.Count < HandSize)
            {
                RandomNumber = Random.Next(0, Three + 1);
                if (OnesRange.Contains(RandomNumber) && CardDict[1].Count > 0)
                {
                    RandomNumber = Random.Next(0, CardDict[1].Count);
                    Hand.Add(CardDict[1][RandomNumber]);
                    CardDict[1].RemoveAt(RandomNumber);
                }
                else if (TwosRange.Contains(RandomNumber) && CardDict[2].Count > 0)
                {
                    RandomNumber = Random.Next(0, CardDict[2].Count);
                    Hand.Add(CardDict[2][RandomNumber]);
                    CardDict[2].RemoveAt(RandomNumber);
                }
                else if (ThreesRange.Contains(RandomNumber) && CardDict[3].Count > 0)
                {
                    RandomNumber = Random.Next(0, CardDict[3].Count);
                    Hand.Add(CardDict[3][RandomNumber]);
                    CardDict[3].RemoveAt(RandomNumber);
                }
            }
        }

        private void Nav(Deck Deck, int Type)
        {
            PopupNavigation.PushAsync(new PlayPopUp(Deck, 2));
            Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
