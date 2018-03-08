using System;
using System.Collections.Generic;
using System.Text;

namespace Rote.Services
{
    public interface ILocalNotifications
    {
        void SendLocalNotification(string title, string desc, int iconID);
    }
}
