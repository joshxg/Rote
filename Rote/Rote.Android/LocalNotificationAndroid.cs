using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Rote.Droid;
using Rote.Services;

[assembly: Xamarin.Forms.Dependency(typeof(LocalNotificationAndroid))]
namespace Rote.Droid
{
    public class LocalNotificationAndroid : ILocalNotifications
    {
        public void SendLocalNotification()
        {
            var sendAt = (Settings.NextNotification - DateTime.Now.Hour) % 24;
            Intent AlarmIntent = new Intent(Application.Context, typeof(AlarmReceiver));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, AlarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + sendAt * 1000, pendingIntent);
        }
    }
}