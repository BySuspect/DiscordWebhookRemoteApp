using Discord;
using Discord.Webhook;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.SavedWebhooksView;

using Newtonsoft.Json;

namespace DiscordWebhookRemoteApp.Services
{
    public class WebhookService
    {
        public static List<SavedWebhookViewItems> SavedWebhookList
        {
            get
            {
                return JsonConvert.DeserializeObject<List<SavedWebhookViewItems>>(
                    Preferences.Get("SavedWebhooks", "[]")
                );
            }
            set
            {
                string json = JsonConvert.SerializeObject(value);
                Preferences.Set("SavedWebhooks", json);
            }
        }

        public static async Task ImportSavedWebhoks(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                    return;

                byte[] bytes = Convert.FromBase64String(input);
                string jsonstring = System.Text.Encoding.UTF8.GetString(bytes);

                var _list = JsonConvert.DeserializeObject<List<SavedWebhookViewItems>>(jsonstring);

                var savedWebhooks = SavedWebhookList;
                savedWebhooks.AddRange(_list);
                SavedWebhookList = savedWebhooks;
            }
            catch (Exception ex)
            {
                ApplicationService.ShowCustomAlert(
                    "Error!",
                    "Something went wrong while trying importin webhooks.\nError Message: "
                        + ex.Message,
                    "Ok"
                );

                var logContent = new { Exception = ex, };
                await LoggingService.Log(
                    JsonConvert.SerializeObject(logContent),
                    LoggingService.LogLevel.Error,
                    "ImportSavedWebhoks error"
                );
            }
            return;
        }

        public static string ExportSavedWebhoks()
        {
            var webhooks = JsonConvert.SerializeObject(SavedWebhookList);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(webhooks);
            string base64String = Convert.ToBase64String(bytes);

            return base64String;
        }

        private class OldSavedWebhookItems
        {
            public int ID { get; set; }
            public string url { get; set; }
            public string name { get; set; }
        }

        public class MessageSend
        {
            private string webhookUrl = string.Empty;
            private string? userName = null;
            private string? avatarImage = null;

            private DiscordWebhookClient client;

            public MessageSend(string url, string? userName = null, string? avatarImage = null)
            {
                webhookUrl = url.Trim();
                this.userName = !string.IsNullOrEmpty(userName.Trim()) ? userName.Trim() : null;
                this.avatarImage = !string.IsNullOrEmpty(avatarImage.Trim())
                    ? avatarImage.Trim()
                    : null;
                client = new DiscordWebhookClient(webhookUrl);
            }

            public async Task<ulong?> SendMessageAsync(
                string Message,
                List<Embed>? embeds = null,
                List<FileAttachment>? files = null
            )
            {
                try
                {
                    if (embeds != null && embeds.Count >= 1 && files != null)
                    {
                        return await client.SendFilesAsync(
                            attachments: files,
                            text: Message,
                            username: userName,
                            avatarUrl: avatarImage,
                            embeds: embeds
                        );
                    }
                    else if (files != null)
                    {
                        return await client.SendFilesAsync(
                            attachments: files,
                            text: Message,
                            username: userName,
                            avatarUrl: avatarImage
                        );
                    }
                    else if (embeds != null && embeds.Count >= 1)
                    {
                        return await client.SendMessageAsync(
                            text: Message,
                            username: userName,
                            avatarUrl: avatarImage,
                            embeds: embeds
                        );
                    }
                    else
                    {
                        return await client.SendMessageAsync(
                            text: Message,
                            username: userName,
                            avatarUrl: avatarImage
                        );
                    }

                    return null;
                    //return await Client.SendMessageAsync(
                    //    text: Message,
                    //    isTTS: false,
                    //    embeds: null,
                    //    username: UserName,
                    //    avatarUrl: ProfileImage,
                    //    options: null,
                    //    allowedMentions: null,
                    //    components: null,
                    //    flags: MessageFlags.None,
                    //    threadId: null,
                    //    threadName: null,
                    //    appliedTags: null
                    //);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
