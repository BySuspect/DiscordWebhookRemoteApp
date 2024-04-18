namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;

public partial class MessageContentView : ContentView
{
    public string MessageContent
    {
        get { return MesageContentEditor.Text; }
        set { MesageContentEditor.Text = value; }
    }

    public MessageContentView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public Task ClearMessage()
    {
        MessageContent = string.Empty;
        return Task.CompletedTask;
    }
}
