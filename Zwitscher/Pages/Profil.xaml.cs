using System;
using System.Collections.Generic;
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

        public Profil(string id)
        {
            InitializeComponent();

            userId = id;
        }

        // Laden des aktuellen Profils. Dabei werden alle Labels auf die richtigen Werte gesetzt und die Sichtbarkeit der Biographie abhängig davon gemacht,
        // ob sie leer ist. Außerdem wird die Sichtbarkeit der Buttons angepasst, je nachdem ob es sich um das eigene Profil handelt oder nicht.
        // Zusätzlich werden die Follow und Block Buttons auf die richtigen Werte gesetzt.
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

        // Wenn der Block Button gedrückt wird, wird der aktive User entweder blockiert oder entblockiert, je nachdem ob er den User bereits blockiert hat.
        // Wenn der aktive User den User blockiert, wird der Block Button auf "Entblocken" gesetzt, ansonsten auf "Blockieren".
        // Wenn der aktive User versucht sich selber zu blockieren, wird eine Fehlermeldung angezeigt.
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

        // Wenn der Follow Button gedrückt wird, wird der aktive User entweder dem User folgen oder ihn nicht mehr folgen, je nachdem ob er ihm bereits folgt.
        // Wenn der aktive User dem User folgt, wird der Follow Button auf "Nicht mehr folgen" gesetzt, ansonsten auf "Folgen".
        // Der Follower Count wird entsprechend angepasst.
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

        // Wenn ein User auf der eigenen Profilseite ist und auf den Edit Button drückt, wird er auf die ProfilBearbeitung Seite weitergeleitet.
        private void EditButton_Clicked(object sender, EventArgs e) //Editbutton
        {
            Navigation.PushAsync(new ProfilBearbeitung(shownUser));
        }
    }
}