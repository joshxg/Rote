using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using Android.Support.V4.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Rote.Models;

namespace Rote.Droid
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {

        NotificationScheduleDB NotificationDB;

        public override void OnReceive(Context context, Intent intent)
        {
            NotificationDB = new NotificationScheduleDB();
            var ScheduledNotifications = NotificationDB.GetNextNotifications();

            foreach (NotificationSchedule ScheduledNotification in ScheduledNotifications)
            {
                Bundle Data = GetDataBundle(ScheduledNotification);
                var NotificationIntent = GetLaunchIntent(Data);
                PendingIntent ContentIntent = PendingIntent.GetActivity(Application.Context, 0, NotificationIntent, PendingIntentFlags.UpdateCurrent);
                var Notification = GetNotification(ScheduledNotification, ContentIntent);
                var NotificationManager = NotificationManagerCompat.From(Application.Context);
                NotificationManager.Notify(ScheduledNotification.ID, Notification);
            }
        }

        private Bundle GetDataBundle(NotificationSchedule ScheduledNotification)
        {
            Bundle Data = new Bundle();
            var DeckName = ScheduledNotification.Deck.Name;
            var DeckID = ScheduledNotification.DeckID;
            var Game = ScheduledNotification.Game;
            Data.PutString("DeckName", DeckName);
            Data.PutInt("ID", DeckID);
            Data.PutInt("Game", Game);
            return Data;
        } 

        private Intent GetLaunchIntent(Bundle Data)
        {
            var NotificationIntent = Application.Context.PackageManager.GetLaunchIntentForPackage(Application.Context.PackageName);
            NotificationIntent.PutExtras(Data);
            NotificationIntent.SetFlags(ActivityFlags.SingleTop);
            return NotificationIntent;
        }

        private Notification GetNotification(NotificationSchedule ScheduledNotification, PendingIntent ContentIntent)
        {
            Notification.Builder builder = new Notification.Builder(Application.Context)
                .SetContentTitle("Time to play!")
                .SetContentIntent(ContentIntent)
                .SetContentText(String.Format("Smash some {0} cards", ScheduledNotification.Deck.Name))
                .SetSmallIcon(Resource.Drawable.icon)
                .SetAutoCancel(true);
            return builder.Build();
        }

        private void SetNextAlarm()
        {
            NotificationDB.SetNextNotificationTime();
            var Notif = new LocalNotificationAndroid();
            Notif.SendLocalNotification();
        }








    }
}

