using DiscordWebhookRemoteApp.Components.Popups.Common;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.AuthorView;

public partial class AuthorView : ContentView
{
    #region AuthorIcon Binding
    public static readonly BindableProperty AuthorIconProperty = BindableProperty.Create(
        nameof(AuthorIcon),
        typeof(string),
        typeof(AuthorView),
        defaultBindingMode: BindingMode.TwoWay,
        defaultValue: "discordlogo.png"
    );
    public string AuthorIcon
    {
        get { return (string)GetValue(AuthorIconProperty); }
        set
        {
            SetValue(
                AuthorIconProperty,
                (string.IsNullOrWhiteSpace(value)) ? "discordlogo.png" : value
            );
            OnPropertyChanged(nameof(AuthorIcon));
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

    private async void Icon_Tapped(object sender, EventArgs e)
    {
        imgAuthorIcon.IsEnabled = false;
        var res = await ApplicationService.ShowPopupAsync(
            new ImageEditAndViewPopup(
                (AuthorIcon is "discordlogo.png") ? string.Empty : AuthorIcon,
                "Select"
            )
        );
        if (res != null)
        {
            AuthorIcon = (string)res;
        }
        imgAuthorIcon.IsEnabled = true;
    }
}
