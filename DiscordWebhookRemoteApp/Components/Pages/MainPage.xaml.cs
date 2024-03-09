using System;
using Discord.Webhook;
using DiscordWebhookRemoteApp.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Components.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void SendButton_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                string uri =
                    (entryWebhookUri.Text == null)
                        ? Preferences.Get("TestWebhookUrl", "null")
                        : entryWebhookUri.Text;
                var client = new DiscordWebhookClient(uri);
                var res = await client.SendMessageAsync(
                    text: WebhookMessageContentView.MessageContent,
                    username: WebhookProfileView.Username,
                    avatarUrl: WebhookProfileView.AvatarImageSource
                );
                ToastController.ShowShortToast("Message sent successfully!");
            }
            catch { }
        }
    }
}
