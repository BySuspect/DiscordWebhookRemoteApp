using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.SavedWebhooksView;
using Newtonsoft.Json;

namespace DiscordWebhookRemoteApp.Services
{
    public static class SavedWebhooksService
    {
        public static List<SavedWebhookViewItems> SavedWebhookList
        {
            get
            {
                return JsonConvert.DeserializeObject<List<SavedWebhookViewItems>>(
                    Preferences.Get("SavedWebhooksBeta", "[]")
                );
            }
            set
            {
                string json = JsonConvert.SerializeObject(value);
                Preferences.Set("SavedWebhooksBeta", json);
            }
        }
    }
}
