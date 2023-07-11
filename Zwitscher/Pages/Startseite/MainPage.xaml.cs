using System;
using Xamarin.Forms;
using Zwitscher.Pages;
using Zwitscher.Services;

namespace Zwitscher
{
    public partial class MainPage : TabbedPage
    {
        AuthService authService = new AuthService();
        public MainPage()
        {
            InitializeComponent();
            Profilepicture.IconImageSource = AuthService.profilePicture;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var activeUser = await authService.GetActiveUser();
            if (string.IsNullOrEmpty(activeUser.userID))
            {
                await Navigation.PushAsync(new Login());
            }
            else
            {
                await Navigation.PushAsync(new Profil(activeUser.userID));
            }
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

        private void ToolbarItem_Clicked_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Impressum());
        }
    }
}
