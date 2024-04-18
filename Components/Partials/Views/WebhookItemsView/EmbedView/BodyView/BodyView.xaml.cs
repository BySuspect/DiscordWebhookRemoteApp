using DiscordWebhookRemoteApp.Components.Popups.Embed;

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
        get { return bvColor.Color; }
        set
        {
            bvColor.Color = value;
            lblColorText.TextColor = ApplicationService.InvertColor(value);
        }
    }
    #endregion
    public BodyView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void BodyColor_Tapped(object sender, TappedEventArgs e)
    {
        var res = await ApplicationService.ShowPopupAsync(new ColorPickPopup(bvColor.Color));
        if (res is null)
            return;

        var selectedColor = (Microsoft.Maui.Graphics.Color)res;
        bvColor.Color = selectedColor;
        lblColorText.TextColor = ApplicationService.InvertColor(selectedColor);
    }
}
