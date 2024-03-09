using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DiscordWebhookRemoteApp.Helpers
{
    public static class ToastController
    {
        public static void ShowLongToast(string message)
        {
            DependencyService.Get<Toast>().ShowLong(message);
        }
        public static void ShowShortToast(string message)
        {
            DependencyService.Get<Toast>().ShowShort(message);
        }
    }
    public interface Toast
    {
        void ShowLong(string message);
        void ShowShort(string message);
    }
}
