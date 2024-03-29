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
        defaultValue: ""
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
    public static readonly BindableProperty UsernameProperty = BindableProperty.Create(
        nameof(Username),
        typeof(string),
        typeof(WebhookProfileView),
        defaultBindingMode: BindingMode.TwoWay,
        defaultValue: ""
    );
    public string Username
    {
        get { return (string)GetValue(UsernameProperty); }
        set
        {
            SetValue(UsernameProperty, value);
            OnPropertyChanged(nameof(Username));
        }
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
