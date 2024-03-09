using DiscordWebhookRemoteApp.Components.Pages;
using Xamarin.Forms;

namespace DiscordWebhookRemoteApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            //Preferences.Set("TestWebhookUrl", "url");
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
