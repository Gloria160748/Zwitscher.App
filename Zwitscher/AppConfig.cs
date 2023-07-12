using System;
using System.Net.Http;

namespace Zwitscher
{
    public class AppConfig
    {
        public static string ApiUrl = "http://10.0.2.2:5141";
        public static string pbPlaceholder = "real-placeholder.png";
        public static HttpClient Client;

        public static HttpClient GetHttpClient()
        {
            if(Client == null)
            {
                Client = new HttpClient
                {
                    BaseAddress = new Uri(ApiUrl)
                };
            }

            return Client;
        }
    }
}
