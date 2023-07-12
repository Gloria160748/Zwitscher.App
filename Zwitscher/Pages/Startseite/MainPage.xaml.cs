using System;
using Xamarin.Forms;
using Zwitscher.Pages;
using Zwitscher.Services;

namespace Zwitscher
{
    public partial class MainPage : TabbedPage
    {
        private readonly AuthService authService = new AuthService();
        public MainPage()
        {
            InitializeComponent();
            Profilepicture.IconImageSource = AuthService.profilePicture;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (AuthService.activeUser != null && AuthService.activeUser.Success)
            {
                LoginButton.Text = "Logout";
                Profilepicture.IconImageSource = AuthService.profilePicture;
            }
            else
            {
                LoginButton.Text = "Login";
                Profilepicture.IconImageSource = AppConfig.ApiUrl + "/Media/" + AppConfig.pbPlaceholder;
            }
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

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            if (AuthService.activeUser != null && AuthService.activeUser.Success)
            {
                try
                {
                    await authService.Logout();
                    await Navigation.PopToRootAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alert", ex.Message, "OK");
                }
            }
            else
            {
                await Navigation.PushAsync(new Login());
            }
        }

        private void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Impressum());
        }
    }
}
