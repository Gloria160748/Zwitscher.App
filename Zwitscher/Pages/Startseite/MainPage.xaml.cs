using System;
using Xamarin.Forms;

namespace Zwitscher
{
    public partial class MainPage : TabbedPage
    {
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

        private void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {

        }
    }
}
