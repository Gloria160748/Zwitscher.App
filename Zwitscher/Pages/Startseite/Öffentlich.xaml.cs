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
	public partial class Öffentlich : ContentPage
	{
		private PostService postService = new PostService();

		public Öffentlich ()
		{
			InitializeComponent ();
		}

		private void ButtonCreatePost_Clicked(object sender, EventArgs e)
		{
            NewPost post = new NewPost
            {
				Id = "",
                TextContent = EntryTextContent.Text,
                IsPublic = SwitchIsPublic.IsToggled,
                UserId = "" // Hier kannst du die UserID setzen
            };

            // Rufe die CreatePost-Methode auf und übergebe die Dateien (IFormFile[]) und den Post (NewPost)
            postService.CreatePost(null, post);
        }
	}
}