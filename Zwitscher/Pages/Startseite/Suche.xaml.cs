using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zwitscher.Models;
using Zwitscher.Services;

namespace Zwitscher.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Suche : ContentPage
	{
        private UserService UserService = new UserService();
        private List<User> apiData { get; set; }

		public Suche ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            apiData = await UserService.Users();
            userList.ItemsSource = apiData;
            OnPropertyChanged("apiData");
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;
            if (string.IsNullOrEmpty(searchText))
            {
                apiData = await UserService.Users();
            }
            else
            {
                apiData = await UserService.SearchUsers(searchText);
            }
            userList.ItemsSource = apiData;
            OnPropertyChanged("apiData");
        }

        private void SelectUser(object sender, EventArgs e)
        {
            var user = (User)((Frame)sender).BindingContext;
            Navigation.PushAsync(new Profil(user.userID));
        }
	}
}