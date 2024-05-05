using System.Text.RegularExpressions;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.InputBehaviors;
using Microsoft.Maui;

namespace DiscordWebhookRemoteApp.Components.Popups.Common;

public partial class ImageEditAndViewPopup : Popup
{
    bool editMode = false;

    public ImageEditAndViewPopup(string imageUrl, string type)
    {
        InitializeComponent();
        ImgView.Source = imageUrl;
        entryImageUrl.Text = imageUrl;

        if (type is "New")
            btnLoad.IsVisible = false;

        if (type is "View")
        {
            btnLoad.IsVisible = false;
            editMode = true;
        }

        if (type is "Edit")
            editMode = true;

        if (type is "Select")
            btnDelete.IsVisible = false;
    }

    private async void Load_Clicked(object sender, EventArgs e)
    {
        btnLoad.IsEnabled = false;

        if (editMode)
        {
            await CloseAsync(
                new ImagesEditOrViewPopupResult(ImagesEditOrViewPopupResultTypes.Select, null)
            );
        }
        else
        {
            var res = await ApplicationService.ShowPopupAsync(new SavedImagesViewPopup());
            if (res != null)
            {
                entryImageUrl.Text = (string)res;
            }
        }

        btnLoad.IsEnabled = true;
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        btnSave.IsEnabled = false;
        if (editMode)
        {
            await CloseAsync(
                new ImagesEditOrViewPopupResult(
                    ImagesEditOrViewPopupResultTypes.Save,
                    entryImageUrl.Text
                )
            );
        }
        else
            await CloseAsync(entryImageUrl.Text);

        btnSave.IsEnabled = true;
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        btnDelete.IsEnabled = false;

        if (editMode)
        {
            var resDelete = await ApplicationService.ShowCustomAlertAsync(
                "Warning",
                "Are you sure you want to delete the current image?",
                "Yes",
                "No"
            );

            if (!resDelete)
            {
                btnDelete.IsEnabled = true;
                return;
            }
            await CloseAsync(
                new ImagesEditOrViewPopupResult(ImagesEditOrViewPopupResultTypes.Delete, null)
            );
        }
        else
        {
            await CloseAsync();
        }
        btnDelete.IsEnabled = true;
    }

    private void entryImageUrl_TextChanged(object sender, TextChangedEventArgs e)
    {
        ImgView.Source = e.NewTextValue;
    }
}

public class ImagesEditOrViewPopupResult
{
    public ImagesEditOrViewPopupResult(
        ImagesEditOrViewPopupResultTypes resultType,
        string? imageUrl
    )
    {
        ResultType = resultType;
        ImageUrl = imageUrl;
    }

    public ImagesEditOrViewPopupResultTypes ResultType { get; set; }
    public string? ImageUrl { get; set; }
}

public enum ImagesEditOrViewPopupResultTypes
{
    Select,
    Save,
    Delete
}
