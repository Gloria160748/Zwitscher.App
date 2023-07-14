using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Zwitscher.Models;

namespace Zwitscher.Services
{
    public class PostService
    {
        private HttpClient _client;
        private AuthService authService = new AuthService();

        public PostService()
        {
            _client = AppConfig.GetHttpClient();
        }

        // Laden der Posts für die Startseite
        public async Task<List<Post>> GetPosts()
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return null;
            }

            HttpResponseMessage response = await _client.GetAsync("API/Posts");
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<Post>>(content);

            foreach (var post in apiData)
            {
                post.isRetweet = post.retweetsPost != "";
                post.user_profilePicture = MediaConverter.ChangeProfilePath(post.user_profilePicture);
                post.mediaList = MediaConverter.ChangeMediaPath(post);
                post.videoList = MediaConverter.GetVideoPath(post.mediaList);
                post.videoIncluded = post.videoList.Count > 0;
                post.mediaList = MediaConverter.GetImagePath(post.mediaList);
                post.mediaIncluded = post.mediaList.Count > 0;
                post.isOwnPost = authService.IsActiveUser(post.user_username);
                if (post.isRetweet)
                {
                    post.RetweetedPost = await GetSinglePost(post.retweetsPost);
                }
            }

            return apiData;
        }

        // Laden der Öffentlichen Posts
        public async Task<List<Post>> GetPostsPublic()
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return null;
            }

            HttpResponseMessage response = await _client.GetAsync("API/OnlyPublicPosts");
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<Post>>(content);

            foreach (var post in apiData)
            {
                post.isRetweet = post.retweetsPost != "";
                post.user_profilePicture = MediaConverter.ChangeProfilePath(post.user_profilePicture);
                post.mediaList = MediaConverter.ChangeMediaPath(post);
                post.videoList = MediaConverter.GetVideoPath(post.mediaList);
                post.videoIncluded = post.videoList.Count > 0;
                post.mediaList = MediaConverter.GetImagePath(post.mediaList);
                post.mediaIncluded = post.mediaList.Count > 0;
                post.isOwnPost = authService.IsActiveUser(post.user_username);
                if (post.isRetweet)
                {
                    post.RetweetedPost = await GetSinglePost(post.retweetsPost);
                }
            }

            return apiData;
        }

        // Laden der Trending Posts
        public async Task<List<Post>> GetPostsTrending()
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return null;
            }

            HttpResponseMessage response = await _client.GetAsync("API/PostsSortedByRating");
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<Post>>(content);

            foreach (var post in apiData)
            {
                post.isRetweet = post.retweetsPost != "";
                post.user_profilePicture = MediaConverter.ChangeProfilePath(post.user_profilePicture);
                post.mediaList = MediaConverter.ChangeMediaPath(post);
                post.videoList = MediaConverter.GetVideoPath(post.mediaList);
                post.videoIncluded = post.videoList.Count > 0;
                post.mediaList = MediaConverter.GetImagePath(post.mediaList);
                post.mediaIncluded = post.mediaList.Count > 0;
                post.isOwnPost = authService.IsActiveUser(post.user_username);
                if (post.isRetweet)
                {
                    post.RetweetedPost = await GetSinglePost(post.retweetsPost);
                }
            }

            return apiData;
        }

        // Laden eines einzelnen Posts nach id
        public async Task<Post> GetSinglePost(string id)
        {
            HttpResponseMessage response = await _client.GetAsync("API/Post?id=" + id);
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<Post>(content);
            apiData.isRetweet = apiData.retweetsPost != "";
            apiData.user_profilePicture = MediaConverter.ChangeProfilePath(apiData.user_profilePicture);
            apiData.mediaList = MediaConverter.ChangeMediaPath(apiData);
            apiData.videoList = MediaConverter.GetVideoPath(apiData.mediaList);
            apiData.videoIncluded = apiData.videoList.Count > 0;
            apiData.mediaList = MediaConverter.GetImagePath(apiData.mediaList);
            apiData.mediaIncluded = apiData.mediaList.Count > 0;
            apiData.isOwnPost = authService.IsActiveUser(apiData.user_username);
            return apiData;
        }

        // Erstellen eines Posts mit Bildern und Videos
        public async Task<HttpResponseMessage> CreatePost(IFormFile[] files, NewPost post)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "API/Posts/Add");
            var content = new MultipartFormDataContent();
            for (int i = 0; i < files.Length; i++)
            {
                content.Add(new StreamContent(files[i].OpenReadStream()), "files", files[i].FileName);
            }
            content.Add(new StringContent(post.TextContent), "TextContent");
            content.Add(new StringContent(post.IsPublic.ToString()), "IsPublic");
            content.Add(new StringContent(post.retweetsID), "retweetsID");
            request.Content = content;
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return response.EnsureSuccessStatusCode();
        }

        // Editieren des Textes eines Posts
        public async Task<HttpResponseMessage> EditPost(NewPost post)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(post.Id), "postID" },
                { new StringContent(post.TextContent), "TextContent" },
                { new StringContent(post.IsPublic.ToString()), "IsPublic" },
                { new StringContent(post.retweetsID), "retweetsID" }
            };
            HttpResponseMessage response = await _client.PostAsync("API/Posts/Edit", content);
            return response.EnsureSuccessStatusCode();
        }

        // Löschen eines Posts
        public async Task<HttpResponseMessage> DeletePost(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("API/Posts/Remove?id=" + id);
            return response.EnsureSuccessStatusCode();
        }

        // Laden der Kommentare zu einem Post. Dabei werden auch die Kommentare der Kommentare geladen und in die liste Comments eines einzelnen
        // Kommentares gespeichert.
        public async Task<List<Comment>> PostComments(string id)
        {
            HttpResponseMessage response = await _client.GetAsync("API/Posts/Comments?id=" + id);
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<Comment>>(content);

            foreach (var post in apiData)
            {
                post.Comments = await CommentsToComments(post.commentId);
                post.hasComments = post.Comments.Count > 0;

                post.user_profilePicture = MediaConverter.ChangeProfilePath(post.user_profilePicture);
            }

            return apiData;
        }

        // Das Voting für einen Post ist abhängig von der ID des Posts und ob der User upvoten oder downvoten möchte.
        public async Task<HttpResponseMessage> ManageVote(string id, bool IsUpVote)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(id), "postId" },
                { new StringContent(IsUpVote.ToString()), "IsUpVote" }
            };
            HttpResponseMessage response = await _client.PostAsync("API/Posts/Vote", content);
            return response.EnsureSuccessStatusCode();
        }

        // Fügt einen Kommentar zu einem Post hinzu
        public async Task<HttpResponseMessage> AddComment(string id, string text)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(id), "postId" },
                { new StringContent(text), "CommentText" }
            };
            HttpResponseMessage response = await _client.PostAsync("API/Posts/Comment/Add", content);
            return response.EnsureSuccessStatusCode();
        }

        // Editiert einen Kommentar
        public async Task<HttpResponseMessage> EditComment(string id, string text)
        {
            HttpResponseMessage response = await _client.PostAsync("API/Comments/Edit?id=" + id + "&CommentText=" + text, null);
            return response.EnsureSuccessStatusCode();
        }

        // Löscht einen Kommentar und alle seine Kommentare
        public async Task<HttpResponseMessage> DeleteComment(string postId, string commentId)
        {
            HttpResponseMessage response = await _client.DeleteAsync("API/Posts/Comment/Remove?postId=" + postId + "&commentId=" + commentId);
            return response.EnsureSuccessStatusCode();
        }

        // Holt alle Kommentare zu einem Kommentar
        public async Task<List<Comment>> CommentsToComments(string id)
        {
            HttpResponseMessage response = await _client.GetAsync("API/Comments/Comments?id=" + id);
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<Comment>>(content);

            foreach (var post in apiData)
            {
                post.user_profilePicture = MediaConverter.ChangeProfilePath(post.user_profilePicture);
            }

            return apiData;
        }

        // Fügt einen Kommentar zu einem Kommentar hinzu
        public async Task<HttpResponseMessage> AddCommentToComment(string id, string text)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(id), "commentId" },
                { new StringContent(text), "CommentText" }
            };
            HttpResponseMessage response = await _client.PostAsync("API/Comments/Comment/Add", content);
            return response.EnsureSuccessStatusCode();
        }

        // Löscht einen Kommentar zu einem Kommentar
        public async Task<HttpResponseMessage> DeleteCommentToComment(string commentId, string commentToCommentId)
        {
            HttpResponseMessage response = await _client.PostAsync("API/Comments/Comment/Remove?commentId=" + commentId + "&commentToRemoveId=" + commentToCommentId, null);
            return response.EnsureSuccessStatusCode();
        }

        // Ermöglicht es zu einem bereits bestehenden Post Medien hinzuzufügen
        public async Task<HttpResponseMessage> AddMediaToPost(string id, IFormFile[] files)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(id), "postID" }
            };
            for (int i = 0; i < files.Length; i++)
            {
                content.Add(new StreamContent(files[i].OpenReadStream()), "files", files[i].FileName);
            }

            HttpResponseMessage response = await _client.PostAsync("API/Posts/Media/Add", content);

            return response.EnsureSuccessStatusCode();
        }

        // Löscht alle Medien von einem Post
        public async Task<HttpResponseMessage> RemoveMediaFromPost(string postId)
        {
            HttpResponseMessage response = await _client.GetAsync("API/Post?id=" + postId);
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<Post>(content);
            foreach (var media in apiData.mediaList)
            {
                response = await _client.PostAsync("API/Posts/Media/Remove?postID=" + postId + "&mediaToRemoveId=" + media.Split('.')[0], null);
            }
            return response.EnsureSuccessStatusCode();
        }
    }
}
