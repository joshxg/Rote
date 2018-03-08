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
        public int Interval { get; set; } //In hours
        public int SleepStart { get; set; } //Starting hour of no notifications
        public int SleepEnd { get; set; } //Ending hour of no notifications
        public int SleepPeriod { get; set; }
        public int Game;

        [Ignore]
        public Deck Deck { get; set; }

        public enum GameType { Random, Multichoice, ByScore }
        
        public NotificationSchedule() { }
        

        public NotificationSchedule(Deck Deck, int SleepStart, int SleepEnd, int Interval, GameType GameType)
        {
            this.Deck = Deck;
            this.SleepStart = SleepStart;
            this.SleepEnd = SleepEnd;
            this.Interval = Interval;
            this.Game = (int)GameType;
            this.SleepPeriod = SleepEnd - SleepStart;
        }
    }



    

}
