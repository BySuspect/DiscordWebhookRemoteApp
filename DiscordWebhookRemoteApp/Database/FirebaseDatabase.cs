using DiscordWebhookRemoteApp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordWebhookRemoteApp.Database
{
    public class FirebaseDatabase
    {
        public FirebaseDatabase()
        {

        }
        public static async Task<DatabaseItems> GetData()
        {
            try
            {
                string content = string.Empty;
                var httpClient = new HttpClient();
                HttpResponseMessage httpResponse = null;
                httpResponse = await httpClient.GetAsync("https://discordwebhookremoteapp-default-rtdb.firebaseio.com/.json?auth=qN0lqSdZLlTMUUDuCZaXOkHszStdhvVSdKHvMycK");
                if (httpResponse.IsSuccessStatusCode)
                {
                    content = await httpResponse.Content.ReadAsStringAsync();
                }
                else
                {
                    content = await httpResponse.Content.ReadAsStringAsync();
                }
                return JsonConvert.DeserializeObject<DatabaseItems>(content);
            }
            catch { return null; }
        }
    }
    public class DatabaseItems
    {
        public Settings Settings { get; set; }
    }

    public class Settings
    {
        public TestingMode TestingMode { get; set; }
    }

    public class TestingMode
    {
        public bool IsOnTest { get; set; }
        public string Version { get; set; }
    }

}
