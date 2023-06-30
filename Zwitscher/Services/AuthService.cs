using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Zwitscher.Models;

namespace Zwitscher.Services
{
    public class AuthService
    {
        private HttpClient _client;
        public static LoginUser activeUser = null;


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
                activeUser = apiData;
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

        public async void Logout()
        {
            activeUser = null;
            await _client.PostAsync("Api/Logout", null);
        }

        public async Task<LoginUser> GetActiveUser()
        {
            var response = await _client.GetAsync("Api/UserDetails");
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<LoginUser>(content);
            return apiData;
        }
    }
}
