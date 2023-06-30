using System;
using System.Collections.Generic;
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
		private PostService PostService = new PostService();

		public Home ()
		{
			InitializeComponent ();
			
			OnPropertyChanged("apiData");
		}

		private async void Button_Clicked(object sender, EventArgs e)
		{
			apiData = await PostService.GetPosts();
			commentView.IsVisible = false;
			postsListView.IsVisible = true;
			postsListView.ItemsSource = apiData;
		}

		private void UpvoteButton_Clicked(object sender, EventArgs e) 
		{
			var post = (Post)((ImageButton)sender).BindingContext;
            PostService.ManageVote(post.postID, true);
		}
		private void DownvoteButton_Clicked(object sender, EventArgs e)
		{
			var post = (Post)((ImageButton)sender).BindingContext;
            PostService.ManageVote(post.postID, false);
		}
		private async void CommentButton_Clicked(object sender, EventArgs e)
		{
			var post = (Post)((Button)sender).BindingContext;
			postsListView.IsVisible = false;
			commentView.IsVisible = true;
			activePost = post;
			commentListView.ItemsSource = await PostService.PostComments(post.postID);
		}
		private void DeleteButton_Clicked(object sender, EventArgs e)
		{
            var post = (Post)((ImageButton)sender).BindingContext;
            PostService.DeletePost(post.postID);
			Button_Clicked(sender, e);
        }

		private void ButtonCreateComment_Clicked(object sender, EventArgs e)
		{
			PostService.AddComment(activePost.postID ,CommentText.Text);
		}

        private void DeleteCommentButton_Clicked(object sender, EventArgs e)
        {
            var comment = (Comment)((ImageButton)sender).BindingContext;
            PostService.DeleteComment(activePost.postID,comment.commentId);
        }

        private void GetPosts(object sender, EventArgs e)
		{
            apiData = PostService.GetPosts().Result;
            postsListView.ItemsSource = apiData;
        }
    }
}