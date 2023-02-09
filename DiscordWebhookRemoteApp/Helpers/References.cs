using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DiscordWebhookRemoteApp.Helpers
{
    public static class References
    {
        static List<webhookItems> _webhookList;
        public static List<webhookItems> WebhookList
        {
            get
            {
                _webhookList = JsonConvert.DeserializeObject<List<webhookItems>>(Preferences.Get("savedwebhookItems", "[]"));
                _webhookList.OrderBy(x => x.ID).ToList();
                return _webhookList;
            }
            set
            {
                _webhookList = value;
                string json = JsonConvert.SerializeObject(value);
                Preferences.Set("savedwebhookItems", json);
            }
        }
    }
    public class webhookItems
    {
        public int ID { get; set; }
        public string url { get; set; }
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
            ThemeColors.BackColor = Color.White;
            ThemeColors.StatusBarColor = Color.White;
            ThemeColors.StatusBarStyle = StatusBarStyle.DarkContent;
        }
        public static void DarkTheme()
        {
            ThemeColors.TextColor = Color.White;
            ThemeColors.TransparentTextColor = Color.FromHex("#BAFFFFFF");
            ThemeColors.BorderColor = Color.White;
            ThemeColors.BorderBackColor = Color.Transparent;
            ThemeColors.BackColor = Color.Black;
            ThemeColors.StatusBarColor = Color.Black;
            ThemeColors.StatusBarStyle = StatusBarStyle.LightContent;
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
    }
}
