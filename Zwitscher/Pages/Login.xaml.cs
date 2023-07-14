using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zwitscher.Services;

namespace Zwitscher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private AuthService authService;

        public Login()
        {
            authService = new AuthService();
            InitializeComponent();
        }

        // Login Button
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var user = await authService.Login(txtUsername.Text, txtPasswort.Text);


            if(user.Success == true)
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Ups..", "Username oder Passwort ist falsch!", "ok");
            }
        }

        // Registrieren Label
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.Registrieren());
        }

        
    }
}