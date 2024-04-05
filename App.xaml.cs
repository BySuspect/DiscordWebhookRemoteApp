namespace DiscordWebhookRemoteApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //converting open beta webhooks no normal webhooks
            var betaWebhooks = Preferences.Get("SavedWebhooksBeta", null);
            if (betaWebhooks != null)
            {
                Preferences.Set("SavedWebhooks", betaWebhooks);
                Preferences.Remove("SavedWebhooksBeta");
            }

            MainPage = new AppShell();
            //MainPage = new TestPage();
        }
    }
}
