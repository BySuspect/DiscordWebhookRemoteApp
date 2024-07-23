using CommunityToolkit.Maui.Views;

namespace DiscordWebhookRemoteApp.Components.Popups.Common;

public partial class CustomAlertPopup : Popup
{
    public CustomAlertPopup(string title, string content, string ok, string? cancel = null)
    {
        InitializeComponent();
        lblTitle.Text = title;
        lblContent.Text = content.Trim();
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
        await CloseAsync(false);
    }

    private async void btnOk_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(true);
    }
}
