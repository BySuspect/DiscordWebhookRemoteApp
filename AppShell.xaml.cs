namespace DiscordWebhookRemoteApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void Support_Clicked(object sender, EventArgs e)
        {
            string supportLink = "https://apps.shiroko.dev/supportus/";

            _ = Browser.OpenAsync(supportLink, BrowserLaunchMode.External);
        }

        private async void Discord_Clicked(object sender, EventArgs e)
        {
            #region Discord Invite Section
            string discorInviteShorten = "https://bit.ly/3NmBFDO";
            string discorInvite = "https://discord.gg/aX4unxzZek";

            _ = Browser.OpenAsync(discorInvite, BrowserLaunchMode.SystemPreferred);
            return;

            var res = await App.Current.MainPage.DisplayActionSheet(
                "Discord Invite",
                string.Empty,
                "Cancel",
                "Open Link",
                "Copy Link"
            );
            if (res is "Open Link")
            {
                _ = Browser.OpenAsync(discorInviteShorten, BrowserLaunchMode.SystemPreferred);
            }
            else if (res is "Copy Link")
            {
                await Clipboard.SetTextAsync(discorInvite);
            }
            #endregion
        }
    }
}
