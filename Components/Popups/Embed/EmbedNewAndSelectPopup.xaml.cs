using CommunityToolkit.Maui.Views;

namespace DiscordWebhookRemoteApp.Components.Popups.Embed;

public partial class EmbedNewAndSelectPopup : Popup
{
    public EmbedNewAndSelectPopup()
    {
        InitializeComponent();
    }

    private async void btnLoad_Clicked(object sender, EventArgs e)
    {
        await CloseAsync("Load");
    }

    private async void btnNew_Clicked(object sender, EventArgs e)
    {
        await CloseAsync("New");
    }
}
