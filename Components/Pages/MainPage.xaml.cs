using CommunityToolkit.Maui.Alerts;
using Discord;
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
            sendHelper = new WebhookSendHelper(
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
                _ = App.Current.MainPage.DisplayAlert("Success.", "Message Sent", "Ok");
                //_ = Toast.Make("Message Sent").Show();
            }
            else
            {
                _ = App.Current.MainPage.DisplayAlert("Warning!", "Message Not Sent", "Ok");
                //_ = Toast.Make("Message Not Sent").Show();
            }
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

    private Task<(List<Discord.Embed>, bool)> BuildEmbed()
    {
        List<Discord.Embed> embeds = new List<Embed>();
        bool hasEmbeds = false;

        if (EmbedsView.Embeds.Count() == 0)
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
                            (byte)embed.BodyColor.Red,
                            (byte)embed.BodyColor.Green,
                            (byte)embed.BodyColor.Blue
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
