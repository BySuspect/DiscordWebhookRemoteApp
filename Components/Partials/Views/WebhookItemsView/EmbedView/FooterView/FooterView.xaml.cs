namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FooterView;

public partial class FooterView : ContentView
{
    #region FooterIconUrl Binding
    public string FooterIconUrl
    {
        get { return entryFooterIconUrl.Text; }
        set
        {
            if (entryFooterIconUrl.Text != value)
            {
                entryFooterIconUrl.Text = value;
            }
        }
    }

    #endregion

    #region FooterTitle Binding

    public string FooterText
    {
        get { return entryFooterText.Text; }
        set
        {
            if (entryFooterText.Text != value)
            {
                entryFooterText.Text = value;
            }
        }
    }

    #endregion

    #region FooterTimestamp Binding
    public bool FooterTimestamp
    {
        get { return cbTimestamp.IsChecked; }
        set
        {
            if (cbTimestamp.IsChecked != value)
            {
                cbTimestamp.IsChecked = value;
            }
        }
    }

    #endregion

    public FooterView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void TimestampCheckBoxLabelTapped(object sender, EventArgs e)
    {
        cbTimestamp.IsChecked = !cbTimestamp.IsChecked;
    }
}
