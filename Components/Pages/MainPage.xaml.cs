using CommunityToolkit.Maui.Alerts;
using Discord;
using DiscordWebhookRemoteApp.Components.Popups.Common;
using DiscordWebhookRemoteApp.Components.Popups.Embed;

namespace DiscordWebhookRemoteApp.Components.Pages;

public partial class MainPage : ContentPage
{
    WebhookService.MessageSend sendHelper;

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
        base.OnAppearing();

        if (!Preferences.Get("PrivacyPolicyV1Accepted", false))
            ApplicationService.ShowPopup(new PrivacyPolicyPopup());

        /*ApplicationService.ShowPopup(
            new CustomInputPopup("title", "input", "placeholder", "ok", "cancel", 24, true, true)
        );/**/
    }

    private async void SendButton_Clicked(object sender, EventArgs e)
    {
        if (SavedWebhooksView.selectedWebhook is null)
        {
            ApplicationService.ShowCustomAlert("Warning!", "Please select a webhook.", "Ok");
            return;
        }

        btnSend.IsEnabled = false;
        ApplicationService.ShowLoadingView();
        try
        {
            ulong? result = null;

            var embedBuild = await BuildEmbed();
            var embeds = embedBuild.Item1;
            var hasEmbeds = embedBuild.Item2;

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
            sendHelper = new WebhookService.MessageSend(
                uri,
                WebhookProfileView.Username,
                WebhookProfileView.AvatarImageSource
            );
            // Send Message if has embed
            if (hasEmbeds)
                result = await sendHelper.SendMessageAsync(
                    !string.IsNullOrEmpty(MessageContentView.MessageContent)
                        ? MessageContentView.MessageContent
                        : string.Empty,
                    embeds,
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
            {
                ApplicationService.HideLoadingView();
                var resSave = await ApplicationService.ShowCustomAlertAsync(
                    "Success.",
                    "Message Sent.",
                    "OK"
                );
                if (resSave)
                {
                    Console.WriteLine("Save Message");
                }
            }
            else
            {
                ApplicationService.ShowCustomAlert("Warning!", "Message Not Sent.", "Ok");
            }
        }
        catch (Exception ex)
        {
            ApplicationService.ShowCustomAlert("Send Error!", ex.Message, "Ok");
        }
        btnSend.IsEnabled = true;
        ApplicationService.HideLoadingView();
    }

    private async void btnClear_Clicked(object sender, EventArgs e)
    {
        var res = await ApplicationService.ShowCustomAlertAsync(
            "Clear All",
            "Are you sure you want to clear all content?",
            "Yes",
            "No"
        );
        if (!res)
            return;

        await MessageContentView.ClearMessage();
        await EmbedsView.ClearEmbeds();
        await FileSendView.ClearFiles();
    }

    private async void btnTest_Clicked(object sender, EventArgs e)
    {
        try
        {
            Console.WriteLine("Test Clicked");
            var res = await ApplicationService.ShowPopupAsync(new EmbedNewAndSelectPopup());
            if (res is null)
                return;

            Console.WriteLine(res);
        }
        catch (Exception ex)
        {
            ApplicationService.ShowCustomAlert("Test Error!", ex.Message, "Ok");
        }
    }

    private Task<(List<Discord.Embed>, bool)> BuildEmbed()
    {
        List<Discord.Embed> embeds = new List<Embed>();
        bool hasEmbeds = false;

        if (EmbedsView.Embeds.Count() is 0)
            return Task.FromResult((new List<Discord.Embed>(), false));

        foreach (var embed in EmbedsView.Embeds)
        {
            if (!embed.IsEmpty)
            {
                embeds.Add(
                    new Discord.EmbedBuilder()
                    {
                        Title = (!string.IsNullOrEmpty(embed.BodyTitle)) ? embed.BodyTitle : null,
                        Description =
                            (!string.IsNullOrEmpty(embed.BodyContent)) ? embed.BodyContent : null,
                        Url = (!string.IsNullOrEmpty(embed.BodyUrl)) ? embed.BodyUrl : null,
                        ImageUrl =
                            (!string.IsNullOrEmpty(embed.ImagesImageUrl))
                                ? embed.ImagesImageUrl
                                : null,
                        ThumbnailUrl =
                            (!string.IsNullOrEmpty(embed.ImagesThumbnailUrl))
                                ? embed.ImagesThumbnailUrl
                                : null,
                        Color = new Discord.Color(
                            (byte)Math.Round(embed.BodyColor.Red * 255),
                            (byte)Math.Round(embed.BodyColor.Green * 255),
                            (byte)Math.Round(embed.BodyColor.Blue * 255)
                        ),
                        Timestamp = embed.FooterTimestamp ? DateTime.Now : null,
                        Author = new Discord.EmbedAuthorBuilder()
                        {
                            Name =
                                (!string.IsNullOrEmpty(embed.AuthorName)) ? embed.AuthorName : null,
                            Url = (!string.IsNullOrEmpty(embed.AuthorUrl)) ? embed.AuthorUrl : null,
                            IconUrl =
                                (!string.IsNullOrEmpty(embed.AuthorIcon)) ? embed.AuthorIcon : null
                        },
                        Fields = embed
                            .Fields.Select(f => new Discord.EmbedFieldBuilder()
                            {
                                Name = f.Name,
                                Value = f.Value,
                                IsInline = f.InLine
                            })
                            .ToList(),
                        Footer = new Discord.EmbedFooterBuilder()
                        {
                            Text =
                                (!string.IsNullOrEmpty(embed.FooterTitle))
                                    ? embed.FooterTitle
                                    : null,
                            IconUrl =
                                (!string.IsNullOrEmpty(embed.FooterIcon)) ? embed.FooterIcon : null
                        },
                    }.Build()
                );
                hasEmbeds = true;
            }
        }

        return Task.FromResult((embeds, hasEmbeds));
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

    private async void Settings_Clicked(object sender, EventArgs e)
    {
        var res = await App.Current.MainPage.DisplayActionSheet(
            "Settings",
            string.Empty,
            "Cancel",
            "Import Webhooks"
        );
        if (res is "Import Webhooks")
        {
            var webhooksData = await App.Current.MainPage.DisplayPromptAsync(
                title: "Import Webhooks",
                message: "Paste your webhooks data here",
                placeholder: "Webhooks Data"
            );
            if (webhooksData != null)
            {
                await WebhookService.ImportSavedWebhoksFromOldApp(webhooksData);
                await SavedWebhooksView.RefreshList();
            }
        }
    }
}
