using CommunityToolkit.Maui.Views;

namespace DiscordWebhookRemoteApp.Components.Popups.Message;

public partial class MessagePreviewPopup : Popup
{
    public MessagePreviewPopup(
        string message,
        string userName,
        string avatar = "",
        Discord.Embed embed = null
    )
    {
        InitializeComponent();

        lblUserName.Text = userName;

        if (string.IsNullOrWhiteSpace(avatar))
            imageAvatar.Source = "discordlogo.png";
        else
            imageAvatar.Source = avatar;

        if (string.IsNullOrWhiteSpace(message))
            messageView.IsVisible = false;
        else
            messageView.Message = message;

        if (embed is not null)
        {
            embedView.setupEmbed(embed);
            embedView.IsVisible = true;
        }
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Close();
    }

    private void btnSend_Clicked(object sender, EventArgs e)
    {
        Close("Send");
    }
}
