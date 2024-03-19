using System.ComponentModel;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.WebhookProfileView;

public partial class WebhookProfileView : ContentView, INotifyPropertyChanged
{
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

    public WebhookProfileView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void Avatar_Tapped(object sender, EventArgs e)
    {
        var res = await Application.Current.MainPage.DisplayPromptAsync(
            title: "Change Avatar",
            message: "Enter a new URL for the avatar",
            accept: "OK",
            cancel: "Cancel",
            placeholder: "https://example.com/image.jpg",
            initialValue: AvatarImageSource
        );
        if (res != null)
        {
            AvatarImageSource = res;
        }
    }

    private void Input_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.Length >= Input.MaxLength)
            lblInputLenght.TextColor = Colors.Red;
        else
            lblInputLenght.TextColor = Colors.White;

        spCharacterCount.Text = e.NewTextValue.Length.ToString();
    }
}
