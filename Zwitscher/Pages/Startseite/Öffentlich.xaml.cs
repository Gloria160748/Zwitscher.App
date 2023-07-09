using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Plugin.Media;
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
using Zwitscher.Services;

namespace Zwitscher.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Öffentlich : ContentPage
	{
		public Öffentlich ()
		{
			InitializeComponent ();
		}
    }
}