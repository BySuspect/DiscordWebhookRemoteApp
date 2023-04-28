using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using Discord;
using Discord.Webhook;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using Xamarin.Forms.PlatformConfiguration;
using static System.Net.Mime.MediaTypeNames;
using static Xamarin.Essentials.AppleSignInAuthenticator;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using DiscordWebhookRemoteApp.Helpers;
using System.Diagnostics;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using DiscordWebhookRemoteApp.Pages.Popups;
using System.Collections.ObjectModel;

namespace DiscordWebhookRemoteApp.Helpers
{
    public static class References
    {
        public static bool supportPopup = true;
        public static string Version = "1.0.0";

        static List<webhookItems> _webhookList;
        public static List<webhookItems> WebhookList
        {
            get
            {
                _webhookList = JsonConvert.DeserializeObject<List<webhookItems>>(Preferences.Get("savedwebhookItems", "[]"));
                //_webhookList = _webhookList.OrderBy(x => x.ID).ToList();
                return _webhookList;
            }
            set
            {
                _webhookList = value;
                string json = JsonConvert.SerializeObject(value);
                Preferences.Set("savedwebhookItems", json);
            }
        }

        static ObservableCollection<webhookProfileItems> _webhookProfileList;
        public static ObservableCollection<webhookProfileItems> WebhookProfileList
        {
            get
            {
                string json = Preferences.Get("savedwebhookprofileItems", "[]");
                _webhookProfileList = JsonConvert.DeserializeObject<ObservableCollection<webhookProfileItems>>(json);
                //_webhookProfileList = _webhookProfileList.OrderBy(x => x.ID).ToList();
                Debug.WriteLine("webhook profile Request: \n" + json);
                return _webhookProfileList;
            }
            set
            {
                _webhookProfileList = value;
                string json = JsonConvert.SerializeObject(value);
                Preferences.Set("savedwebhookprofileItems", json);
                Debug.WriteLine("webhook profile Update: \n" + json);
            }
        }
    }
    public class webhookItems
    {
        public int ID { get; set; }
        public string url { get; set; }
        public string name { get; set; }
    }
    public class webhookProfileItems
    {
        public int ID { get; set; }
        public string image { get; set; }
        public string name { get; set; }
    }
    public static class ChangeAppTheme
    {
        public static void LightTheme()
        {
            ThemeColors.TextColor = Color.Black;
            ThemeColors.TransparentTextColor = Color.FromHex("#BA000000");
            ThemeColors.BorderColor = Color.Black;
            ThemeColors.BorderBackColor = Color.Transparent;
            ThemeColors.BackColor = Color.FromHex("#DBDBDB");
            ThemeColors.StatusBarColor = Color.FromHex("#FFFFFF");
            ThemeColors.StatusBarStyle = StatusBarStyle.DarkContent;
            ThemeColors.backgroundImg = null;
        }
        public static void DarkTheme()
        {
            ThemeColors.TextColor = Color.FromHex("#FFFFFF");
            ThemeColors.TransparentTextColor = Color.FromHex("#BAFFFFFF");
            ThemeColors.BorderColor = Color.FromHex("#BAFFFFFF");
            ThemeColors.BorderBackColor = Color.Transparent;
            ThemeColors.BackColor = Color.FromHex("#101010");
            ThemeColors.StatusBarColor = Color.FromHex("#000000");
            ThemeColors.StatusBarStyle = StatusBarStyle.LightContent;
            ThemeColors.backgroundImg = null;
        }
        public static void ForDenizTheme()
        {
            ThemeColors.TextColor = Color.FromHex("#000000");
            ThemeColors.TransparentTextColor = Color.FromHex("#BA470041");
            ThemeColors.BorderColor = Color.FromHex("#000000");
            ThemeColors.BorderBackColor = Color.FromHex("#55fc03d7");
            ThemeColors.BackColor = Color.FromHex("#fc03d7");
            ThemeColors.StatusBarColor = Color.FromHex("#fc03d7");
            ThemeColors.StatusBarStyle = StatusBarStyle.DarkContent;
            ThemeColors.backgroundImg = "wallpaperfordeniz.jpg";
        }
    }
    public static class ThemeColors
    {
        public static Color TextColor { get; set; }
        public static Color TransparentTextColor { get; set; }
        public static Color BorderColor { get; set; }
        public static Color BorderBackColor { get; set; }
        public static Color BackColor { get; set; }
        public static Color StatusBarColor { get; set; }
        public static StatusBarStyle StatusBarStyle { get; set; }
        public static ImageSource backgroundImg { get; set; }
    }
    public class ThemeChangedEventArgs : EventArgs
    {
        public ThemeChangedEventArgs(string newTheme)
        {
            NewTheme = newTheme;
        }

        public string NewTheme { get; private set; }
    }

    public delegate void ThemeChangedEventHandler(object sender, ThemeChangedEventArgs e);
    public static class Theme
    {
        public static event ThemeChangedEventHandler ThemeChanged;

        public static void OnThemeChanged(string newTheme)
        {
            ThemeChanged?.Invoke(null, new ThemeChangedEventArgs(newTheme));
        }
    }

}
