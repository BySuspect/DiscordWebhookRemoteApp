﻿using DiscordWebhookRemoteApp.Droid.Helpers;
using Android.Widget;
[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]
namespace DiscordWebhookRemoteApp.Droid.Helpers
{
    public class Toast_Android : DiscordWebhookRemoteApp.Helpers.Toast
    {
        public void ShowLong(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
        public void ShowShort(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}