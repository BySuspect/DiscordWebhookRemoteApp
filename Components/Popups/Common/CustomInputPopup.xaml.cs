using CommunityToolkit.Maui.Views;

namespace DiscordWebhookRemoteApp.Components.Popups.Common;

public partial class CustomInputPopup : Popup
{
    public CustomInputPopup(
        string title,
        string input,
        string placeholder,
        string ok,
        string? cancel = null,
        int inputMaxLength = 0,
        bool hasTitle = false,
        bool hasCharCounterText = false
    )
    {
        InitializeComponent();
        lblTitle.Text = title;
        entryInput.Text = input.Trim();
        entryInput.Placeholder = placeholder.Trim();
        entryInput.HasTitle = hasTitle;
        entryInput.HasCharCounterText = hasCharCounterText;
        if (inputMaxLength is not 0)
            entryInput.MaxLength = inputMaxLength.ToString();

        btnOk.Text = ok;
        if (cancel != null)
        {
            btnCancel.Text = cancel;
        }
        else
        {
            btnCancel.IsVisible = false;
        }
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }

    private async void btnOk_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(entryInput.Text);
    }
}
