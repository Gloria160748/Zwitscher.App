﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zwitscher.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registrieren : ContentPage
	{
		public Registrieren ()
		{
			InitializeComponent ();
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }
    }
}