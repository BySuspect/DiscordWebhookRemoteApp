using Discord;
using Discord.Webhook;

namespace DiscordWebhookRemoteApp.Helpers
{
    public class WebhookSendHelper
    {
        private string webhookUrl = string.Empty;
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
