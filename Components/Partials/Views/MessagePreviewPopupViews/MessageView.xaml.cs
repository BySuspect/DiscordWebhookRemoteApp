namespace DiscordWebhookRemoteApp.Components.Partials.Views.MessagePreviewPopupViews;

public partial class MessageView : ContentView
{
    public string? Message
    {
        set { lblMessage.Text = value; }
    }

    public MessageView()
    {
        InitializeComponent();
    }
}
