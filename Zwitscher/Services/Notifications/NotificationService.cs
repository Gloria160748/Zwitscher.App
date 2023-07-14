using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Zwitscher.Services.Notifications
{
    public class NotificationService
    {
        // Nach einigen Versuchen ist es gelungen auf einem Android Telefon Benachrichtigungen zu erhalten.
        // Die Benachrichtigungen konnten aber nicht in den Android Emulator übertragen werden, weshalb keine weiteren Funktionen implementiert wurden.
        // Eine komplette Funktionsübertragung auf die Hardware ist aus Zertifizierungsgründen während der Entwicklung nicht möglich gewesen.


        public NotificationService()
        {
        }

        public void SendNotification(string message, DateTime? notifyTime = null)
        {
            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                NotificationId = 1500,
                Title = "Notification",
                Description = message,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = notifyTime ?? DateTime.Now.AddSeconds(5)
                },
                Android = new AndroidOptions
                {
                    VibrationPattern = new long[] { 1000, 1000, 1000 },
                    Priority = AndroidPriority.High,
                },
            };

            LocalNotificationCenter.Current.Show(notification);
        }
    }
}
