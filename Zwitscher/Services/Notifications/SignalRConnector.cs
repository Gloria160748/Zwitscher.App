using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zwitscher.Services.Notifications
{
    // Diese Klasse ist für die Verbindung mit dem SignalR Server zuständig
    public class SignalRConnector
    {
        private readonly string connectionString = AppConfig.ApiUrl + "/userHub";
        private HubConnection hubConnection;
        private NotificationService notificationService = new NotificationService();
        public bool IsConnected = false;

        public SignalRConnector()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(connectionString)
                .WithAutomaticReconnect(new RetryPolicyLoop())
                .Build();

            hubConnection.On<string>("newComment", (message) =>
            {
                Console.WriteLine(message);
            });

 
        }

        public async void Connect()
        {

            notificationService.SendNotification("Try to connect SignalR");
            try
            {
                await hubConnection.StartAsync();
                IsConnected = true;
                await hubConnection.InvokeAsync("TestConnection", "Test");
                notificationService.SendNotification("SignalR is connected");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Delay(5000);
                Connect();
            }
        }

        public async void Disconnect()
        {
            try
            {
                await hubConnection.StopAsync();
                IsConnected = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class RetryPolicyLoop : IRetryPolicy
    {
        private const int DelayInSeconds = 5;

        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            return TimeSpan.FromSeconds(DelayInSeconds);
        }
    }
}
