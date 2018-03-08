using Rote.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Rote.ViewModels
{
    public class NotificationSettingsViewModel
    {
        public ICommand Schedule { get; set; }
        

        public NotificationSettingsViewModel()
        {
            Schedule = new Command(ScheduleNotification);
        }

        private void ScheduleNotification(object obj)
        {
            DependencyService.Get<ILocalNotifications>().SendLocalNotification("TITLE", "CONTENT", 0);
        }
    }
}
