namespace DiscordWebhookRemoteApp.Services
{
    public static class ImageService
    {
        public static bool isImage(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            if (
                url.EndsWith(".png")
                || url.EndsWith(".jpg")
                || url.EndsWith(".jpeg")
                || url.EndsWith(".gif")
            )
                return true;

            return false;
        }
    }
}
