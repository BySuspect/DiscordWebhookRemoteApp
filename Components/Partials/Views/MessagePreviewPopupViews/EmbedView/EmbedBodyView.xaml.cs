namespace DiscordWebhookRemoteApp.Components.Partials.Views.MessagePreviewPopupViews.EmbedView;

public partial class EmbedBodyView : ContentView
{
    public string Title
    {
        set
        {
            if (string.IsNullOrEmpty(value))
                lblTitle.IsVisible = false;
            else
                lblTitle.Text = value;
        }
    }
    public string Description
    {
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                lblDescription.IsVisible = false;
            else
                lblDescription.Text = value;
        }
    }

    public EmbedBodyView()
    {
        InitializeComponent();
    }
}
