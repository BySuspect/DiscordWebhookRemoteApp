using Android.App;
using Android.OS;

namespace DiscordWebhookRemoteApp.Droid
{
    [Activity(Label = "Discord Webhook Remote", Theme = "@style/SplashTheme", Icon = "@mipmap/icon", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.StartActivity(typeof(MainActivity));
        }
    }
}