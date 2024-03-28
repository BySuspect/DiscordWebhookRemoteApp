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

        public static Task ImportSavedWebhoksFromOldApp(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                    return Task.CompletedTask;

                byte[] bytes = Convert.FromBase64String(input);
                string jsonstring = System.Text.Encoding.UTF8.GetString(bytes);

                var json = JsonConvert.DeserializeObject<List<OldSavedWebhookItems>>(jsonstring);
                var _list = SavedWebhookList;
                foreach (var item in json)
                {
                    _list.Add(
                        new SavedWebhookViewItems()
                        {
                            WebhookId = (_list.Count > 0) ? _list.Last().WebhookId + 1 : 1,
                            WebhookUrl = item.url,
                            Name = item.name,
                            ImageSource = "discordlogo.png"
                        }
                    );
                }
                SavedWebhookList = _list;
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Something went wrong while trying importin webhooks.\nError Message: "
                        + ex.Message,
                    "Ok"
                );
            }
            return Task.CompletedTask;
        }

        private class OldSavedWebhookItems
        {
            public int ID { get; set; }
            public string url { get; set; }
            public string name { get; set; }
        }
    }
}
