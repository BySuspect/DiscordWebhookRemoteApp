/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-windows10.0.19041.0)'
Before:
using CommunityToolkit.Maui.Views;
using Discord;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
After:
using CommunityToolkit.Maui.Views;

using Discord;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-maccatalyst)'
Before:
using CommunityToolkit.Maui.Views;
using Discord;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
After:
using CommunityToolkit.Maui.Views;

using Discord;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-ios)'
Before:
using CommunityToolkit.Maui.Views;
using Discord;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
After:
using CommunityToolkit.Maui.Views;

using Discord;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
*/
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;

namespace DiscordWebhookRemoteApp.Components.Popups.Message;

public partial class MessagePreviewPopup : Popup
{
    public MessagePreviewPopup(
        string message,
        string userName,
        string? avatar = null,
        Discord.Embed? embed = null,
        List<FileSendViewItems>? files = null
    )
    {
        InitializeComponent();

        lblUserName.Text = userName;

        if (string.IsNullOrWhiteSpace(avatar))
            imageAvatar.Source = "discordlogo.png";
        else
            imageAvatar.Source = avatar;

        if (string.IsNullOrWhiteSpace(message))
            messageView.IsVisible = false;
        else
            messageView.Message = message;

        if (embed is null)
            embedView.IsVisible = false;
        else
            embedView.setupEmbed(embed);

        if (files is null)
            filesView.IsVisible = false;
        else
            filesView.setupFiles(files);
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Close();
    }

    private void btnSend_Clicked(object sender, EventArgs e)
    {
        Close("Send");
    }
}
