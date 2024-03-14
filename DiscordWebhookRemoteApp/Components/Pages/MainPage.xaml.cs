using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
            btnSend.IsEnabled = false;
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
                //var embed = new EmbedBuilder
                //{
                //    Author = new EmbedAuthorBuilder()
                //    {
                //        Name = EmbedView.AuthorName,
                //        IconUrl = EmbedView.AuthorIconUrl,
                //        Url = EmbedView.AuthorUrl
                //    },
                //}.Build();

                await SendHelper.SendMessageAsync(MessageContentView.Text);

                ToastController.ShowShortToast("Message sent successfully!");
            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Error", ex.Message, "OK");
            }
            btnSend.IsEnabled = true;
        }

        private void btnTest_Clicked(object sender, EventArgs e)
        {
            try
            {
                var embedAuthor = new EmbedAuthorBuilder()
                {
                    IconUrl = EmbedView.AuthorIconUrl,
                    Name = EmbedView.AuthorName,
                    Url = EmbedView.AuthorUrl
                }.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                var embedBody = new EmbedBuilder
                {
                    Title = EmbedView.BodyTitle,
                    Description = EmbedView.BodyContent,
                    Url = EmbedView.BodyUrl,
                    Color = new Discord.Color(
                        (byte)EmbedView.BodyColor.R,
                        (byte)EmbedView.BodyColor.G,
                        (byte)EmbedView.BodyColor.B
                    )
                }.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
