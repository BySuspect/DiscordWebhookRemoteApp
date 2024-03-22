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
        private string WebhookUrl = "";
        private string? UserName = null;
        private string? AvatarImage = null;

        private DiscordWebhookClient Client;

        public WebhookSendHelper(string url, string? userName = null, string? avatarImage = null)
        {
            WebhookUrl = url.Trim();
            UserName = !string.IsNullOrEmpty(userName.Trim()) ? userName.Trim() : null;
            AvatarImage = !string.IsNullOrEmpty(avatarImage.Trim()) ? avatarImage.Trim() : null;
            Client = new DiscordWebhookClient(WebhookUrl);
        }

        public async Task<ulong?> SendMessageAsync(string Message, List<Embed> embeds = null)
        {
            try
            {
                if (embeds != null && embeds.Count >= 1)
                {
                    return await Client.SendMessageAsync(
                        text: Message,
                        username: UserName,
                        avatarUrl: AvatarImage,
                        embeds: embeds
                    );
                }
                else
                {
                    return await Client.SendMessageAsync(
                        text: Message,
                        username: UserName,
                        avatarUrl: AvatarImage
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
