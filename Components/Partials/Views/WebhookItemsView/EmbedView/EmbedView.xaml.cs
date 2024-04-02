namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView;

public partial class EmbedView : ContentView
{
    #region AuthorView
    public string AuthorIconUrl
    {
        get
        {
            return (_authorView.AuthorIcon == "discordlogo.png")
                ? string.Empty
                : _authorView.AuthorIcon;
        }
        set { _authorView.AuthorIcon = value; }
    }
    public string AuthorName
    {
        get { return _authorView.AuthorName; }
        set { _authorView.AuthorName = value; }
    }
    public string AuthorUrl
    {
        get { return _authorView.AuthorUrl; }
        set { _authorView.AuthorUrl = value; }
    }
    #endregion

    #region BodyView
    public string BodyTitle
    {
        get { return _bodyView.BodyTitle; }
        set { _bodyView.BodyTitle = value; }
    }
    public string BodyContent
    {
        get { return _bodyView.BodyContent; }
        set { _bodyView.BodyContent = value; }
    }
    public string BodyUrl
    {
        get { return _bodyView.BodyUrl; }
        set { _bodyView.BodyUrl = value; }
    }
    public Color BodyColor
    {
        get { return _bodyView.BodyColor; }
        set { _bodyView.BodyColor = value; }
    }
    #endregion

    #region ImagesView
    public string ImagesImageUrl
    {
        get { return _imagesView.ImagesImageUrl; }
        set { _imagesView.ImagesImageUrl = value; }
    }

    public string ImagesThumbnailUrl
    {
        get { return _imagesView.ImagesThumbnailUrl; }
        set { _imagesView.ImagesThumbnailUrl = value; }
    }
    #endregion

    #region FooterView
    public string FooterText
    {
        get { return _footerView.FooterText; }
        set { _footerView.FooterText = value; }
    }

    public string FooterIconUrl
    {
        get
        {
            return (_footerView.FooterIcon == "discordlogo.png")
                ? string.Empty
                : _footerView.FooterIcon;
        }
        set { _footerView.FooterIcon = value; }
    }

    public bool FooterTimestamp
    {
        get { return _footerView.FooterTimestamp; }
        set { _footerView.FooterTimestamp = value; }
    }
    #endregion

    public EmbedView()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
