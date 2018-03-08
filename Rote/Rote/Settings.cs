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
        

    }
}
