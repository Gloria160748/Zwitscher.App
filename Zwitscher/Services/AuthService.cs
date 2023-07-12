﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Zwitscher.Models;

namespace Zwitscher.Services
{
    public class AuthService
    {
        public static LoginUser activeUser = null;
        public static string profilePicture = AppConfig.ApiUrl + "/Media/" + AppConfig.pbPlaceholder;
        private readonly HttpClient _client;


        public AuthService()
        {
            _client = AppConfig.GetHttpClient();
        }


        public async Task<LoginUser> Login(string username, string password)
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return null;
            }
            var credentials = new MultipartFormDataContent
            {
                { new StringContent(username), "Username" },
                { new StringContent(password), "Password" }
            };


            HttpResponseMessage response = await _client.PostAsync("Api/Login", credentials);
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize <LoginUser>(content);

            if (apiData != null && apiData.Success)
            {
                activeUser = await GetActiveUser();
                profilePicture = AppConfig.ApiUrl + "/Media/" + apiData.ProfilePicture;
            }

            return apiData;
        }

        public async Task<LoginUser> Register(string LastName, String FirstName, int Gender, String Username, String Password, DateTime Birthday)
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return null;
            }
            var credentials = new MultipartFormDataContent
            {
                { new StringContent(LastName), "LastName" },
                { new StringContent(FirstName), "FirstName" },
                { new StringContent(Gender.ToString()), "Gender" },
                { new StringContent(Username), "Username" },
                { new StringContent(Password), "Password" },
                { new StringContent(Birthday.ToString("yyyy-MM-dd")), "Birthday" }
            };
            HttpResponseMessage response = await _client.PostAsync("Api/Register", credentials);
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<LoginUser>(content);

            if (apiData != null && apiData.Success)
            {
                activeUser = apiData;
            }

            return apiData;

        }

        public async Task<HttpResponseMessage> Logout()
        {
            activeUser = null;
            var response = await _client.PostAsync("Api/Logout", null);
            return response.EnsureSuccessStatusCode();
        }

        public async Task<LoginUser> GetActiveUser()
        {
            var response = await _client.GetAsync("Api/UserDetails");
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<LoginUser>(content);
            activeUser = apiData;
            return apiData;
        }

        public bool IsActiveUser(string username)
        {
            if (activeUser == null)
            {
                return false;
            }
            else if (activeUser.Username == username)
            {
                return true;
            }
            return false;
        }
    }
}
