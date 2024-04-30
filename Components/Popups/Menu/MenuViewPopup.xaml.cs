using CommunityToolkit.Maui.Views;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.SavedWebhooksView;
using DiscordWebhookRemoteApp.Components.Popups.Common;
using DiscordWebhookRemoteApp.Components.Popups.Embed;

namespace DiscordWebhookRemoteApp.Components.Popups.Menu;

public partial class MenuViewPopup : Popup
{
    private SavedWebhooksView savedWebhooksView;
    public MenuViewPopup(SavedWebhooksView swView = null)
    {
        InitializeComponent();
        savedWebhooksView = swView;
    }

    private void Close_Tapped(object sender, TappedEventArgs e)
    {
        Close();
    }

    private void btnSavedImages_Clicked(object sender, EventArgs e)
    {
        ApplicationService.ShowPopup(new SavedImagesViewPopup("View"));
    }

    private void btnSavedEmbeds_Clicked(object sender, EventArgs e)
    {
        ApplicationService.ShowPopup(new SavedEmbedsViewPopup("View"));
    }

    private async void btnImportOldWebhooks_Clicked(object sender, EventArgs e)
    {
        this.Close();
        var webhooksData = await App.Current.MainPage.DisplayPromptAsync(
                title: "Import Webhooks",
                message: "Paste your webhooks data here",
                placeholder: "Webhooks Data"
            );
        if (webhooksData != null)
        {
            await WebhookService.ImportSavedWebhoksFromOldApp(webhooksData);
            await savedWebhooksView.RefreshList();
        }
    }
}