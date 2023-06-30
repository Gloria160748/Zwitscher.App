using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
                post.user_profilePicture = baseUrl + "/Media/" + post.user_profilePicture;
            }

            return apiData;
        }

        public async void CreatePost(IFormFile[] files, NewPost post)
        {
            // TODO: Zur Zeit ist es noch nicht möglich, Dateien mit zu übermitteln
            var content = new MultipartFormDataContent
            {
                { new StringContent(post.TextContent), "TextContent" },
                { new StringContent(post.IsPublic.ToString()), "IsPublic" },
                { new StringContent(post.UserId), "UserId" }
            };


            HttpResponseMessage response = await _client.PostAsync("Api/Posts/Add", content);
        }

        public async void DeletePost(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("API/Posts/Remove?id="+id);
        }

        public async Task<List<Comment>> PostComments(string id)
        {
            HttpResponseMessage response = await _client.GetAsync("API/Posts/Comments?id=" + id);
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<Comment>>(content);

            foreach (var post in apiData)
            {
                post.user_profilePicture = baseUrl + "/Media/" + post.user_profilePicture;
            }

            return apiData;
        }

         public async void ManageVote(string id, bool IsUpVote)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(id), "postId" },
                { new StringContent(IsUpVote.ToString()), "IsUpVote" }
            };
            HttpResponseMessage response = await _client.PostAsync("API/Posts/Vote", content);
        }

        public async void AddComment(string id, string text)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(id), "postId" },
                { new StringContent(text), "CommentText" }
            };
            HttpResponseMessage response = await _client.PostAsync("API/Posts/Comment/Add", content);
        }

        public async void DeleteComment(string postId, string commentId)
        {
            HttpResponseMessage response = await _client.DeleteAsync("API/Posts/Comment/Remove?postId=" + postId + "&commentId=" + commentId);
        }
    }
}
