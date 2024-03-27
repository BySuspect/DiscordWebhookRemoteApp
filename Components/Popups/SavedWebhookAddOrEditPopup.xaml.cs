using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.SavedWebhooksView;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordWebhookRemoteApp.Components.Popups;

public partial class SavedWebhookAddOrEditPopup : Popup
{
    private bool isEditMode = false;
    private SavedWebhookView editWebhook;

    public SavedWebhookAddOrEditPopup()
    {
        InitializeComponent();
    }

    public void NewMode()
    {
        isEditMode = false;
        entryWebhookUrl.Text = "";
        entryImageUrl.Text = "";
        entryName.Text = "";
    }

    public void EditMode(SavedWebhookViewItems item)
    {
        isEditMode = true;
        entryWebhookUrl.Text = item.WebhookUrl;
        entryImageUrl.Text =
            item.ImageSource == "discordlogo.png" ? string.Empty : item.ImageSource;
        entryName.Text = item.Name;
    }

    private void Save_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(entryWebhookUrl.Text.Trim()))
            return;

        var webhook = new SavedWebhookViewItems
        {
            WebhookUrl = entryWebhookUrl.Text.Trim(),
            ImageSource = string.IsNullOrEmpty(entryImageUrl.Text.Trim())
                ? "discordlogo.png"
                : entryImageUrl.Text.Trim(),
            Name = string.IsNullOrEmpty(entryName.Text.Trim()) ? "Webhook" : entryName.Text.Trim(),
        };
        Close(webhook);
    }

    private async void WebhookUrl_TextComplated(object sender, EventArgs e)
    {
        try
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(entryWebhookUrl.Text);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<JObject>(content);

                var avatarData = json["avatar"].ToString();
                if (!string.IsNullOrEmpty(avatarData))
                {
                    var avatar =
                        $"https://cdn.discordapp.com/avatars/{json["id"]}/{avatarData}.png";
                    entryImageUrl.Text = avatar;
                }

                var nameData = json["name"].ToString();
                if (!string.IsNullOrEmpty(nameData))
                {
                    entryName.Text = nameData;
                }
            }
        }
        catch { }
    }
}
