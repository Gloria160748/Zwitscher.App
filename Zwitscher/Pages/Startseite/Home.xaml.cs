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
        public string retweetId = "";

        public List<Post> apiData { get; set; }
        private Post editPost = null;
        private List<IFormFile> files = new List<IFormFile>();
        private PostService PostService = new PostService();
        private UserService UserService = new UserService();


        public Home()
        {
            InitializeComponent();
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
            files.Add(await MediaConverter.SelectImage());
        }

        private async void CreateButton_Clicked(object sender, EventArgs e) //post erstellen
        {
            NewPost post = new NewPost
            {
                Id = "",
                TextContent = PostText.Text,
                IsPublic = SwitchIsPublic.IsToggled,
                UserId = "", // Hier kannst du die UserID setzen
                retweetsID = retweetId
            };

            // Rufe die CreatePost-Methode auf und übergebe die Dateien (IFormFile[]) und den Post (NewPost)
            try
            {
                if (editPost != null)
                {
                    post.Id = editPost.postID;
                    await PostService.EditPost(post);
                    editPost = null;
                    CreateButton.Text = "Post erstellen";
                }
                else
                {
                    await PostService.CreatePost(files.ToArray(), post);
                }
                files = new List<IFormFile>();
                PostText.Text = "";
                retweetId = "";
                CreateRezwitscher.IsVisible = false;
                Refresh();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
        }

        private void DeleteRezwitscherButton_Clicked(object sender, EventArgs e)
        {
            retweetId = "";
            CreateRezwitscher.IsVisible = false;
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

        private void EditButton_Clicked(object sender, EventArgs e)
        {
            var post = (Post)((ImageButton)sender).BindingContext;
            editPost = post;
            if (post.isRetweet)
            {
                retweetId = post.RetweetedPost.postID;
                CreateRezwitscher.IsVisible = true;
                RezwitscherLabel.Text = "Rezwitscher von " + post.RetweetedPost.user_username;
            }

            PostText.Text = post.postText;
            SwitchIsPublic.IsToggled = true;
            CreateButton.Text = "Bearbeiten";

            ScrollViewSite.ScrollToAsync(0, 0, true);
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
            await Navigation.PushAsync(new Kommentare(post.postID));
        }

        private void RezwitscherButton_Clicked(object sender, EventArgs e)
        {
            var post = (Post)((ImageButton)sender).BindingContext;
            retweetId = post.postID;
            CreateRezwitscher.IsVisible = true;
            RezwitscherLabel.Text = "Rezwitscher von " + post.user_username;
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

        private async void Refresh()
        {
            apiData = await PostService.GetPosts();
            postsListView.ItemsSource = apiData;
            OnPropertyChanged("apiData");
        }
    }
}