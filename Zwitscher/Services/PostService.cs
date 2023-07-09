using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Zwitscher.Models;

namespace Zwitscher.Services
{
    public class PostService
    {
        private HttpClient _client;
        private string baseUrl = AppConfig.ApiUrl;
        private AuthService authService = new AuthService();

        public PostService()
        {
            _client = AppConfig.GetHttpClient();
        }

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
                if (post.user_profilePicture != "")
                {
                    post.user_profilePicture = baseUrl + "/Media/" + post.user_profilePicture;
                }
                else
                {
                    post.user_profilePicture = baseUrl + "/Media/" + AppConfig.pbPlaceholder;
                }

                for (int i = 0; i < post.mediaList.Count; i++)
                {
                    post.mediaList[i] = baseUrl + "/Media/" + post.mediaList[i];
                }
                post.mediaIncluded = post.mediaList.Count > 0;
                post.isRetweet = post.retweetsPost != "";
                post.isOwnPost = authService.IsActiveUser(post.user_username);
                if (post.isRetweet)
                {
                    post.RetweetedPost = await GetSinglePost(post.retweetsPost);
                }
            }

            return apiData;
        }

        public async Task<Post> GetSinglePost(string id)
        {
            HttpResponseMessage response = await _client.GetAsync("API/Post?id="+id);
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<Post>(content);
            if (apiData.user_profilePicture != "")
            {
                apiData.user_profilePicture = baseUrl + "/Media/" + apiData.user_profilePicture;
            }
            else
            {
                apiData.user_profilePicture = baseUrl + "/Media/" + AppConfig.pbPlaceholder;
            }
            for (int i = 0; i < apiData.mediaList.Count; i++)
            {
                apiData.mediaList[i] = baseUrl + "/Media/" + apiData.mediaList[i];
            }
            apiData.mediaIncluded = apiData.mediaList.Count > 0;
            apiData.isRetweet = apiData.retweetsPost != "";
            apiData.isOwnPost = authService.IsActiveUser(apiData.user_username);
            return apiData;
        }

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

        public async Task<HttpResponseMessage> DeletePost(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("API/Posts/Remove?id="+id);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<List<Comment>> PostComments(string id)
        {
            HttpResponseMessage response = await _client.GetAsync("API/Posts/Comments?id=" + id);
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<Comment>>(content);

            foreach (var post in apiData)
            {
                if (post.user_profilePicture != "")
                {
                    post.user_profilePicture = baseUrl + "/Media/" + post.user_profilePicture;
                }
                else
                {
                    post.user_profilePicture = baseUrl + "/Media/" + AppConfig.pbPlaceholder;
                }
            }

            return apiData;
        }

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

        public async Task<HttpResponseMessage> DeleteComment(string postId, string commentId)
        {
            HttpResponseMessage response = await _client.DeleteAsync("API/Posts/Comment/Remove?postId=" + postId + "&commentId=" + commentId);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<List<Comment>> CommentsToComments(string id)
        {
            HttpResponseMessage response = await _client.GetAsync("API/Comments/Comments?id=" + id);
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<Comment>>(content);

            foreach (var post in apiData)
            {
                if (post.user_profilePicture != "")
                {
                    post.user_profilePicture = baseUrl + "/Media/" + post.user_profilePicture;
                }
                else
                {
                    post.user_profilePicture = baseUrl + "/Media/" + AppConfig.pbPlaceholder;
                }
            }

            return apiData;
        }

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

        public async Task<HttpResponseMessage> DeleteCommentToComment(string commentId, string commentToCommentId)
        {
            HttpResponseMessage response = await _client.PostAsync("API/Comments/Comment/Remove?commentId=" + commentId + "&commentToRemoveId=" + commentToCommentId, null);
            return response.EnsureSuccessStatusCode();
        }
    }
}
