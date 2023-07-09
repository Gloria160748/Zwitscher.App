using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zwitscher.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		public Home ()
		{
			InitializeComponent ();
		}

        private void ImageButton_Clicked(object sender, EventArgs e) // bild hochladen
        {

        }

        private void Button_Clicked(object sender, EventArgs e) //post erstellen
        {

        }
    }
}