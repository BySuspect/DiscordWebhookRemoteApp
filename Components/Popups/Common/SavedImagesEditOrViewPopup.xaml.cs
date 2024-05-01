using System.Text.RegularExpressions;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.InputBehaviors;

namespace DiscordWebhookRemoteApp.Components.Popups.Common;

public partial class SavedImagesEditOrViewPopup : Popup
{
    public SavedImagesEditOrViewPopup(string imageUrl, string type = null)
    {
        InitializeComponent();
        entryImageUrl.Text = imageUrl;

        if (type is "View")
            btnLoad.IsVisible = false;
    }

    private async void Load_Clicked(object sender, EventArgs e)
    {
        btnLoad.IsEnabled = false;
        await CloseAsync(
            new SavedImagesEditOrViewPopupResult(SavedImagesEditOrViewPopupResultTypes.Select, null)
        );
        btnLoad.IsEnabled = true;
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        btnSave.IsEnabled = false;
        await CloseAsync(
            new SavedImagesEditOrViewPopupResult(
                SavedImagesEditOrViewPopupResultTypes.Save,
                entryImageUrl.Text
            )
        );
        btnSave.IsEnabled = true;
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        btnDelete.IsEnabled = false;
        await CloseAsync(
            new SavedImagesEditOrViewPopupResult(SavedImagesEditOrViewPopupResultTypes.Delete, null)
        );
        btnDelete.IsEnabled = true;
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

    public class SavedImagesEditOrViewPopupResult
    {
        public SavedImagesEditOrViewPopupResult(
            SavedImagesEditOrViewPopupResultTypes resultType,
            string? imageUrl
        )
        {
            ResultType = resultType;
            ImageUrl = imageUrl;
        }

        public SavedImagesEditOrViewPopupResultTypes ResultType { get; set; }
        public string? ImageUrl { get; set; }
    }

    public enum SavedImagesEditOrViewPopupResultTypes
    {
        Select,
        Save,
        Delete
    }
}
