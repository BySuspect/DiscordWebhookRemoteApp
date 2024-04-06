using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.SavedWebhooksView;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordWebhookRemoteApp.Components.Popups.Webhook;

public partial class SavedWebhookAddOrEditPopup : Popup
{
    private bool isEditMode = false;
    private SavedWebhookView editWebhook;
    private string webhookUrl;

    public SavedWebhookAddOrEditPopup()
    {
        InitializeComponent();
    }

    public void NewMode()
    {
        isEditMode = false;
        entryWebhookUrl.Text = string.Empty;
        entryImageUrl.Text = string.Empty;
        entryName.Text = string.Empty;
    }

    public void EditMode(SavedWebhookViewItems item)
    {
        isEditMode = true;
        webhookUrl = item.WebhookUrl;

        entryWebhookUrl.Text = item.WebhookUrl;
        entryImageUrl.Text =
            item.ImageSource == "discordlogo.png" ? string.Empty : item.ImageSource;
        entryName.Text = item.Name;
    }

    private void Save_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(entryWebhookUrl.Text.Trim()))
            return;
        btnSave.IsEnabled = false;
        var webhook = new SavedWebhookViewItems
        {
            WebhookUrl = entryWebhookUrl.Text.Trim(),
            ImageSource = string.IsNullOrEmpty(entryImageUrl.Text.Trim())
                ? "discordlogo.png"
                : entryImageUrl.Text.Trim(),
            Name = string.IsNullOrEmpty(entryName.Text.Trim()) ? "Webhook" : entryName.Text.Trim(),
        };
        Close(webhook);
        btnSave.IsEnabled = true;
    }

    private async void WebhookUrl_TextComplated(object sender, EventArgs e)
    {
        try
        {
            if (
                (
                    string.IsNullOrEmpty(entryWebhookUrl.Text.Trim())
                    || !entryWebhookUrl.Text.Trim().Contains("discord.com/api/webhooks/")
                )
                && entryWebhookUrl.Text != webhookUrl
            )
                return;

            entryWebhookUrl.IsEnabled = false;
            entryImageUrl.IsEnabled = false;
            entryName.IsEnabled = false;

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(entryWebhookUrl.Text.Trim());
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<JObject>(content);

                var avatarData = json?["avatar"]?.ToString();
                if (!string.IsNullOrEmpty(avatarData))
                {
                    var avatar =
                        $"https://cdn.discordapp.com/avatars/{json?["id"]}/{avatarData}.png";
                    entryImageUrl.Text = avatar;
                }

                var nameData = json?["name"]?.ToString();
                if (!string.IsNullOrEmpty(nameData))
                {
                    entryName.Text = nameData;
                }
            }
            else
                ApplicationService.ShowCustomAlert("Error!", "Webhook URL is not valid!", "OK");
        }
        catch { }

        entryWebhookUrl.IsEnabled = true;
        entryImageUrl.IsEnabled = true;
        entryName.IsEnabled = true;
    }
}
