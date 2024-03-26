using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.SavedWebhooksView;

namespace DiscordWebhookRemoteApp.Components.Popups;

public partial class SavedWebhookAddOrEditPopup : Popup
{
    private bool isEditMode = false;
    private SavedWebhookView editWebhook;

    public SavedWebhookAddOrEditPopup()
    {
        InitializeComponent();
        isEditMode = false;
    }

    public SavedWebhookAddOrEditPopup(SavedWebhookView webhook)
    {
        InitializeComponent();
        isEditMode = true;
        editWebhook = webhook;
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        if (isEditMode) { }
        else
        {
            var webhook = new SavedWebhookViewItems
            {
                WebhookId = -1,
                WebhookUrl = entryWebhookUrl.Text,
                ImageSource = string.IsNullOrEmpty(entryImageUrl.Text) ? null : entryImageUrl.Text,
                Name = string.IsNullOrEmpty(entryName.Text) ? "Webhook" : entryName.Text,
            };
            await CloseAsync(webhook);
        }
    }

    private void WebhookUrl_TextComplated(object sender, EventArgs e)
    {
        Console.WriteLine(entryWebhookUrl.Text);
    }
}
