using CommunityToolkit.Maui.Views;

namespace DiscordWebhookRemoteApp.Components.Popups.Common;

public partial class PrivacyPolicyPopup : Popup
{
    public PrivacyPolicyPopup()
    {
        InitializeComponent();
        lblUp.Text =
            @"
Before you start using our application, we kindly ask you to read and accept our privacy policy. 
Our privacy policy provides detailed information on how we collect, use, and protect your personal data.
Please click on the link below to review and accept our privacy policy:" + "\n\n";
        lblDown.Text =
            "\n\n"
            + @"Continuing to use our application signifies that you have read and agreed to our privacy policy.
We appreciate your understanding of the importance we place on privacy and data security.
If you have any questions, please don't hesitate to contact us.

Best regards,
Shiroko";
    }

    private void btnOk_Clicked(object sender, EventArgs e)
    {
        Preferences.Set("PrivacyPolicyV1Accepted", true);
        Close();
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    private async void PrivacyPolicy_Tapped(object sender, TappedEventArgs e)
    {
        await Browser.OpenAsync(
            "https://apps.shiroko.dev/discordwebhookremoteapp/privacy-policy.html"
        );
    }
}
