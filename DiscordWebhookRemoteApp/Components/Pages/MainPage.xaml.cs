using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using Discord;
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
        WebhookMessageSendHelper SendHelper;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void SendButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string uri =
                    (entryWebhookUri.Text == null)
                        ? Preferences.Get("TestWebhookUrl", "null")
                        : entryWebhookUri.Text;
                SendHelper = new WebhookMessageSendHelper(
                    uri,
                    WebhookProfileView.Username,
                    WebhookProfileView.AvatarImageSource
                );
                var embed = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        Name = EmbedView.AuthorName,
                        IconUrl = EmbedView.AuthorIconUrl,
                        Url = EmbedView.AuthorUrl
                    },
                }.Build();

                await SendHelper.SendMessageAsync(MessageContentView.MessageContent);

                ToastController.ShowShortToast("Message sent successfully!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
