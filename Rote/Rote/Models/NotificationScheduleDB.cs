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
            lock (Locker)
            {
                Database.CreateTable<NotificationSchedule>();
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

        public NotificationSchedule GetScheduleByDeck(int DeckID)
        {
            ObservableCollection<NotificationSchedule> Schedules;
            lock (Locker)
            {
                Schedules = new ObservableCollection<NotificationSchedule>(Database.Query<NotificationSchedule>("SELECT * FROM NotificationSchedule WHERE DeckID = ?", DeckID));
            }

            return Schedules[0];
        }


    }
}
