using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zwitscher.Services.Notifications;

namespace Zwitscher
{
    public partial class App : Application
    {
        public SignalRConnector SignalRConnector = new SignalRConnector();
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.LightBlue,
                BarTextColor = Color.White
            };


           
        }
            
        
        
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
