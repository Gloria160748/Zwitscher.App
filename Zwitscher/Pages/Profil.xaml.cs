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
    public partial class Profil : ContentPage
    {
        private readonly UserService userService = new UserService();
        private readonly AuthService authService = new AuthService();
        private string userId = "";
        private User shownUser = null;
        private bool ownProfile = false;
        private bool activeUserFollows = false;
        private bool activeUserBlocked = false;
        private List<Post> posts = new List<Post>();

        public Profil()
        {
            InitializeComponent();
        }

        public Profil(string id)
        {
            InitializeComponent();

            userId = id;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var user = await userService.GetUserById(userId);
            shownUser = user;
            username.Text = user.username;
            avatar.Source = user.pbFileName;
            followerCount.Text = user.followerCount.ToString();
            followCount.Text = user.followedCount.ToString();
            if (string.IsNullOrEmpty(user.biography))
            {
                BioFrame.IsVisible = false;
            }
            else
            {
                bio.Text = user.biography;
            }
            ownProfile = authService.IsActiveUser(user.username);
            if (ownProfile)
            {
                FollowButton.IsVisible = false;
                EditButton.IsVisible = true;
            }
            else
            {
                FollowButton.IsVisible = true;
                EditButton.IsVisible = false;
            }

            activeUserFollows = await userService.ActiveUserFollows(userId);

            if (activeUserFollows)
            {
                FollowButton.Text = "Nicht mehr folgen";
            }
            else
            {
                FollowButton.Text = "Folgen";
            }

            posts = await userService.UserPosts(userId);
            postsListView.ItemsSource = posts;
            postCount.Text = posts.Count.ToString();

            activeUserBlocked = await userService.ActiveUserBlocked(userId);
            if (activeUserBlocked)
            {
                BlockButton.Text = "Entblocken";
            }
            else
            {
                BlockButton.Text = "Blockieren";
            }
        }

        private void BlockierenClicked(object sender, EventArgs e)
        {
            if (ownProfile)
            {
                DisplayAlert("Alert", "Du kannst dich nicht selber blockieren!", "OK");
                return;
            }
            if (activeUserBlocked)
            {
                _ = userService.UnblockUser(userId);
                activeUserBlocked = false;
                BlockButton.Text = "Blockieren";
            }
            else
            {
                _ = userService.BlockUser(userId);
                activeUserBlocked = true;
                BlockButton.Text = "Entblocken";
            }
        }

        private async void FollowButton_Clicked(object sender, EventArgs e) //Followbutton
        {
            try
            {
                if (activeUserFollows)
                {
                    await userService.UnfollowUser(shownUser.userID);
                    activeUserFollows = false;
                    FollowButton.Text = "Folgen";
                    shownUser.followerCount--;
                    followerCount.Text = shownUser.followerCount.ToString();
                }
                else
                {
                    await userService.FollowUser(shownUser.userID);
                    activeUserFollows = true;
                    FollowButton.Text = "Nicht mehr folgen";
                    shownUser.followerCount++;
                    followerCount.Text = shownUser.followerCount.ToString();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "OK");
            }


        }

        private void EditButton_Clicked(object sender, EventArgs e) //Editbutton
        {

        }
    }
}