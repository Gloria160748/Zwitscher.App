using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Zwitscher.Services.Notifications
{
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;
        void Initialize();
        void SendNotification(string title, string message, DateTime? notifyTime = null);
        void ReceiveNotification(string title, string message);
    }

    //public class NotificationService
    //{

    //    public static INotificationManager notificationManager;
    //    public static int notificationNumber = 0;

    //    public NotificationService()
    //    {
    //        notificationManager = DependencyService.Get<INotificationManager>();
    //        notificationManager.Initialize();
    //    }

    //    public void SendNotification(string title, string message, DateTime? notifyTime = null)
    //    {
    //        notificationManager.SendNotification(title, message, notifyTime);
    //    }

    //    public void ReceiveNotification(string title, string message)
    //    {
    //        notificationManager.ReceiveNotification(title, message);
    //    }
    //}

    public class NotificationService
    {
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
                ReturningData = "Dummy data",
                Android = new AndroidOptions
                {
                    VibrationPattern = new long[] { 1000, 1000, 1000 },
                },
            };

            LocalNotificationCenter.Current.Show(notification);
        }
    }
}
