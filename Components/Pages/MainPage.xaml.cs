using System.Text;

using Discord;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
using DiscordWebhookRemoteApp.Components.Popups.Common;
using DiscordWebhookRemoteApp.Components.Popups.Menu;
using DiscordWebhookRemoteApp.Components.Popups.Message;
using DiscordWebhookRemoteApp.Helpers;

using Newtonsoft.Json;

namespace DiscordWebhookRemoteApp.Components.Pages;

public partial class MainPage : ContentPage
{
    WebhookService.MessageSend sendHelper;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
#if RELEASE
        btnTest.IsVisible = false;
#endif
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!Preferences.Get("PrivacyPolicyV1Accepted", false))
            ApplicationService.ShowPopup(new PrivacyPolicyPopup());

        /*ApplicationService.ShowPopup(
            new MessagePreviewPopup(
                "",
                "username",
                "",
                new EmbedBuilder
                {
                    Fields = new List<EmbedFieldBuilder>()
                    {
                        new EmbedFieldBuilder { Name = "name 1", Value = "value 1" },
                        new EmbedFieldBuilder { Name = "name 2", Value = "value 2" },
                        new EmbedFieldBuilder { Name = "name 3", Value = "value 3" },
                        new EmbedFieldBuilder { Name = "name 4", Value = "value 4" },
                    }
                }.Build(),
                new List<FileSendViewItems>()
                {
                    new FileSendViewItems()
                    {
                        Id = 1,
                        Extension = ".jpg",
                        FileName = "test.jpg",
                        FileSizeText = "1.2 MB",
                        FilePath = "https://i.imgur.com/niLjyNS.jpg"
                    },
                    new FileSendViewItems()
                    {
                        Id = 1,
                        Extension = ".jpg",
                        FileName = "test.jpg",
                        FileSizeText = "1.2 MB",
                        FilePath = "https://i.imgur.com/niLjyNS.jp"
                    }
                }
            )
        ); /**/
    }

    private async void SendButton_Clicked(object sender, EventArgs e)
    {
        btnSend.IsEnabled = false;
        ApplicationService.ShowLoadingView();
        List<int> errorCounter = new List<int>();
        try
        {
            ulong? result = null;

            var embedBuild = await BuildEmbed();
            var embeds = embedBuild.Item1;
            var hasEmbeds = embedBuild.Item2;
            errorCounter.Add(1);

            var _files = new List<FileAttachment>();

            foreach (var file in FileSendView.SelectedFiles)
            {
                _files.Add(new FileAttachment(file.FilePath));
            }
            errorCounter.Add(2);

            if (
                string.IsNullOrWhiteSpace(MessageContentView.MessageContent)
                && !hasEmbeds
                && _files.Count == 0
            )
            {
                ApplicationService.ShowCustomAlert("Warning!", "Message is Empty.", "Ok");
                btnSend.IsEnabled = true;
                ApplicationService.HideLoadingView();
                return;
            }

            errorCounter.Add(3);
            var preRes = await ApplicationService.ShowPopupAsync(
                new MessagePreviewPopup(
                    MessageContentView.MessageContent,
                    (
                        string.IsNullOrWhiteSpace(WebhookProfileView.Username)
                            ? (SavedWebhooksView.selectedWebhook is null)
                                ? "TEMP USERNAME"
                                : SavedWebhooksView.selectedWebhook.Name
                            : WebhookProfileView.Username
                    ),
                    WebhookProfileView.AvatarImageSource,
                    (hasEmbeds) ? embeds[0] : null,
                    FileSendView.SelectedFiles.ToList()
                )
            );

            errorCounter.Add(4);
            if (preRes is not "Send")
            {
                btnSend.IsEnabled = true;
                ApplicationService.HideLoadingView();
                return;
            }

            if (SavedWebhooksView.selectedWebhook is null)
            {
                ApplicationService.ShowCustomAlert("Warning!", "Please select a webhook.", "Ok");
                btnSend.IsEnabled = true;
                ApplicationService.HideLoadingView();
                return;
            }

            errorCounter.Add(5);
            string uri = SavedWebhooksView.selectedWebhook.WebhookUrl;

            if (string.IsNullOrEmpty(uri))
            {
                btnSend.IsEnabled = true;
                ApplicationService.HideLoadingView();
                return;
            }
            sendHelper = new WebhookService.MessageSend(
                uri,
                WebhookProfileView.Username,
                WebhookProfileView.AvatarImageSource
            );
            errorCounter.Add(6);
            // Send Message if has embed
            if (hasEmbeds)
            {
                errorCounter.Add(7);
                result = await sendHelper.SendMessageAsync(
                    !string.IsNullOrEmpty(MessageContentView.MessageContent)
                        ? MessageContentView.MessageContent
                        : string.Empty,
                    embeds,
                    _files.Count >= 1 ? _files : null
                );
            }
            // Send Message if has files and message
            else if (!string.IsNullOrEmpty(MessageContentView.MessageContent))
            {
                errorCounter.Add(8);
                result = await sendHelper.SendMessageAsync(
                    !string.IsNullOrEmpty(MessageContentView.MessageContent)
                        ? MessageContentView.MessageContent
                        : string.Empty,
                    null,
                    _files.Count >= 1 ? _files : null
                );
            }
            // Send Message if has files
            else if (_files.Count >= 1)
            {
                errorCounter.Add(9);
                result = await sendHelper.SendMessageAsync(string.Empty, null, _files);
            }

            if (result != null)
            {
                errorCounter.Add(10);
                Console.WriteLine(result);
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
                errorCounter.Add(11);
                ApplicationService.ShowCustomAlert("Warning!", "Message Not Sent.", "Ok");
            }
        }
        catch (Exception ex)
        {
            ApplicationService.ShowCustomAlert("Send Error!", ex.Message, "Ok");

            var logContent = new { Exception = ex, ErrorCounter = errorCounter, };
            await LoggingService.Log(
                JsonConvert.SerializeObject(logContent),
                LoggingService.LogLevel.Error,
                "message send error"
            );
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
            ApplicationService.ShowPopup(new PrivacyPolicyPopup());
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

    private void Menu_Tapped(object sender, TappedEventArgs e)
    {
        ApplicationService.ShowPopup(new MessagePreviewPopupViews(SavedWebhooksView));
    }
}
