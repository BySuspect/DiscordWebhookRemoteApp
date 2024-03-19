using CommunityToolkit.Maui.Alerts;
using Discord;
using DiscordWebhookRemoteApp.Helpers;

namespace DiscordWebhookRemoteApp.Components.Pages;

public partial class MainPage : ContentPage
{
    WebhookSendHelper SendHelper;

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
            ulong? result = null;

            var embedBuild = await BuildEmbed();
            var embed = embedBuild.Item1;
            var hasEmbed = embedBuild.Item2;

            string uri =
                (entryWebhookUri.Text == null)
                    ? Preferences.Get("TestWebhookUrl", "null")
                    : entryWebhookUri.Text ?? Preferences.Get("WebhookUrl", string.Empty);
            SendHelper = new WebhookSendHelper(
                uri,
                WebhookProfileView.Username,
                WebhookProfileView.AvatarImageSource
            );
            if (hasEmbed)
                result = await SendHelper.SendMessageAsync(
                    !string.IsNullOrEmpty(MessageContentView.Text) ? MessageContentView.Text : "",
                    new List<Embed>() { embed }
                );
            else if (!string.IsNullOrEmpty(MessageContentView.Text))
                result = await SendHelper.SendMessageAsync(
                    !string.IsNullOrEmpty(MessageContentView.Text) ? MessageContentView.Text : ""
                );
            if (result != null)
                Toast.Make("Message Sent");
            else
                Toast.Make("Message Not Sent");
        }
        catch (Exception ex)
        {
            _ = DisplayAlert("Send Error", ex.Message, "Ok");
        }
        btnSend.IsEnabled = true;
    }

    private async void btnTest_Clicked(object sender, EventArgs e)
    {
        try
        {
            var embedBuild = await BuildEmbed();
            var embed = embedBuild.Item1;
            var hasEmbed = embedBuild.Item2;

            string uri =
                (entryWebhookUri.Text == null)
                    ? Preferences.Get("TestWebhookUrl", "null")
                    : entryWebhookUri.Text;
            SendHelper = new WebhookSendHelper(
                uri,
                WebhookProfileView.Username,
                WebhookProfileView.AvatarImageSource
            );
            if (hasEmbed)
                await SendHelper.SendMessageAsync(
                    !string.IsNullOrEmpty(MessageContentView.Text) ? MessageContentView.Text : "",
                    new List<Embed>() { embed }
                );
            else if (!string.IsNullOrEmpty(MessageContentView.Text))
                await SendHelper.SendMessageAsync(
                    !string.IsNullOrEmpty(MessageContentView.Text) ? MessageContentView.Text : ""
                );
            else
                return;
        }
        catch (Exception ex)
        {
            _ = DisplayAlert("Send Error", ex.Message, "Ok");
        }
    }

    private Task<(Discord.Embed, bool)> BuildEmbed()
    {
        Discord.Embed embed = null;
        bool hasEmbed = false;

        embed = new Discord.EmbedBuilder()
        {
            Title = (!string.IsNullOrEmpty(EmbedView.BodyTitle)) ? EmbedView.BodyTitle : null,
            Description =
                (!string.IsNullOrEmpty(EmbedView.BodyContent)) ? EmbedView.BodyContent : null,
            Url = (!string.IsNullOrEmpty(EmbedView.BodyUrl)) ? EmbedView.BodyUrl : null,
            ImageUrl =
                (!string.IsNullOrEmpty(EmbedView.ImagesImageUrl)) ? EmbedView.ImagesImageUrl : null,
            ThumbnailUrl =
                (!string.IsNullOrEmpty(EmbedView.ImagesThumbnailUrl))
                    ? EmbedView.ImagesThumbnailUrl
                    : null,
            Color = new Discord.Color(
                (byte)EmbedView.BodyColor.Red,
                (byte)EmbedView.BodyColor.Green,
                (byte)EmbedView.BodyColor.Blue
            ),
            Timestamp = EmbedView.FooterTimestamp ? DateTime.Now : null,
            Author = new Discord.EmbedAuthorBuilder()
            {
                Name = (!string.IsNullOrEmpty(EmbedView.AuthorName)) ? EmbedView.AuthorName : null,
                Url = (!string.IsNullOrEmpty(EmbedView.AuthorUrl)) ? EmbedView.AuthorUrl : null,
                IconUrl =
                    (!string.IsNullOrEmpty(EmbedView.AuthorIconUrl))
                        ? EmbedView.AuthorIconUrl
                        : null
            },
            Footer = new Discord.EmbedFooterBuilder()
            {
                Text = (!string.IsNullOrEmpty(EmbedView.FooterText)) ? EmbedView.FooterText : null,
                IconUrl =
                    (!string.IsNullOrEmpty(EmbedView.FooterIconUrl))
                        ? EmbedView.FooterIconUrl
                        : null
            },
        }.Build();
        if (
            embed.Title != null
            || embed.Image.Value.Url != null
            || embed.Thumbnail.Value.Url != null
            || embed.Description != null
            || embed.Author.Value.Name != null
            || embed.Footer.Value.Text != null
        )
            hasEmbed = true;
        return Task.FromResult((embed, hasEmbed));
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
