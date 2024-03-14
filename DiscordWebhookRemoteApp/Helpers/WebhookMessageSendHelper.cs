using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Webhook;

namespace DiscordWebhookRemoteApp.Helpers
{
    public class WebhookMessageSendHelper
    {
        private string WebhookUrl = "",
            UserName = "",
            ProfileImage = "";

        private DiscordWebhookClient Client;

        public WebhookMessageSendHelper(
            string url,
            string userName = null,
            string profileImage = null
        )
        {
            WebhookUrl = url;
            UserName = userName;
            ProfileImage = profileImage;
            Client = new DiscordWebhookClient(WebhookUrl);
        }

        public async Task<ulong> SendMessageAsync(string Message, List<Embed> embeds = null)
        {
            try
            {
                return await Client.SendMessageAsync(
                    text: Message,
                    isTTS: false,
                    embeds: null,
                    username: UserName,
                    avatarUrl: ProfileImage,
                    options: null,
                    allowedMentions: null,
                    components: null,
                    flags: MessageFlags.None,
                    threadId: null,
                    threadName: null,
                    appliedTags: null
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
