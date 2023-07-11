using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zwitscher.Models;
using Plugin.Media;
using Zwitscher.Services;
using System.Globalization;

namespace Zwitscher.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilBearbeitung : ContentPage
    {
        private User User;
        private IFormFile profilePicture;
        private UserService userService = new UserService();
        private AuthService authService = new AuthService();

        public ProfilBearbeitung(User user)
        {
            User = user;
            InitializeComponent();

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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var user = new User();
            user.userID = User.userID;
            user.lastname = Nachname.Text;
            user.firstname = Vorname.Text;
            user.username = Username.Text;
            user.birthday = Geburtsdatum.Date.ToString();
            user.biography = Biographíe.Text;
            user.gender = Geschlecht.SelectedIndex.ToString();

            user.password = Passwort.Text ?? "";

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

        private async void UploadButton_Clicked(object sender, EventArgs e)
        {
            profilePicture = await MediaConverter.SelectImage();
            ProfilePicture.Source = profilePicture.FileName;
        }

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