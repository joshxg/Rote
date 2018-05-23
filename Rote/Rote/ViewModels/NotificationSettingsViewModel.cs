using Rote.Models;
using Rote.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static Rote.Models.NotificationSchedule;

namespace Rote.ViewModels
{
    public class NotificationSettingsViewModel
    {
        public ICommand Schedule { get; set; }
        public ObservableCollection<string> GamesList { get; set; }
        public int Interval { get; set; }
        public int GameSelection { get; set; }
        NotificationScheduleDB NotificationScheduleDB { get; set; }
        Deck Deck;

        public NotificationSettingsViewModel(Deck Deck)
        {
            this.Deck = Deck;
            GamesList = new ObservableCollection<string>
            {
                "Random",
                "Multichoice",
                "By Score"
            };

            Schedule = new Command(ScheduleNotification);
        }



        private void ScheduleNotification(object obj)
        {
            NotificationScheduleDB = new NotificationScheduleDB();
            ScheduleNotifications();
            NotificationScheduleDB.SetNextNotificationTime();
            DependencyService.Get<ILocalNotifications>().SendLocalNotification();
        }

        private void ScheduleNotifications()
        {
            NotificationScheduleDB.RemoveSchedules(Deck);
            var ScheduleTime = Settings.NotificationStartTime;
            while(ScheduleTime < 24)
            {
                NotificationScheduleDB.AddSchedule(new NotificationSchedule(Deck, ScheduleTime, (GameType)GameSelection + 1));
                ScheduleTime += Interval;
            }
        }

        private void CreateNotifications()
        {
            var TimeNow = DateTime.Now.Hour;
            var CurrentTime = DateTime.Now.Hour;
            int ScheduleTime = -1;
            NotificationScheduleDB.RemoveSchedules(Deck);

            do
            {
                CurrentTime = (CurrentTime + Interval) % 24;

                if (CurrentTime >= Settings.NotificationStartTime && CurrentTime <= Settings.NotificationEndTime)
                {
                    var Notification = new NotificationSchedule(Deck, CurrentTime, (GameType)GameSelection+1);
                    NotificationScheduleDB.AddSchedule(Notification);
                    if (ScheduleTime < 0)
                    {
                        ScheduleTime = CurrentTime;
                    }
                }

            } while (CurrentTime != CurrentTime);

            ScheduleAlarm(ScheduleTime);
        }

        private void ScheduleAlarm(int ScheduleTime)
        {
            NotificationScheduleDB.SetNextNotificationTime();
            DependencyService.Get<ILocalNotifications>().SendLocalNotification();
        }
    }
}


