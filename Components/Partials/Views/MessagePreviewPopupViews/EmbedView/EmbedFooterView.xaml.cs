namespace DiscordWebhookRemoteApp.Components.Partials.Views.MessagePreviewPopupViews.EmbedView;

public partial class EmbedFooterView : ContentView
{
    public string FooterIcon
    {
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                ImgView.IsVisible = false;
            }
            else
            {
                ImgView.Source = value;
            }
        }
    }

    public string FooterText
    {
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                lblFooterText.IsVisible = false;
            else
                lblFooterText.Text = value;
        }
    }

    public string Timestamp
    {
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                lblTimestamp.IsVisible = false;
            else
                lblTimestamp.Text = value;
        }
    }

    public EmbedFooterView()
    {
        InitializeComponent();
    }
}
