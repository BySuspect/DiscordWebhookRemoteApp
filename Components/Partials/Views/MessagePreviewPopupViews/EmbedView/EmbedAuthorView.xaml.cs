namespace DiscordWebhookRemoteApp.Components.Partials.Views.MessagePreviewPopupViews.EmbedView;

public partial class EmbedAuthorView : ContentView
{
    public string Avatar
    {
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                ImgView.IsVisible = false;

            ImgView.Source = value;
        }
    }

    public string AuthorText
    {
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                lblAuthorText.IsVisible = false;

            lblAuthorText.Text = value;
        }
    }

    public EmbedAuthorView()
    {
        InitializeComponent();
    }
}
