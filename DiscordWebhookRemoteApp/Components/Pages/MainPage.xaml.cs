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
            entryWebhookUri.Text = Preferences.Get("WebhookUrl", string.Empty);
#if !DEBUG
            btnTest.IsVisible = false;
#endif
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
            var test = Preferences.Get("WebhookUrl", string.Empty);
            try
            {
                var embedAuthor = new EmbedAuthorBuilder
                {
                    IconUrl = EmbedView.AuthorIconUrl,
                    Name = EmbedView.AuthorName,
                    Url = EmbedView.AuthorUrl
                }.Build();
            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Embed Author Error", ex.Message, "Ok");
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
                _ = DisplayAlert("Embed Body Error", ex.Message, "Ok");
            }

            try
            {
                var embedFooter = new EmbedFooterBuilder
                {
                    IconUrl = EmbedView.FooterIconUrl,
                    Text = EmbedView.FooterText
                }.Build();
                var timestamp = EmbedView.FooterTimestamp;
            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Embed Footer Error", ex.Message, "Ok");
            }
        }

        private void entryWebhookUri_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine(e.NewTextValue);
        }

        private void entryWebhookUri_TextComplated(object sender, EventArgs e)
        {
            var entry = (Entry)sender;
            Preferences.Set("WebhookUrl", entry.Text);
        }
    }
}
