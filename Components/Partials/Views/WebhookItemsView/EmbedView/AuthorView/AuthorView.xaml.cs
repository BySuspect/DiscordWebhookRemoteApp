namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.AuthorView;

public partial class AuthorView : ContentView
{
    #region AuthorIconUrl Binding
    public string AuthorIconUrl
    {
        get { return entryAuthorIconUrl.Text; }
        set
        {
            if (entryAuthorIconUrl.Text != value)
            {
                entryAuthorIconUrl.Text = value;
            }
        }
    }

    #endregion

    #region AuthorName Binding

    public string AuthorName
    {
        get { return entryAuthorName.Text; }
        set
        {
            if (entryAuthorName.Text != value)
            {
                entryAuthorName.Text = value;
            }
        }
    }

    #endregion

    #region AuthorUrl Binding
    public string AuthorUrl
    {
        get { return entryAuthorUrl.Text; }
        set
        {
            if (entryAuthorUrl.Text != value)
            {
                entryAuthorUrl.Text = value;
            }
        }
    }

    #endregion

    public AuthorView()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
