using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Rote.Models
{
    public class CardDB
    {
        public static readonly string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database3.db3");
        public static SQLiteConnection Database;
        public ObservableCollection<Card> Cards;
        public Deck _Deck;
        public string DeckName;
        static object locker = new object();

        public CardDB(Deck deck)
        {
            Database = new SQLiteConnection(DatabasePath);
            DeckName = deck.Name;
            _Deck = deck;
        }

        public CardDB(string DeckName)
        {
            Database = new SQLiteConnection(DatabasePath);
            lock (locker)
            {
                _Deck = Database.Table<Deck>().Where(t => t.Name == DeckName).FirstOrDefault();
            }
            DeckName = _Deck.Name;
        }

        public void AddCard(Card card)
        {
            lock (locker)
            {
                Database.Insert(card);
            }
            
        }

        public ObservableCollection<Card> GetCards()
        {
            lock (locker)
            {
                return new ObservableCollection<Card>(Database.Query<Card>("SELECT * FROM Card WHERE DeckName = ?", DeckName));
            }
        }

        public void UpdateCard(Card card)
        {
            if (card.Question == string.Empty || card.Answer == string.Empty)
            {
                DeleteCard(card);
            }
            else
            {
                lock (locker)
                {
                    Database.InsertOrReplace(card);
                }
            }

        }

        public ObservableCollection<Card> GetCardsWithScore(int Score)
        {
            lock (locker)
            {
                var x = new ObservableCollection<Card>(Database.Query<Card>("SELECT * FROM Card WHERE Score = ? AND DeckName = ?", Score, _Deck.Name));
                return x;

            }
            
        }

        public void MigrateCards(Deck deck, string NewName)
        {
            var Cards = Database.Query<Card>("SELECT * FROM Card WHERE DeckName = ?", DeckName);
            foreach (Card card in Cards)
            {
                card.DeckName = NewName;
                Database.Update(card);
            }
        }

        public void DeleteCard(Card card)
        {
            lock (locker)
            {
                Database.Delete(card);
            }
        }

        public void RightAnswer(Card Card)
        {
            if (Card.Score < 3)
            {
                Card.Score++;
            }
            UpdateCard(Card);
        }

        public void WrongAnswer(Card Card)
        {
            Card.Score = 0;
            UpdateCard(Card);
        }

    }
}