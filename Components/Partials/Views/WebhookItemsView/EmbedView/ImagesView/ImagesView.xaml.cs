namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.ImagesView;

public partial class ImagesView : ContentView
{
    #region ImagesImageUrl Binding
    public string ImagesImageUrl
    {
        get { return entryImagesImageUrl.Text; }
        set
        {
            if (entryImagesImageUrl.Text != value)
            {
                entryImagesImageUrl.Text = value;
            }
        }
    }
    #endregion

    #region ImagesThumbnailUrl Binding
    public string ImagesThumbnailUrl
    {
        get { return entryImagesThumbnailUrl.Text; }
        set
        {
            if (entryImagesThumbnailUrl.Text != value)
            {
                entryImagesThumbnailUrl.Text = value;
            }
        }
    }
    #endregion
    public ImagesView()
    {
        InitializeComponent();
        BindingContext = this;
    }
}
