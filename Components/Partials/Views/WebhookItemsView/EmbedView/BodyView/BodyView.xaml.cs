namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.BodyView;

public partial class BodyView : ContentView
{
    #region BodyTitle Binding
    public string BodyTitle
    {
        get { return entryBodyTitle.Text; }
        set
        {
            if (entryBodyTitle.Text != value)
            {
                entryBodyTitle.Text = value;
            }
        }
    }
    #endregion

    #region BodyContent Binding
    public string BodyContent
    {
        get { return editorBodyContent.Text; }
        set
        {
            if (editorBodyContent.Text != value)
            {
                editorBodyContent.Text = value;
            }
        }
    }
    #endregion

    #region BodyUrl Binding
    public string BodyUrl
    {
        get { return entryBodyUrl.Text; }
        set
        {
            if (entryBodyUrl.Text != value)
            {
                entryBodyUrl.Text = value;
            }
        }
    }
    #endregion

    #region BodyColor Binding
    public Color BodyColor
    {
        get
        {
            if (!string.IsNullOrEmpty(entryBodyColor.Text))
                return Color.FromHex(entryBodyColor.Text);
            else
                return new Color(
                    Discord.Color.Default.R,
                    Discord.Color.Default.G,
                    Discord.Color.Default.B
                );
        }
        set
        {
            if (Color.FromHex(entryBodyColor.Text) != value)
            {
                entryBodyColor.Text = value.ToHex();
            }
        }
    }
    #endregion
    public BodyView()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
