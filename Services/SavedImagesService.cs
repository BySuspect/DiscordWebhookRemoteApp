using
/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-maccatalyst)'
Before:
using System.Threading.Tasks;
using Newtonsoft.Json;
After:
using System.Threading.Tasks;

using Newtonsoft.Json;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-ios)'
Before:
using System.Threading.Tasks;
using Newtonsoft.Json;
After:
using System.Threading.Tasks;

using Newtonsoft.Json;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-windows10.0.19041.0)'
Before:
using System.Threading.Tasks;
using Newtonsoft.Json;
After:
using System.Threading.Tasks;

using Newtonsoft.Json;
*/
Newtonsoft.Json;

namespace DiscordWebhookRemoteApp.Services
{
    public static class SavedImagesService
    {
        public static List<SavedImagesItems> SavedImages
        {
            get
            {
                return JsonConvert.DeserializeObject<List<SavedImagesItems>>(
                    Preferences.Get("SavedImages", "[]")
                );
            }
            set
            {
                string json = JsonConvert.SerializeObject(value);
                Preferences.Set("SavedImages", json);
            }
        }
    }

    public class SavedImagesItems
    {
        public required int Id { get; set; }
        public required string ImageUrl { get; set; }
    }
}
