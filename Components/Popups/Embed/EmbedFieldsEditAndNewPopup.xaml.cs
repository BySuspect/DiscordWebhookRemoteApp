using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FieldsView;

namespace DiscordWebhookRemoteApp.Components.Popups.Embed;

public partial class EmbedFieldsEditAndNewPopup : Popup
{
    private bool isEditMode = false;

    public EmbedFieldsEditAndNewPopup(string name, string value, bool inLine)
    {
        InitializeComponent();

        entryTitle.Text = name;
        entryValue.Text = value;
        cbInline.IsChecked = inLine;
        isEditMode = true;

        BindingContext = this;
    }

    public EmbedFieldsEditAndNewPopup()
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
        var res = await Application.Current.MainPage.DisplayAlert(
            "Warning!",
            $"Are you sure you want to delete Field?",
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
        await CloseAsync(
            new FieldView(
                -1,
                -1,
                entryTitle.Text,
                entryValue.Text,
                cbInline.IsChecked,
                string.IsNullOrWhiteSpace(entryTitle.Text)
                    && string.IsNullOrWhiteSpace(entryValue.Text)
            )
        );
    }
}
