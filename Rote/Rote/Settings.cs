using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rote
{
    public class Settings
    {

        private static ISettings AppSettings => CrossSettings.Current;
        public static int HandSize
            {
                get => AppSettings.GetValueOrDefault(nameof(HandSize), 5);
                set => AppSettings.AddOrUpdateValue(nameof(HandSize), value);
            }
        public static int NotificationStartTime
        {
            get => AppSettings.GetValueOrDefault(nameof(NotificationStartTime), 10);
            set => AppSettings.AddOrUpdateValue(nameof(NotificationStartTime), value);
        }
        public static int NotificationEndTime
        {
            get => AppSettings.GetValueOrDefault(nameof(NotificationEndTime), 6);
            set => AppSettings.AddOrUpdateValue(nameof(NotificationEndTime), value);
        }

        public static int NextNotification
        {
            get => AppSettings.GetValueOrDefault(nameof(NextNotification), -1);
            set => AppSettings.AddOrUpdateValue(nameof(NextNotification), value);
        }
        public static int Test
        {
            get => AppSettings.GetValueOrDefault(nameof(Test), 1);
            set => AppSettings.AddOrUpdateValue(nameof(Test), value);
        }


    }
}
