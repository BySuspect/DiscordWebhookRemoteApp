using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                ApplicationService.ShowCustomAlert(
                    "Error!",
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
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
