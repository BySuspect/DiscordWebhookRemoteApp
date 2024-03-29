using System.Text.RegularExpressions;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.InputBehaviors;

namespace DiscordWebhookRemoteApp.Components.Popups;

public partial class SavedImagesEditOrViewPopup : Popup
{
    public SavedImagesEditOrViewPopup(string imageUrl)
    {
        InitializeComponent();
        entryImageUrl.Text = imageUrl;
    }

    private async void Load_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(
            new SavedImagesEditOrViewPopupResult(SavedImagesEditOrViewPopupResultTypes.Select, null)
        );
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(
            new SavedImagesEditOrViewPopupResult(
                SavedImagesEditOrViewPopupResultTypes.Save,
                entryImageUrl.Text
            )
        );
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(
            new SavedImagesEditOrViewPopupResult(SavedImagesEditOrViewPopupResultTypes.Delete, null)
        );
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
