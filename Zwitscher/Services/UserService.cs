using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Zwitscher.Models;

namespace Zwitscher.Services
{
    public class UserService
    {
        private HttpClient _client;
        private string baseUrl = AppConfig.ApiUrl;

        public UserService()
        {
            _client = AppConfig.GetHttpClient();
        }

        public async Task<User> GetUserById(string id)
        {
            HttpResponseMessage response = await _client.GetAsync("API/User?id=" + id);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<User>(content);

            if (apiData.pbFileName != null)
            {
                apiData.pbFileName = baseUrl + "/Media/" + apiData.pbFileName;
            }
            else
            {
                apiData.pbFileName = baseUrl + "/Media/" + AppConfig.pbPlaceholder;
            }

            return apiData;
        }

        public async Task<List<User>> Users()
        {
            var response = await _client.GetAsync("API/Users");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<User>>(content);

            foreach (var user in apiData)
            {
                if (user.pbFileName != null)
                {
                    user.pbFileName = baseUrl + "/Media/" + user.pbFileName;
                }
                else
                {
                    user.pbFileName = baseUrl + "/Media/" + AppConfig.pbPlaceholder;
                }
            }

            return apiData;
        }

        public async Task<List<Post>> UserPosts(string id)
        {
            var response = await _client.GetAsync("API/UserPosts?id=" + id);
            response.EnsureSuccessStatusCode();
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
            }

            return apiData;
        }

        public async Task<List<User>> FollowedUsers(string id)
        {
            var response = await _client.GetAsync("API/Users/Following?UserID=" + id);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<User>>(content);

            foreach (var user in apiData)
            {
                if (user.pbFileName != null)
                {
                    user.pbFileName = baseUrl + "/Media/" + user.pbFileName;
                }
                else
                {
                    user.pbFileName = baseUrl + "/Media/" + AppConfig.pbPlaceholder;
                }
            }
            return apiData;
        }

        public async Task<List<User>> Followers(string id)
        {
            var response = await _client.GetAsync("API/Users/FollowedBy?UserID=" + id);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<User>>(content);

            foreach (var user in apiData)
            {
                if (user.pbFileName != null)
                {
                    user.pbFileName = baseUrl + "/Media/" + user.pbFileName;
                }
                else
                {
                    user.pbFileName = baseUrl + "/Media/" + AppConfig.pbPlaceholder;
                }
            }
            return apiData;
        }

        public async Task<List<User>> SearchUsers(string search)
        {
            var response = await _client.GetAsync("API/Users/Search?searchString=" + search);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<User>>(content);

            foreach (var user in apiData)
            {
                if (user.pbFileName != null)
                {
                    user.pbFileName = baseUrl + "/Media/" + user.pbFileName;
                }
                else
                {
                    user.pbFileName = baseUrl + "/Media/" + AppConfig.pbPlaceholder;
                }
            }
            return apiData;
        }

        public async Task<List<User>> BlockedUsers()
        {
            var response = await _client.GetAsync("API/Users/Blocking");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<List<User>>(content);

            foreach (var user in apiData)
            {
                if (user.pbFileName != null)
                {
                    user.pbFileName = baseUrl + "/Media/" + user.pbFileName;
                }
                else
                {
                    user.pbFileName = baseUrl + "/Media/" + AppConfig.pbPlaceholder;
                }
            }
            return apiData;
        }

        public async Task<HttpResponseMessage> FollowUser(string id)
        {
            var response = await _client.PostAsync("API/Users/Following/Add?userToFollowId=" + id, null);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> UnfollowUser(string id)
        {
            var response = await _client.PostAsync("API/Users/Following/Remove?userToUnfollowId=" + id, null);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> BlockUser(string id)
        {
            var response = await _client.PostAsync("API/Users/Blocking/Add?userToBlockId=" + id, null);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> UnblockUser(string id)
        {
            var response = await _client.PostAsync("API/Users/Blocking/Remove?userToUnblockId=" + id, null);
            return response.EnsureSuccessStatusCode();
        }
    }
}
