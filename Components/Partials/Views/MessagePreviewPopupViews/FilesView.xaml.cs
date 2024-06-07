/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-windows10.0.19041.0)'
Before:
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using Discord;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
After:
using System.Collections.ObjectModel;

using CommunityToolkit.Maui.Core.Extensions;

using Discord;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-maccatalyst)'
Before:
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using Discord;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
After:
using System.Collections.ObjectModel;

using CommunityToolkit.Maui.Core.Extensions;

using Discord;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-ios)'
Before:
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using Discord;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
After:
using System.Collections.ObjectModel;

using CommunityToolkit.Maui.Core.Extensions;

using Discord;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;
*/
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.MessagePreviewPopupViews;

public partial class FilesView : ContentView
{
    public FilesView()
    {
        InitializeComponent();
    }

    public void setupFiles(List<FileSendViewItems> files)
    {
        fileList.ItemsSource = files;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        //testImg.Source = "https://i.imgur.com/mcbAmuo.jpg";
    }
}
