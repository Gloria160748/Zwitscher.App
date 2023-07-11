using Microsoft.AspNetCore.Http;
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
        private AuthService authService = new AuthService();
        private PostService postService = new PostService();

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

            if (apiData.pbFileName != "")
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
                if (user.pbFileName != "")
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
            var response = await _client.GetAsync("API/Users/Posts?id=" + id);
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
                post.mediaIncluded = post.mediaList.Count > 0;
                post.isRetweet = post.retweetsPost != "";
                post.isOwnPost = authService.IsActiveUser(post.user_username);
                if (post.isRetweet)
                {
                    post.RetweetedPost = await postService.GetSinglePost(post.retweetsPost);
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
                if (user.pbFileName != "")
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
                if (user.pbFileName != "")
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
                if (user.pbFileName != "")
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
                if (user.pbFileName != "")
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

        public async Task<HttpResponseMessage> EditUser(User user)
        {
            var response = await _client.PutAsync("API/Users/Edit?userID=" + user.userID + "&LastName=" + user.lastname + "&FirstName=" +
                user.firstname + "&Username=" + user.username + "&Password=" + user.password + "&Birthday" + user.birthday + "&Biography=" +
                user.biography + "&Gender=" + user.gender, null);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            var response = await _client.DeleteAsync("API/Users/Remove?id=" + id);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> AddProfilePicture(string userId, IFormFile file)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(userId), "userID" },
                { new StreamContent(file.OpenReadStream()), "file", file.FileName }
            };

            var response = await _client.PostAsync("API/Users/Media/Add", content);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<HttpResponseMessage> RemoveProfilePicture(string userId, string mediaId)
        {
            var response = await _client.DeleteAsync("API/Users/Media/Remove?userID=" + userId + "&mediaToRemoveId=" + mediaId);
            return response.EnsureSuccessStatusCode();
        }


        public async Task<bool> ActiveUserFollows(string requestId)
        {
            var activeUser = AuthService.activeUser;
            if (activeUser == null || string.IsNullOrEmpty(activeUser.userID))
                return false;

            var followedUsers = await FollowedUsers(activeUser.userID);
            foreach (var user in followedUsers)
            {
                if (user.userID == requestId)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> ActiveUserBlocked(string requestId)
        {
            var activeUser = AuthService.activeUser;
            if (activeUser == null || string.IsNullOrEmpty(activeUser.userID))
                return false;

            var blockedUsers = await BlockedUsers();
            foreach (var user in blockedUsers)
            {
                if (user.userID == requestId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
