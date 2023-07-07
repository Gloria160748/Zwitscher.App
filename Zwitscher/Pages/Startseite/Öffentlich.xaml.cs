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
		private PostService postService = new PostService();
		private List<IFormFile> files = new List<IFormFile>();

		public Öffentlich ()
		{
			InitializeComponent ();
		}

		private async void ButtonCreatePost_Clicked(object sender, EventArgs e)
		{
            NewPost post = new NewPost
            {
				Id = "",
                TextContent = EntryTextContent.Text,
                IsPublic = SwitchIsPublic.IsToggled,
                UserId = "" // Hier kannst du die UserID setzen
            };

            // Rufe die CreatePost-Methode auf und übergebe die Dateien (IFormFile[]) und den Post (NewPost)
            try
            {
                await postService.CreatePost(files.ToArray(), post);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
        }

		private async void ButtonSelectImage_Clicked(object sender, EventArgs e)
		{
			// Hier kannst du die Dateien auswählen
			await CrossMedia.Current.Initialize();
			var file = await CrossMedia.Current.PickPhotoAsync();
			if (file == null)
                    return;
			files.Add(ConvertToFormFile(file));
		}

        private IFormFile ConvertToFormFile(MediaFile file)
        {
            // Erstellen Sie eine neue MemoryStream und kopieren Sie die Daten aus der MediaFile-Stream
            var memoryStream = new MemoryStream();
            file.GetStream().CopyTo(memoryStream);
            memoryStream.Position = 0;

            // Erstellen Sie ein neues FormFile-Objekt mit der MemoryStream und den erforderlichen Metadaten
            return new FormFile(memoryStream, 0, memoryStream.Length, file.Path, file.Path);
        }
    }
}