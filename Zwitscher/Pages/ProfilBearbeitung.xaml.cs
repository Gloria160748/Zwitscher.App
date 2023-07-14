using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zwitscher.Models;
using Zwitscher.Services;
using System.Globalization;

namespace Zwitscher.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilBearbeitung : ContentPage
    {
        private readonly User User;
        private IFormFile profilePicture;
        private readonly UserService userService = new UserService();
        private readonly AuthService authService = new AuthService();

        public ProfilBearbeitung(User user)
        {
            User = user;
            InitializeComponent();

            // Setze die Werte der Textfelder auf die Werte des Users
            Nachname.Text = user.lastname ?? "";
            Vorname.Text = user.firstname ?? "";
            Username.Text = user.username ?? "";
            Biographíe.Text = user.biography ?? "";
            Geburtsdatum.Date = DateTime.ParseExact(user.birthday, "dd.MM.yyyy", CultureInfo.CurrentCulture);
            ProfilePicture.Source = user.pbFileName;

            if (user.gender == "Männlich")
            {
                Geschlecht.SelectedIndex = 0;
            }
            else if (user.gender == "Weiblich")
            {
                Geschlecht.SelectedIndex = 1;
            }
            else
            {
                Geschlecht.SelectedIndex = 2;
            }
        }

        // Speichert die Änderungen am User ab. Dabei wird das Profilbild nur gespeichert, wenn es geändert wurde.
        // Das Passwort wird, falls es leer ist, nicht geändert und als leeren String übergeben. Anschließend wird die Seite geschlossen.
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var user = new User
            {
                userID = User.userID,
                lastname = Nachname.Text,
                firstname = Vorname.Text,
                username = Username.Text,
                birthday = Geburtsdatum.Date.ToString(),
                biography = Biographíe.Text,
                gender = Geschlecht.SelectedIndex.ToString(),

                password = Passwort.Text ?? ""
            };

            try
            {
                await userService.EditUser(user);
                if (profilePicture != null)
                    await userService.AddProfilePicture(user.userID, profilePicture);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ups..", ex.Message, "OK");
            }

            Navigation.RemovePage(this);
        }

        // Öffnet den Filepicker und speichert das ausgewählte Bild in der Variable profilePicture
        // Anschließend wird das Bild in der App angezeigt
        private async void UploadButton_Clicked(object sender, EventArgs e)
        {
            profilePicture = await MediaConverter.SelectImage();
            ProfilePicture.Source = profilePicture.FileName;
        }

        // Löscht das Profilbild des Users. Dabei wird das Bild nur gelöscht, wenn es nicht das Standardbild ist.
        private async void DeletePicture_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Profilbild löschen", "Möchtest du dein Profilbild wirklich löschen?", "Ja", "Nein");

            if (answer)
            {
                try
                {
                    if (User.pbFileName != AppConfig.pbPlaceholderUrl)
                    {
                        var fileName = User.pbFileName.Split('/').Last();
                        var id = fileName.Split('.').First();

                        await userService.RemoveProfilePicture(User.userID, id);
                        ProfilePicture.Source = AppConfig.pbPlaceholderUrl;
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alert", ex.Message, "OK");
                }

            }
        }

        // Löscht den User. Dabei wird der User nur gelöscht, wenn er sich vorher nochmal bestätigt hat.
        // Anschließend wird der User ausgeloggt und die App wird auf die Startseite zurückgesetzt.
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Account löschen", "Möchtest du dein Account wirklich löschen? Dies kann nicht rückgängig gemacht werden", "Ja", "Nein");

            if (answer)
            {
                try
                {
                    await userService.DeleteUser(User.userID);
                    await authService.Logout();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alert", ex.Message, "OK");
                }

                await Navigation.PopToRootAsync();
            }
        }

    }
}