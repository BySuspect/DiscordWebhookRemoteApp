using System.Text.RegularExpressions;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.InputBehaviors;

namespace DiscordWebhookRemoteApp.Components.Popups;

public partial class WebhookProfileImageEditAndViewPopup : Popup
{
    public WebhookProfileImageEditAndViewPopup(string imageUrl, bool isNew = true)
    {
        InitializeComponent();
        entryImageUrl.Text = imageUrl;
        if (isNew)
        {
            btnLoad.IsVisible = false;
        }
    }

    private async void Load_Clicked(object sender, EventArgs e)
    {
        btnLoad.IsVisible = false;
        var res = await ApplicationService.ShowPopupAsync(new SavedWebhookProfileImagesViewPopup());
        if (res != null)
        {
            entryImageUrl.Text = res?.ToString();
        }
        btnLoad.IsVisible = true;
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryImageUrl.Text))
            Close();
        await CloseAsync(((UriImageSource)ImageView.Source).Uri.AbsoluteUri);
    }

    private void entryImageUrl_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (
            Regex.IsMatch(
                e.NewTextValue,
                ImageUrlValidatorBehaviour.pattern,
                RegexOptions.IgnoreCase
            )
        )
        {
            ImageView.Source = e.NewTextValue;
        }
    }
}
