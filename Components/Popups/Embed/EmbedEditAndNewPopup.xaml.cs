using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FieldsView;

namespace DiscordWebhookRemoteApp.Components.Popups.Embed;

public partial class EmbedEditAndNewPopup : Popup
{
    private bool isEditMode = false;

    public EmbedEditAndNewPopup(
        string authorIcon,
        string authorName,
        string authorUrl,
        string bodyTitle,
        string bodyContent,
        string bodyUrl,
        Color bodyColor,
        ObservableCollection<FieldView> fields,
        string imagesImageUrl,
        string imagesThumbnailUrl,
        string footerIcon,
        string footerTitle,
        bool footerTimestamp
    )
    {
        InitializeComponent();
        BindingContext = this;

        _authorView.AuthorIcon = authorIcon;
        _authorView.AuthorName = authorName;
        _authorView.AuthorUrl = authorUrl;
        _bodyView.BodyTitle = bodyTitle;
        _bodyView.BodyContent = bodyContent;
        _bodyView.BodyUrl = bodyUrl;
        _bodyView.BodyColor = bodyColor;
        _fieldsView.Fields = fields;
        _imagesView.ImagesImageUrl = imagesImageUrl;
        _imagesView.ImagesThumbnailUrl = imagesThumbnailUrl;
        _footerView.FooterIcon = footerIcon;
        _footerView.FooterTitle = footerTitle;
        _footerView.FooterTimestamp = footerTimestamp;
    }

    public EmbedEditAndNewPopup()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        if (!isEditMode)
        {
            Close("delete");
            return;
        }
        btnDelete.IsEnabled = false;
        var res = await ApplicationService.ShowCustomAlertAsync(
            "Warning!",
            $"Are you sure you want to delete Embed?",
            "Yes",
            "No"
        );
        if (res == null || !res)
        {
            btnDelete.IsEnabled = true;
            return;
        }
        if (res)
            Close("delete");
        btnDelete.IsEnabled = true;
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        bool isEmpty = true;
        if (
            !string.IsNullOrWhiteSpace(_authorView.AuthorName)
            || !string.IsNullOrWhiteSpace(_bodyView.BodyTitle)
            || !string.IsNullOrWhiteSpace(_bodyView.BodyContent)
            || !string.IsNullOrWhiteSpace(_imagesView.ImagesImageUrl)
            || !string.IsNullOrWhiteSpace(_footerView.FooterTitle)
        )
        {
            isEmpty = false;
        }
        else if (_fieldsView.Fields.Count > 0)
        {
            foreach (var field in _fieldsView.Fields)
            {
                if (!field.IsEmpty)
                {
                    isEmpty = false;
                }
            }
        }

        var _authorIcon = _authorView.AuthorIcon.Trim().Contains("discordlogo.png")
            ? string.Empty
            : _authorView.AuthorIcon.Trim();

        var _footerIcon = _footerView.FooterIcon.Trim().Contains("discordlogo.png")
            ? string.Empty
            : _footerView.FooterIcon.Trim();

        await CloseAsync(
            new EmbedView(
                -1,
                -1,
                _authorIcon,
                _authorView.AuthorName.Trim(),
                _authorView.AuthorUrl.Trim(),
                _bodyView.BodyTitle.Trim(),
                _bodyView.BodyContent.Trim(),
                _bodyView.BodyUrl.Trim(),
                _bodyView.BodyColor,
                _fieldsView.Fields,
                _imagesView.ImagesImageUrl.Trim(),
                _imagesView.ImagesThumbnailUrl.Trim(),
                _footerIcon,
                _footerView.FooterTitle.Trim(),
                _footerView.FooterTimestamp,
                isEmpty
            )
        );
    }
}
