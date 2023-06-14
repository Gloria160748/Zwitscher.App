using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zwitscher
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.FromHex("#27408b")
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
