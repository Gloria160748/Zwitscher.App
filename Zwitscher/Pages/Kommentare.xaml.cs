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
    public partial class Kommentare : ContentPage
    {
        private string postID;
        private PostService PostService = new PostService();
        public List<Comment> comments;
        public Kommentare()
        {
            InitializeComponent();
        }
        public Kommentare(string id)
        {
            InitializeComponent();
            postID = id;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            comments = await PostService.PostComments(postID);
            commentListView.ItemsSource = comments;
            OnPropertyChanged("comments");
        }

        private async void DeleteCommentButton_Clicked(object sender, EventArgs e)
        {
            var comment = (Comment)((ImageButton)sender).BindingContext;
            try
            {
                await PostService.DeleteComment(postID, comment.commentId);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
            comments = await PostService.PostComments(postID);
            commentListView.ItemsSource = comments;
            OnPropertyChanged("comments");
        }

        private async void ButtonCreateComment_Clicked(object sender, EventArgs e)
        {
            try
            {
                await PostService.AddComment(postID, CommentText.Text);
                CommentText.Text = "";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }
            comments = await PostService.PostComments(postID);
            commentListView.ItemsSource = comments;
            OnPropertyChanged("comments");
        }
    }
}