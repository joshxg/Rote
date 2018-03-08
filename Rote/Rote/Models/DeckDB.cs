using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Rote.Models
{
    public class DeckDB
    {
        public static readonly string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database3.db3");
        public static SQLiteConnection Database;
        public ObservableCollection<Deck> Decks;
        public Deck _Deck;
        public string DeckName;
        static object locker = new object();

        public DeckDB()
        {

            Database = new SQLiteConnection(DatabasePath);
            lock (locker)
            {
                Decks = GetDecks();
            }
        }

        public void AddDeck(Deck Deck)
        {
            Database.Insert(Deck);
        }
            

        public ObservableCollection<Deck> GetDecks()
        {
            lock (locker)
            {
                return new ObservableCollection<Deck>(Database.Query<Deck>("SELECT * FROM Deck"));
            }
        }

        public void UpdateDeck(Deck deck)
        {
            lock (locker)
            {
                Database.Update(deck);
            }
        }

        public void DeleteDeck(Deck deck)
        {
            lock (locker)
            {
                Database.Delete(deck);
            }
        }

        public Deck GetDeck(int id)
        {
           return Database.Get<Deck>(id);
        }

        public static void ClearDatabase()
        {
            Database = new SQLiteConnection(DatabasePath);
            Database.DropTable<Card>();
            Database.DropTable<Deck>();
            Database.Close();
        }


        public int GetScheduled(Deck Deck)
        {
            Deck.Schedule = new ObservableCollection<int>
            {
                1,1,2,1,2,3
            };
            UpdateDeck(Deck);
            return Deck.Schedule[Deck.ScheduleIndex];
            
        }

        public void ScheduledRunDone(Deck Deck)
        {
            Deck.ScheduleIndex = (Deck.ScheduleIndex + 1) % 6;
            UpdateDeck(Deck);
        }

        public static void SetupTestDatabase()
        {
            Database = new SQLiteConnection(DatabasePath);
            Database.CreateTable<Deck>();
            Database.CreateTable<Card>();

            Database.Insert(new Deck("Deck One"));
            Database.Insert(new Deck("Deck Two"));
            Database.Insert(new Deck("Deck Three"));

            for (var i = 0; i < 10; i++)
            {
                Database.Insert(new Card("Deck One - Question " + i, "Deck One - Answer " + i, "Deck One"));
            }

            for (var i = 0; i < 10; i++)
            {
                Database.Insert(new Card("Deck Two - Question " + i, "Deck Two - Answer " + i, "Deck Two"));
            }

            for (var i = 0; i < 10; i++)
            {
                Database.Insert(new Card("Deck Three - Question " + i, "Deck Three - Answer " + i, "Deck Three"));
            }
            Database.Close();
        }
    }
}
