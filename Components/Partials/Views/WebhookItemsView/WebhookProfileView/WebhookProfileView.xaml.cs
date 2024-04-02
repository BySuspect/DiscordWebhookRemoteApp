using System.ComponentModel;
using DiscordWebhookRemoteApp.Components.Popups;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.WebhookProfileView;

public partial class WebhookProfileView : ContentView, INotifyPropertyChanged
{
    #region AvatarImageSource Binding
    public static readonly BindableProperty AvatarImageSourceProperty = BindableProperty.Create(
        nameof(AvatarImageSource),
        typeof(string),
        typeof(WebhookProfileView),
        defaultBindingMode: BindingMode.TwoWay,
        defaultValue: string.Empty
    );
    public string AvatarImageSource
    {
        get { return (string)GetValue(AvatarImageSourceProperty); }
        set
        {
            SetValue(AvatarImageSourceProperty, value);
            OnPropertyChanged(nameof(AvatarImageSource));
        }
    }
    #endregion

    #region Username Binding
    public string Username
    {
        get { return entryUsername.Text.Trim(); }
        set { entryUsername.Text = value; }
    }
    #endregion

    public WebhookProfileView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void Avatar_Tapped(object sender, EventArgs e)
    {
        avatarBtn.IsEnabled = false;
        var res = await ApplicationService.ShowPopupAsync(
            new ImageEditAndViewPopup(AvatarImageSource.ToString(), false)
        );
        if (res != null)
        {
            AvatarImageSource = (string)res;
        }
        avatarBtn.IsEnabled = true;
    }
}
