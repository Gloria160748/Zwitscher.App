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
	public partial class Profil : ContentPage
	{
		public Profil ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e) //Followbutton
        {

        }
    }
}