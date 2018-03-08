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
        public void SendLocalNotification(string title, string desc, int iconID)
        {
            Intent AlarmIntent = new Intent(Application.Context, typeof(AlarmReceiver));
            Bundle values = new Bundle();
            values.PutString("title", "THIS IS THE TITLE");
            values.PutString("desc", "THIS IS THE DESCRIPTION");
            AlarmIntent.PutExtras(values);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, AlarmIntent, PendingIntentFlags.UpdateCurrent);

            AlarmManager alarmManager = Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 5 * 1000, pendingIntent);
        }
    }
}