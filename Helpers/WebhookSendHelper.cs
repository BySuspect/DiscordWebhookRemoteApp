/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-windows10.0.19041.0)'
Before:
using System;
After:
using Discord;
using Discord.Webhook;
using System;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-maccatalyst)'
Before:
using System;
After:
using Discord;
using Discord.Webhook;
using System;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-ios)'
Before:
using System;
After:
using Discord;
using Discord.Webhook;
using System;
*/
using
/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-windows10.0.19041.0)'
Before:
using System.Threading.Tasks;
using Discord;
using Discord.Webhook;
After:
using System.Threading.Tasks;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-maccatalyst)'
Before:
using System.Threading.Tasks;
using Discord;
using Discord.Webhook;
After:
using System.Threading.Tasks;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-ios)'
Before:
using System.Threading.Tasks;
using Discord;
using Discord.Webhook;
After:
using System.Threading.Tasks;
*/
Discord;
using Discord.Webhook;

namespace DiscordWebhookRemoteApp.Helpers
{
    public class WebhookSendHelper
    {
        private string webhookUrl = "";
        private string? userName = null;
        private string? avatarImage = null;

        private DiscordWebhookClient client;

        public WebhookSendHelper(string url, string? userName = null, string? avatarImage = null)
        {
            webhookUrl = url.Trim();
            this.userName = !string.IsNullOrEmpty(userName.Trim()) ? userName.Trim() : null;
            this.avatarImage = !string.IsNullOrEmpty(avatarImage.Trim())
                ? avatarImage.Trim()
                : null;
            client = new DiscordWebhookClient(webhookUrl);
        }

        public async Task<ulong?> SendMessageAsync(string Message, List<Embed> embeds = null)
        {
            try
            {
                if (embeds != null && embeds.Count >= 1)
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
