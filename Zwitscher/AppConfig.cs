using System;
using System.Net.Http;

namespace Zwitscher
{
    public class AppConfig
    {
        public static string ApiUrl = "http://10.0.2.2:5141";
        public static string pbPlaceholder = "real-placeholder.png";
        public static string pbPlaceholderUrl = ApiUrl + "/Media/" + pbPlaceholder;
        public static HttpClient Client;

        // Über diese Variable besteht die Möglichkeit, die Video-Funktionalität zu deaktivieren.
        // Gerade bei der Simulation auf einem Emulator kann es sonst zu Problemen kommen, wenn zu viele Videos gleichzeitig abgespielt werden.
        public static bool disableVideo = true;

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
