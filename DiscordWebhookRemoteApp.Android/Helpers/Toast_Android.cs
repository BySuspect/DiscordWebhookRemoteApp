using Android.Widget;
using DiscordWebhookRemoteApp.Droid.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]

namespace DiscordWebhookRemoteApp.Droid.Helpers
{
    public class Toast_Android : DiscordWebhookRemoteApp.Helpers.Toast
    {
        public void ShowLong(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShowShort(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}
