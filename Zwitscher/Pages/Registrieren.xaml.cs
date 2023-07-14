using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zwitscher.Services;

namespace Zwitscher.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registrieren : ContentPage
	{
		private AuthService authService = new AuthService();

		public Registrieren ()
		{
			InitializeComponent ();
		}

		// Login Label, um zur Login Seite zu gelangen
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

		// Registrieren Button, der bei erfolgreicher Registrierung zur Startseite weiterleitet
		private async void Button_Clicked(object sender, EventArgs e)
		{
            var user = await authService.Register(Nachname.Text, Vorname.Text, Geschlecht.SelectedIndex, Username.Text, Passwort.Text, Geburtsdatum.Date);
			if (user.Success)
			{
				await Navigation.PushAsync(new MainPage());
			}
			else
			{
				  await DisplayAlert("Ups..", "Der Nutzername ist bereits vergeben!", "OK");
			}
        }
    }
}