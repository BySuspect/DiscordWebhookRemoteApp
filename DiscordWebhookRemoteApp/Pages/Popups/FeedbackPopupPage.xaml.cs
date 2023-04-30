using DiscordWebhookRemoteApp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPopupPage : Popup
    {
        int counter = 1;
        string uid = "none";
        public FeedbackPopupPage()
        {
            InitializeComponent();
            if (Preferences.Get("user_feedback_date", DateTime.Now.DayOfYear) == DateTime.Now.DayOfYear)
            {
                counter = Preferences.Get("user_feedback_count", 1);
            }
            else
            {
                Preferences.Set("user_feedback_date", DateTime.Now.DayOfYear);
                Preferences.Set("user_feedback_count", 1);
                counter = 1;
            }
        }
        async void send(string messagecontent)
        {
            try
            {
                btnSend.IsEnabled = false; edtContent.Unfocus();
                if (counter <= 5)
                {
                    Preferences.Set("user_feedback_date", DateTime.Now.DayOfYear);
                    if (Preferences.Get("user_feedback_ID", "none") == "none")
                    {
                        uid = Guid.NewGuid().ToString();
                        Preferences.Set("user_feedback_ID", uid);
                    }
                    else
                    {
                        uid = Preferences.Get("user_feedback_ID", "none");
                    }

                    // Device Model (SMG-950U, iPhone11,6)
                    string device = DeviceInfo.Model;

                    // Manufacturer (Samsung)
                    string manufacturer = DeviceInfo.Manufacturer;

                    // Device Name (Motz's iPhone)
                    string deviceName = DeviceInfo.Name;

                    // Operating System Version Number (7.0)
                    string version = DeviceInfo.VersionString;

                    // Platform (Android)
                    string platform = DeviceInfo.Platform.ToString();

                    // Idiomatic Device Type (Tablet)
                    string deviceType = DeviceInfo.Idiom.ToString();

                    var httpClient = new HttpClient();
                    string requestBody = JsonConvert.SerializeObject(new RootFeedBack
                    {
                        Content = messagecontent,
                        Device = device,
                        Manufacturer = manufacturer,
                        DeviceName = deviceName,
                        Version = version,
                        DeviceType = deviceType,
                        Platform = platform,
                        Timestramp = getTimestamp(),
                    });
                    HttpContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    if (uid == "none")
                    {
                        uid = Guid.NewGuid().ToString();
                        Preferences.Set("user_feedback_ID", uid);
                    }
                    var res = await httpClient.PostAsync("https://awgstudiosapps-default-rtdb.europe-west1.firebasedatabase.app/WebhookRemoteFeedback/" + uid + "/.json?auth=VjX98JeBVbUmZviKQOrW7yv8NPT69VRzG3m7sXNl", content);
                    if (res.IsSuccessStatusCode) counter++;
                    Preferences.Set("user_feedback_count", counter);
                }
                else
                {
                    Dismiss("counterror");
                }
            }
            catch
            {
                Dismiss("catcherror");
            }
            edtContent.Text = string.Empty;
            btnSend.IsEnabled = true;
        }
        public static long getTimestamp()
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan = DateTime.UtcNow.ToUniversalTime() - unixEpoch;
            return (long)timeSpan.TotalSeconds;
        }

        class RootFeedBack
        {
            public string Content { get; set; }
            public string Device { get; set; }
            public string Manufacturer { get; set; }
            public string DeviceName { get; set; }
            public string Version { get; set; }
            public string Platform { get; set; }
            public string DeviceType { get; set; }
            public long Timestramp { get; set; }
            public string Ip { get; set; }
        }

        private void btnSend_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edtContent.Text)) return;
            if (string.IsNullOrEmpty(edtContent.Text.Trim())) return;
            send(edtContent.Text.Trim());
        }
        private void btnDismiss_Clicked(object sender, EventArgs e)
        {
            this.Dismiss("exit");
        }
    }
}