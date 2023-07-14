using Xamarin.Forms;
using Zwitscher.Services.Notifications;

namespace Zwitscher
{
    public partial class App : Application
    {
        public SignalRConnector signalRConnector = new SignalRConnector();
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
            signalRConnector.Connect();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
