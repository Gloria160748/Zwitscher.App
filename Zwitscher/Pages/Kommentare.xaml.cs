using System;
using System.Collections.Generic;
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
        private string recommentId;
        private PostService PostService = new PostService();
        private Comment editComment = null;
        public List<Comment> comments;
        
        public Kommentare(string id)
        {
            InitializeComponent();
            postID = id;
        }

        // Laden der Kommentare beim Start der Seite
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            comments = await PostService.PostComments(postID);
            commentListView.ItemsSource = comments;
            OnPropertyChanged("comments");
        }

        // Löschen eines Kommentars und aktualisieren der Kommentare
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

        // Erstellen eines Kommentars. Alternativ kann ein Kommentar bearbeitet werden.
        private async void ButtonCreateComment_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (editComment != null)
                {
                    await PostService.EditComment(editComment.commentId, CommentText.Text);
                    editComment = null;
                    CreateComment.Text = "Kommentar erstellen";
                }
                else if (recommentId != null)
                {
                    await PostService.AddCommentToComment(recommentId, CommentText.Text);
                    recommentId = null;
                    CreateRecomment.IsVisible = false;
                    RecommentLabel.Text = "";
                }
                else
                {
                    await PostService.AddComment(postID, CommentText.Text);
                }

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

        // Bearbeiten eines Kommentars über die Kommentar erstellen UI
        private void EditButton_Clicked(object sender, EventArgs e)
        {
            var comment = (Comment)((ImageButton)sender).BindingContext;
            editComment = comment;

            CommentText.Text = comment.commentText;
            CreateComment.Text = "Bearbeiten";

            ScrollViewSite.ScrollToAsync(0, 0, true);
        }

        // Antworten auf einen Kommentar
        private void CommentButton_Clicked(object sender, EventArgs e)
        {
            var comment = (Comment)((ImageButton)sender).BindingContext;
            recommentId = comment.commentId;
            CreateRecomment.IsVisible = true;
            RecommentLabel.Text = "Antworten auf " + comment.user_username;
        }

        // Entferne den Verweis auf einen anderen Kommentar, der beantwortet werden sollte
        private void DeleteRecommentButton_Clicked(object sender, EventArgs e)
        {
            recommentId = null;
            CreateRecomment.IsVisible = false;
            RecommentLabel.Text = "";
        }
    }
}