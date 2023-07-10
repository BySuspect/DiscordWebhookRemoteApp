using Discord;
using Discord.Webhook;
using DiscordWebhookRemoteApp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        string uid = "none",
            imagesText = "";

        List<FileInfo> FeedbackImages = new List<FileInfo>();
        public FeedbackPopupPage()
        {
            InitializeComponent();
#if DEBUG
            Preferences.Set("user_feedback_count", 1);
#endif
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
                sendIndcator.IsVisible = true;
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

                    DiscordMessage dcmessage = new DiscordMessage();

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
                    DiscordWebhook hook = new DiscordWebhook()
                    {
                        Url = "https://discord.com/api/webhooks/1125737504889372673/z6YHkSv3OU1O0_yQJ-Ul_0wZrWmfPO4THuFPGB4nvsfYThYJLobm8X9xSv4f8tOr9RxV",
                    };
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
                    dcmessage.Content = "https://awgstudiosapps-default-rtdb.europe-west1.firebasedatabase.app/WebhookRemoteFeedback/" + uid;
                    ToastController.ShowShortToast("Sending...");
                    var res = await httpClient.PostAsync("https://awgstudiosapps-default-rtdb.europe-west1.firebasedatabase.app/WebhookRemoteFeedback/" + uid + "/.json?auth=VjX98JeBVbUmZviKQOrW7yv8NPT69VRzG3m7sXNl", content);
                    if (FeedbackImages.Count > 0)
                        await hook.Send(dcmessage, FeedbackImages.ToArray());
                    if (res.IsSuccessStatusCode)
                    {
                        counter++;
                        FeedbackImages = new List<FileInfo>();
                        edtContent.Text = string.Empty;
                        Preferences.Set("user_feedback_count", counter);

                        ToastController.ShowShortToast("Feedback Sent.");
                    }
                    else
                        ToastController.ShowShortToast("Something went wrong!");
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
            btnSend.IsEnabled = true;
            sendIndcator.IsVisible = false;
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
        private async void btnSelectFiles_Clicked(object sender, EventArgs e)
        {
            FeedbackImages = new List<FileInfo>();
            double filesizeCounter = 0;
            var selectedFiles = await FilePicker.PickMultipleAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select Max 10 Images",
            });

            if (selectedFiles != null)
            {
                if (selectedFiles.Count() > 10)
                {
                    ToastController.ShowLongToast("Please select max 10 files!");
                    return;
                }
                foreach (var _file in selectedFiles)
                {
                    var fileinfo = new FileInfo(_file.FullPath);
                    filesizeCounter += fileinfo.Length;
                    if (fileinfo.Length <= 13000000)
                        FeedbackImages.Add(new FileInfo(_file.FullPath));
                    else
                    {
                        ToastController.ShowShortToast("File can only be 12mb maximum!");
                        return;
                    }
                }
                if (filesizeCounter > 13000000)
                {
                    ToastController.ShowLongToast("Please select total max 12mb files!");
                    FeedbackImages = new List<FileInfo>();
                    if (edtContent.Text != null)
                        edtContent.Text = edtContent.Text.Replace(imagesText, "");
                    return;
                }
            }
            else
                FeedbackImages = new List<FileInfo>();
            if (selectedFiles.Count() > 0)
            {
                try
                {
                    if (edtContent.Text != null && edtContent.Text.Contains(imagesText) && imagesText != "")
                        edtContent.Text = edtContent.Text.Replace(imagesText, "");
                    else if (edtContent.Text == null)
                        edtContent.Text = string.Empty;
                    if (FeedbackImages.Count() > 1)
                        imagesText = "{Do not change this!!!! Images: " + (filesizeCounter / 1024 / 1024).ToString("0.00") + "mb :" + string.Join(", ", FeedbackImages.ConvertAll(x => x.Name.ToString())) + "}";
                    else
                        imagesText = "{Do not change this!!!! Images: " + (filesizeCounter / 1024 / 1024).ToString("0.00") + "mb :" + FeedbackImages[0].Name + "}";
                    edtContent.Text = imagesText + edtContent.Text.ToString();
                }
                catch
                {

                }
            }
        }
        private void btnSend_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edtContent.Text))
            {
                ToastController.ShowShortToast("You cannot send blank messages!");
                return;
            }
            if (string.IsNullOrEmpty(edtContent.Text.Trim()))
            {
                ToastController.ShowShortToast("You cannot send blank messages!");
                return;
            }
            send(edtContent.Text.Trim());
        }
        private void btnDismiss_Clicked(object sender, EventArgs e)
        {
            this.Dismiss("exit");
        }
    }
}