using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rote.Models
{
    [Table("NotificationSchedule")]
    public class NotificationSchedule
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int DeckID { get; set; }
        public int Time { get; set; } //In 24 hour time
        public int Game { get; set; }

        
        public Deck Deck;
        public enum GameType { Random, Multichoice, ByScore }

        
        public NotificationSchedule() { }
        

        public NotificationSchedule(Deck Deck, int Time, GameType GameType)
        {
            this.Deck = Deck;
            this.DeckID = Deck.ID;
            this.Time = Time;
            this.Game = (int)GameType;
        }
    }



    

}
