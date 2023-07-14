using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace DiscordWebhookRemoteApp.Helpers
{
    public static class References
    {
        public static bool supportPopup = true;
        public static string Version = "1.0.4";

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
        public static async Task<bool> CheckConnection()
        {
            var client = new HttpClient();
            try
            {
                await client.GetAsync("https://www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string CurrentVersion { get; private set; }
        public static string ServerVersion { get; private set; }
        public static async Task CheckAppVersion()
        {
            // Mevcut sürüm numarasını al
            CurrentVersion = Version; // Kendi mevcut sürüm numaranızı buraya yazın

            // Sunucudan sürüm numarasını al
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync("https://awgstudiosapps.web.app/discordwebhookremoteapp/version.txt"); // Sürüm numarasını döndüren sunucu URL'sini buraya yazın
                    response.EnsureSuccessStatusCode();
                    ServerVersion = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda nasıl bir işlem yapmak istediğinizi burada belirtebilirsiniz
                Console.WriteLine("Sürüm numarası alınamadı: " + ex.Message);
                return;
            }

            // Sürüm numaralarını karşılaştır
            if (ServerVersion != CurrentVersion)
            {
                // Popup göster
#if !DEBUG
                await App.Current.MainPage.DisplayAlert("Update Available", "A new version is available. Please update the app.", "Ok");
#endif
            }
        }

        #region Discord Invite Section
        public static string discorInviteShorten = "https://bit.ly/3NmBFDO";
        public static string discorInvite = "https://discord.gg/aX4unxzZek";
        public static async void discordClicked()
        {
            var res = await App.Current.MainPage.DisplayActionSheet("Discord Invite", "", "Cancel", "Open Link", "Copy Link");
            if (res == "Open Link")
            {
                _ = Browser.OpenAsync(discorInviteShorten, BrowserLaunchMode.SystemPreferred);
            }
            else if (res == "Copy Link")
            {
                await Clipboard.SetTextAsync(discorInvite);
                ToastController.ShowShortToast("Discord Link Copied!");
            }
        }
        #endregion
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
            ThemeColors.TransparentTextColor = Color.FromHex("#BAAAAAAA");
            ThemeColors.BorderColor = Color.Black;
            ThemeColors.BorderBackColor = Color.FromHex("#40FFFFFF");
            ThemeColors.BackColor = Color.FromHex("#FFFFFF");
            ThemeColors.StatusBarColor = Color.FromHex("#E9E9E9");
            ThemeColors.StatusBarStyle = StatusBarStyle.DarkContent;
            ThemeColors.backgroundImg = null;
        }
        public static void DarkTheme()
        {
            ThemeColors.TextColor = Color.FromHex("#FFFFFF");
            ThemeColors.TransparentTextColor = Color.FromHex("#BAFFFFFF");
            ThemeColors.BorderColor = Color.FromHex("#BAFFFFFF");
            ThemeColors.BorderBackColor = Color.FromHex("#40000000");
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
