using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
