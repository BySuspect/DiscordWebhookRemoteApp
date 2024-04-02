using DiscordWebhookRemoteApp.Components.Popups.Common;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FooterView;

public partial class FooterView : ContentView
{
    #region FooterIcon Binding
    public static readonly BindableProperty FooterIconProperty = BindableProperty.Create(
        nameof(FooterIcon),
        typeof(string),
        typeof(FooterView),
        defaultBindingMode: BindingMode.TwoWay,
        defaultValue: "discordlogo.png"
    );
    public string FooterIcon
    {
        get { return (string)GetValue(FooterIconProperty); }
        set
        {
            SetValue(
                FooterIconProperty,
                (string.IsNullOrWhiteSpace(value)) ? "discordlogo.png" : value
            );
            OnPropertyChanged(nameof(FooterIcon));
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

    private async void Icon_Tapped(object sender, EventArgs e)
    {
        iconBtn.IsEnabled = false;
        var res = await ApplicationService.ShowPopupAsync(
            new ImageEditAndViewPopup(
                (FooterIcon == "discordlogo.png") ? string.Empty : FooterIcon,
                false
            )
        );
        if (res != null)
        {
            FooterIcon = (string)res;
        }
        iconBtn.IsEnabled = true;
    }
}
