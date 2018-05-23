using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Rote.Models
{
    public class NotificationScheduleDB
    {
        public static readonly string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database3.db3");
        public static SQLiteConnection Database;
        ObservableCollection<NotificationSchedule> Schedule;
        object Locker;

        public NotificationScheduleDB()
        {
            Database = new SQLiteConnection(DatabasePath);
            Locker = new object();
            lock (Locker)
            {
                Database.CreateTable<NotificationSchedule>();
            }
        }

        public void RemoveSchedules(Deck deck)
        {
            var Schedules = GetSchedulesByDeck(deck);

            lock (Locker)
            {
                foreach(NotificationSchedule Schedule in Schedules)
                {
                    Database.Delete(Schedule);
                }
            }
        }

        public void UpdateSchedule(NotificationSchedule notificationSchedule)
        {
            lock (Locker)
            {
                Database.Update(notificationSchedule);
            }
        }

        public void AddSchedule(NotificationSchedule notificationSchedule)
        {
            lock (Locker)
            {
                Database.Insert(notificationSchedule);
            }
        }

        public ObservableCollection<NotificationSchedule> GetSchedules()
        {
            ObservableCollection<NotificationSchedule>Schedules;
            lock (Locker)
            {
                Schedules = new ObservableCollection<NotificationSchedule>(Database.Query<NotificationSchedule>("SELECT * FROM NotificationSchedule"));
            }

            return Schedules;
        }

        public ObservableCollection<NotificationSchedule> GetSchedulesByDeck(Deck deck)
        {
            ObservableCollection<NotificationSchedule> Schedules;
            lock (Locker)
            {
                Schedules = new ObservableCollection<NotificationSchedule>(Database.Query<NotificationSchedule>("SELECT * FROM NotificationSchedule WHERE DeckID = ?", deck.ID));
            }

            return Schedules;
        }

        public ObservableCollection<NotificationSchedule> GetNextNotifications()
        {
            ObservableCollection<NotificationSchedule> Schedules;
            var CurrentHour = DateTime.Now.Hour;
            lock (Locker)
            {
                Schedules = new ObservableCollection<NotificationSchedule>(Database.Query<NotificationSchedule>("SELECT * FROM NotificationSchedule WHERE Time = ?", CurrentHour));
            }

            var DeckDB = new DeckDB();
            foreach(NotificationSchedule ScheduledNotification in Schedules)
            {
                ScheduledNotification.Deck = DeckDB.GetDeck(ScheduledNotification.DeckID);
            }
            return Schedules;
        }

        public ObservableCollection<NotificationSchedule> GetFutureNotifications()
        {
            ObservableCollection<NotificationSchedule> Schedules;
            var FutureHour = (DateTime.Now.Hour + 1) % 24;
            lock (Locker)
            {
                do
                {
                    Schedules = new ObservableCollection<NotificationSchedule>(Database.Query<NotificationSchedule>("SELECT * FROM NotificationSchedule WHERE Time = ?", FutureHour));
                    FutureHour = (FutureHour + 1) % 24;
                }
                while (Schedules.Count < 1);

                
            }

            var DeckDB = new DeckDB();
            foreach (NotificationSchedule ScheduledNotification in Schedules)
            {
                ScheduledNotification.Deck = DeckDB.GetDeck(ScheduledNotification.DeckID);
            }
            return Schedules;
        }

        public void SetNextNotificationTime()
        {
            var CurrentTime = DateTime.Now.Hour;
            ObservableCollection<NotificationSchedule> Time =  new ObservableCollection<NotificationSchedule>();

            lock (Locker)
            {
                do
                {
                    if (CurrentTime >= Settings.NotificationStartTime && CurrentTime <= Settings.NotificationEndTime)
                    {
                        Time = new ObservableCollection<NotificationSchedule>(Database.Query<NotificationSchedule>("SELECT Time FROM NotificationSchedule WHERE Time = ?", CurrentTime));
                    }

                    CurrentTime = (CurrentTime + 1) % 24;
                }
                while (Time.Count < 1);
                
            }

            Settings.NextNotification = Time[0].Time;
        }
    }
}


