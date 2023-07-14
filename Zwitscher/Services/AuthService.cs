using System;
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
        public static string profilePicture = AppConfig.pbPlaceholderUrl;
        private readonly HttpClient _client;


        public AuthService()
        {
            _client = AppConfig.GetHttpClient();
        }

        // Diese Methode wird aufgerufen, wenn der Benutzer sich einloggt. Dafür wird der Username und das Passwort in den Body der Anfrage gepackt.
        // Die Antwort wird in ein LoginUser Objekt umgewandelt und zurückgegeben. Bei einem erfolgreichen Login wird der aktive Benutzer gesetzt und
        // das Profilbild geladen.
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
                profilePicture = MediaConverter.ChangeProfilePath(apiData.ProfilePicture);
            }

            return apiData;
        }

        // Diese Methode wird aufgerufen, wenn der Benutzer sich registriert. Dafür werden alle Daten in den Body der Anfrage gepackt.
        // Falls die Registrierung erfolgreich war, wird der aktive Benutzer gesetzt und das Profilbild geladen. Ein weiterer Login ist nicht nötig.
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

        // Diese Methode loggt den Benutzer aus und setzt den aktiven Benutzer auf null.
        public async Task<HttpResponseMessage> Logout()
        {
            activeUser = null;
            var response = await _client.PostAsync("Api/Logout", null);
            return response.EnsureSuccessStatusCode();
        }

        // Über diese Methode wird der aktive Benutzer abgefragt und zurückgegeben. Dabei werden auch weitere Daten wie die UserID ermittelt.
        public async Task<LoginUser> GetActiveUser()
        {
            var response = await _client.GetAsync("Api/UserDetails");
            string content = await response.Content.ReadAsStringAsync();
            var apiData = JsonSerializer.Deserialize<LoginUser>(content);
            activeUser = apiData;
            return apiData;
        }

        // Diese Methode überprüft, ob der aktive Benutzer den übergebene Benutzernamen hat.
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
