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
		private UserService UserService = new UserService();

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

		private async void UpvoteButton_Clicked(object sender, EventArgs e) 
		{
			var post = (Post)((ImageButton)sender).BindingContext;
			try
			{
				  await PostService.ManageVote(post.postID, true);
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
			}
			catch (Exception ex)
			{
                await DisplayAlert("Alert", ex.Message, "OK");
            }
		}
		private async void CommentButton_Clicked(object sender, EventArgs e)
		{
			var post = (Post)((Button)sender).BindingContext;
			postsListView.IsVisible = false;
			commentView.IsVisible = true;
			activePost = post;
			commentListView.ItemsSource = await PostService.PostComments(post.postID);
		}
		private async void DeleteButton_Clicked(object sender, EventArgs e)
		{
            var post = (Post)((ImageButton)sender).BindingContext;
			try
			{
				await PostService.DeletePost(post.postID);
			}
			catch (Exception ex)
			{
                await DisplayAlert("Alert", ex.Message, "OK");
            }
			Button_Clicked(sender, e);
        }

		private async void ButtonCreateComment_Clicked(object sender, EventArgs e)
		{
			try
			{
                await PostService.AddComment(activePost.postID, CommentText.Text);
            }
            catch (Exception ex)
			{
                await DisplayAlert("Alert", ex.Message, "OK");
            }
		}

        private async void DeleteCommentButton_Clicked(object sender, EventArgs e)
        {
            var comment = (Comment)((ImageButton)sender).BindingContext;
            try
			{
                await PostService.DeleteComment(activePost.postID, comment.commentId);
            }
            catch (Exception ex)
			{
                await DisplayAlert("Alert", ex.Message, "OK");
            }
        }

        private void GetPosts(object sender, EventArgs e)
		{
            apiData = PostService.GetPosts().Result;
            postsListView.ItemsSource = apiData;
        }
    }
}