using DiscordWebhookRemoteApp.Helpers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivacyPolicyPage : ContentPage
    {
        //policyi webden çekicek hale getiricem
        //ve deişiklik olduğu zaman policy son deiştirme tarihiyle locale kaydettim veriye göre kullanıcıya tekrar bildiricem
        public PrivacyPolicyPage()
        {
            InitializeComponent();
            if (Preferences.Get("privacy_policy_accepted", false))
                lblTitleChangedText.Text = "Privacy Policy is changed.";
            lblUp.Text = @"
Before you start using our application, we kindly ask you to read and accept our privacy policy. Our privacy policy provides detailed information on how we collect, use, and protect your personal data.

Please click on the link below to review and accept our privacy policy:" + "\n\n";
            lblDown.Text = "\n\n" + @"Continuing to use our application signifies that you have read and agreed to our privacy policy. We appreciate your understanding of the importance we place on privacy and data security.

If you have any questions, please don't hesitate to contact us.

Best regards,
Webhook Remote App Team";
        }
        private void btnPrivacyPolicyAccept(object sender, EventArgs e)
        {
            // Save preference indicating that the user has accepted the privacy policy
            Preferences.Set("privacy_policy_accepted17May2023", true);
            btnaccept.IsEnabled = false;
            App.Current.MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = ThemeColors.StatusBarColor,
                BarTextColor = ThemeColors.TextColor,
            };
        }

        private async void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://awgstudiosapps.web.app/discordwebhookremoteapp/privacy-policy.html");
        }
    }
}