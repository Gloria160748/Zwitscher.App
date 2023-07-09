using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Zwitscher.Models;
using Zwitscher.Services;

namespace Zwitscher.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		public List<Post> apiData { get; set; }
		public Post activePost { get; set; }
		private List<IFormFile> files = new List<IFormFile>();
		private PostService PostService = new PostService();
		private UserService UserService = new UserService();

		public Home ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            apiData = await PostService.GetPosts();
            postsListView.ItemsSource = apiData;
            OnPropertyChanged("apiData");
        }

        private async void UploadButton_Clicked(object sender, EventArgs e) // bild hochladen
        {
            // Hier kannst du die Dateien auswählen
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;
            files.Add(ConvertToFormFile(file));
        }

		private async void CreateButton_Clicked(object sender, EventArgs e) //post erstellen
		{
			NewPost post = new NewPost
            {
                Id = "",
                TextContent = PostText.Text,
                IsPublic = SwitchIsPublic.IsToggled,
                UserId = "" // Hier kannst du die UserID setzen
            };

            // Rufe die CreatePost-Methode auf und übergebe die Dateien (IFormFile[]) und den Post (NewPost)
            try
            {
                await PostService.CreatePost(files.ToArray(), post);
                files = new List<IFormFile>();
                PostText.Text = "";
                Refresh();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
		}

        private async void UpvoteButton_Clicked(object sender, EventArgs e)
        {
            var post = (Post)((ImageButton)sender).BindingContext;
            try
            {
                await PostService.ManageVote(post.postID, true);
                Refresh();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
        }
        private async void DownvoteButton_Clicked(object sender, EventArgs e)
        {
            var post = (Post)((ImageButton)sender).BindingContext;
            try
            {
                await PostService.ManageVote(post.postID, false);
                Refresh();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
        }
        private async void CommentButton_Clicked(object sender, EventArgs e)
        {
            var post = (Post)((ImageButton)sender).BindingContext;
            activePost = post;
            await Navigation.PushAsync(new Kommentare(activePost.postID));
        }
        private async void RezwitscherButton_Clicked(object sender, EventArgs e)
        {
            var post = (Post)((ImageButton)sender).BindingContext;
            try
            {
                //await PostService.RetweetPost(post.postID);
                Refresh();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var post = (Post)((ImageButton)sender).BindingContext;
            try
            {
                await PostService.DeletePost(post.postID);
                Refresh();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
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

        private async void Refresh()
        {
            apiData = await PostService.GetPosts();
            postsListView.ItemsSource = apiData;
            OnPropertyChanged("apiData");
        }
    }
}