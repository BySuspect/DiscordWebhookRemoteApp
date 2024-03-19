namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;

public partial class MessageContentView : ContentView
{
    public static readonly BindableProperty MessageContentProperty = BindableProperty.Create(
        nameof(MessageContent),
        typeof(string),
        typeof(MessageContentView)
    );
    public string MessageContent
    {
        get { return (string)GetValue(MessageContentProperty); }
        set { SetValue(MessageContentProperty, value); }
    }

    public MessageContentView()
    {
        InitializeComponent();
        BindingContext = this;
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
