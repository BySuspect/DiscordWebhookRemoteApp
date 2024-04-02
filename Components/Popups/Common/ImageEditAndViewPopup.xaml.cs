using System.Text.RegularExpressions;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.InputBehaviors;

namespace DiscordWebhookRemoteApp.Components.Popups.Common;

public partial class ImageEditAndViewPopup : Popup
{
    public ImageEditAndViewPopup(string imageUrl, bool isNew = true)
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
        btnLoad.IsEnabled = false;
        var res = await ApplicationService.ShowPopupAsync(new SavedImagesViewPopup());
        if (res != null)
        {
            entryImageUrl.Text = res?.ToString();
        }
        btnLoad.IsEnabled = true;
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        btnSave.IsEnabled = false;
        await CloseAsync(entryImageUrl.Text);
        btnSave.IsEnabled = true;
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
