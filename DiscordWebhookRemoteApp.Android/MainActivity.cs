using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using MarcTron.Plugin;

namespace DiscordWebhookRemoteApp.Droid
{
    [Activity(
        Label = "Discord Webhook Remote",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize
            | ConfigChanges.Orientation
            | ConfigChanges.UiMode
            | ConfigChanges.ScreenLayout
            | ConfigChanges.SmallestScreenSize
    )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.DateTimePopups.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CrossMTAdmob.Current.Init(this, "ca-app-pub-3881259676793306~9834360439");

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions,
            [GeneratedEnum] Android.Content.PM.Permission[] grantResults
        )
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(
                requestCode,
                permissions,
                grantResults
            );

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
