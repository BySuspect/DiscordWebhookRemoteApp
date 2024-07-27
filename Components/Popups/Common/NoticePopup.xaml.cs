using CommunityToolkit.Maui.Views;

namespace DiscordWebhookRemoteApp.Components.Popups.Common
{
    public partial class NoticePopup : Popup
    {
        public NoticePopup(string message)
        {
            InitializeComponent();
            lblMsg.Text = message;
        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Discord_Clicked(object sender, EventArgs e)
        {
            string discorInvite = "https://discord.gg/aX4unxzZek";

            Browser.OpenAsync(discorInvite, BrowserLaunchMode.SystemPreferred);
        }
    }
}
