﻿using Microsoft.AspNetCore.Http;
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
        private readonly HttpClient _client;
        private readonly string baseUrl = AppConfig.ApiUrl;
        private readonly AuthService authService = new AuthService();
        private readonly PostService postService = new PostService();

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

            apiData.pbFileName = MediaConverter.ChangeProfilePath(apiData.pbFileName);

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
                user.pbFileName = MediaConverter.ChangeProfilePath(user.pbFileName);
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
                user.pbFileName = MediaConverter.ChangeProfilePath(user.pbFileName);
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
                user.pbFileName = MediaConverter.ChangeProfilePath(user.pbFileName);
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
                user.pbFileName = MediaConverter.ChangeProfilePath(user.pbFileName);
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
                user.pbFileName = MediaConverter.ChangeProfilePath(user.pbFileName);
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
            var content = new MultipartFormDataContent
            {
                { new StringContent(user.userID), "userID" },
                { new StringContent(user.lastname), "LastName" },
                { new StringContent(user.firstname), "FirstName" },
                { new StringContent(user.username), "Username" },
                { new StringContent(user.password), "Password" },
                { new StringContent(user.birthday), "Birthday" },
                { new StringContent(user.biography), "Biography" },
                { new StringContent(user.gender), "Gender" }
            };

            var response = await _client.PostAsync("API/Users/Edit", content);
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
            var response = await _client.PostAsync("API/Users/Media/Remove?userID=" + userId + "&mediaToRemoveId=" + mediaId, null);
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
