using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;
using Plugin.MauiMTAdmob;

namespace DiscordWebhookRemoteApp
{
    [Activity(
        Theme = "@style/Maui.SplashTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize
            | ConfigChanges.Orientation
            | ConfigChanges.UiMode
            | ConfigChanges.ScreenLayout
            | ConfigChanges.SmallestScreenSize
            | ConfigChanges.Density,
        ScreenOrientation = ScreenOrientation.Portrait
    )]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            MobileAds.Initialize(this);
            CrossMauiMTAdmob.Current.Init(this, "ca-app-pub-3881259676793306~9834360439");

            base.OnCreate(savedInstanceState);
        }
    }
}
