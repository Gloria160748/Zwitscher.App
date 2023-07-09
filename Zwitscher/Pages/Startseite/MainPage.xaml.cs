using System;
using Xamarin.Forms;
using Zwitscher.Services;

namespace Zwitscher
{
    public partial class MainPage : TabbedPage
    {
        AuthService authService = new AuthService();
        public MainPage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.Profil());
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {
            try
            {
                await authService.Logout();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
        }
    }
}
