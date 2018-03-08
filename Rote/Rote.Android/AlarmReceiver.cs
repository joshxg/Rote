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

namespace Rote.Droid
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var title = intent.GetStringExtra("title");
            var description = intent.GetStringExtra("desc");

            var NotificationIntent = Application.Context.PackageManager.GetLaunchIntentForPackage(Application.Context.PackageName);

            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(Application.Context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(NotificationIntent);
            PendingIntent ContentIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.OneShot);

            var NotificationManager = NotificationManagerCompat.From(Application.Context);

            Notification.Builder builder = new Notification.Builder(Application.Context)
                .SetContentTitle(title)
                .SetContentIntent(ContentIntent)
                .SetContentText(description)
                .SetSmallIcon(Resource.Drawable.icon)
                .SetAutoCancel(true);

            Notification notification = builder.Build();
            NotificationManager.Notify(0, notification);
        }
    }
}

                


