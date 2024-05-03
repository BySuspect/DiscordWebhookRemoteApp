using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using Discord;
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
