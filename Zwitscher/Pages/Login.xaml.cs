using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zwitscher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
             if(txtUsername.Text=="admin" && txtPasswort.Text=="1234")
            {
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Ups..", "Username oder Passwort ist falsch!", "ok");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.Registrieren());
        }

        
    }
}