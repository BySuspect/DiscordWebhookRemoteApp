using
/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-maccatalyst)'
Before:
using CommunityToolkit.Maui.Views;
using Discord;
using DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;
After:
using CommunityToolkit.Maui.Views;

using Discord;

using DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-ios)'
Before:
using CommunityToolkit.Maui.Views;
using Discord;
using DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;
After:
using CommunityToolkit.Maui.Views;

using Discord;

using DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-windows10.0.19041.0)'
Before:
using CommunityToolkit.Maui.Views;
using Discord;
using DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;
After:
using CommunityToolkit.Maui.Views;

using Discord;

using DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;
*/
Discord;
using CommunityToolkit.Maui.Alerts;
using DiscordWebhookRemoteApp.Helpers;

namespace DiscordWebhookRemoteApp.Components.Pages;

public partial class MainPage : ContentPage
{
    WebhookSendHelper sendHelper;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
#if !DEBUG
        btnTest.IsVisible = false;
#endif
    }

    protected override void OnAppearing()
    {
#if !DEBUG
        Application.Current.MainPage.DisplayAlert(
            "Important Notice",
            "you're using the beta version of app, your saved data might get deleted or significant changes could be made in future updates!",
            "OK"
        );
#endif
        //ApplicationService.ShowPopup(new EmbedFieldsEditAndNewPopup());
        base.OnAppearing();
    }

    private async void SendButton_Clicked(object sender, EventArgs e)
    {
        if (SavedWebhooksView.selectedWebhook == null)
        {
            _ = Toast.Make("Please select a webhook").Show();
            return;
        }

        btnSend.IsEnabled = false;
        ApplicationService.ShowLoadingView();
        try
        {
            ulong? result = null;

            var embedBuild = await BuildEmbed();
            var embed = embedBuild.Item1;
            var hasEmbed = embedBuild.Item2;

            var _files = new List<FileAttachment>();

            foreach (var file in FileSendView.SelectedFiles)
            {
                _files.Add(new FileAttachment(file.Path));
            }

            string uri = SavedWebhooksView.selectedWebhook.WebhookUrl;

            if (string.IsNullOrEmpty(uri))
            {
                btnSend.IsEnabled = true;
                return;
            }
            sendHelper = new WebhookSendHelper(
                uri,
                WebhookProfileView.Username,
                WebhookProfileView.AvatarImageSource
            );

            // Send Message if has embed
            if (hasEmbed)
                result = await sendHelper.SendMessageAsync(
                    !string.IsNullOrEmpty(MessageContentView.MessageContent)
                        ? MessageContentView.MessageContent
                        : string.Empty,
                    new List<Embed>() { embed },
                    _files.Count >= 1 ? _files : null
                );
            // Send Message if has files and message
            else if (!string.IsNullOrEmpty(MessageContentView.MessageContent))
                result = await sendHelper.SendMessageAsync(
                    !string.IsNullOrEmpty(MessageContentView.MessageContent)
                        ? MessageContentView.MessageContent
                        : string.Empty,
                    null,
                    _files.Count >= 1 ? _files : null
                );
            // Send Message if has files
            else if (_files.Count >= 1)
                result = await sendHelper.SendMessageAsync(string.Empty, null, _files);

            if (result != null)
                _ = Toast.Make("Message Sent").Show();
            else
                _ = Toast.Make("Message Not Sent").Show();
        }
        catch (Exception ex)
        {
            _ = DisplayAlert("Send Error", ex.Message, "Ok");
        }
        btnSend.IsEnabled = true;
        ApplicationService.HideLoadingView();
    }

    private async void btnTest_Clicked(object sender, EventArgs e)
    {
        try
        {
            Console.WriteLine("Test Clicked");
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
            Fields = EmbedView
                .Fields.Select(f => new Discord.EmbedFieldBuilder()
                {
                    Name = f.Name,
                    Value = f.Value,
                    IsInline = f.InLine
                })
                .ToList(),
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
            || embed.Fields.Count() >= 1
        )
            hasEmbed = true;
        return Task.FromResult((embed, hasEmbed));
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
        if (res == "Open Link")
        {
            _ = Browser.OpenAsync(discorInviteShorten, BrowserLaunchMode.SystemPreferred);
        }
        else if (res == "Copy Link")
        {
            await Clipboard.SetTextAsync(discorInvite);
        }
        #endregion
    }

    private async void Settings_Clicked(object sender, EventArgs e)
    {
        var res = await App.Current.MainPage.DisplayActionSheet(
            "Settings",
            string.Empty,
            "Cancel",
            "Import Webhooks"
        );
        if (res == "Import Webhooks")
        {
            var webhooksData = await App.Current.MainPage.DisplayPromptAsync(
                title: "Import Webhooks",
                message: "Paste your webhooks data here",
                placeholder: "Webhooks Data"
            );
            if (webhooksData != null)
            {
                await SavedWebhooksService.ImportSavedWebhoksFromOldApp(webhooksData);
                await SavedWebhooksView.RefreshList();
            }
        }
    }
}
