using Android.App;
using Android.OS;

namespace DiscordWebhookRemoteApp.Droid
{
    [Activity(Label = "Discord Webhook Remote", Icon = "@mipmap/icon", Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.StartActivity(typeof(MainActivity));
        }
    }
}