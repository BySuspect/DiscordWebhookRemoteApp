using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Webhook;
using DiscordWebhookRemoteApp.Helpers;
using DiscordWebhookRemoteApp.Pages.Popups;
using MarcTron.Plugin;
using MarcTron.Plugin.Extra;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.DateTimePopups;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        DiscordWebhook hook = new DiscordWebhook();

        bool webhookSelected = false,
            webhookProfileAvatar = false,
            webhookProfileName = false;

        string selectedUrl = "",
            webhookImageUrl = "",
            webhookName = "",
            embedFooterIconUrl = "",
            embedAuthorIconUrl = "";

        List<FileInfo> SelectedFiles = new List<FileInfo>();

        int selectedId = -1;

        public MainPage()
        {
            InitializeComponent();

            CrossMTAdmob.Current.TagForChildDirectedTreatment =
                MTTagForChildDirectedTreatment.TagForChildDirectedTreatmentUnspecified;
            CrossMTAdmob.Current.TagForUnderAgeOfConsent =
                MTTagForUnderAgeOfConsent.TagForUnderAgeOfConsentUnspecified;
            CrossMTAdmob.Current.MaxAdContentRating = MTMaxAdContentRating.MaxAdContentRatingG;

            BindingContext = this;
            var savedDate = Preferences.Get("SupportPopupDate", DateTime.MinValue);
            if (savedDate.Date != DateTime.Now.Date)
                popupInfoBack.IsVisible = true;
            BindableLayout.SetItemsSource(blSavedWebhooks, References.WebhookList);
            //DisplayAlert("Hello!", "This is the first version of my app. Please post your feedback in google play comments.", "Ok");
            Theme.ThemeChanged += (s, e) =>
            {
                Debug.WriteLine("theme changed: " + e.NewTheme);
            };
#if DEBUG
            //btnPreview.IsVisible = true;
            //string mytext = "";
            //Clipboard.SetTextAsync(mytext);
            //popupInfoBack.IsVisible = false;
#endif
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await References.CheckAppVersion();
        }

        #region Back Exit
        Timer timer = new Timer { Interval = 2000 };
        int _backButtonCounter = 0;

        void setupTimer()
        {
            if (!timer.Enabled)
            {
                timer.Elapsed += (s, e) =>
                {
                    _backButtonCounter = 0;
                    timer.Stop();
                };
                timer.Start();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (MessageSaveLoadCV.IsVisible)
            {
                MessageSaveLoadCV.IsVisible = false;
                return true;
            }
            if (_backButtonCounter >= 2)
                return false;
            if (_backButtonCounter == 0)
            {
                ToastController.ShowShortToast("Press 3 times to exit.");
                setupTimer();
            }
            _backButtonCounter++;
            return true;
        }
        #endregion

        //---------------------------------------------------------------------------------------------------

        #region Webhook Url
        private async void WebhookAdd_Tapped(object sender, EventArgs e)
        {
            Popup popup = new WebhookAddEditPopup();
            await App.Current.MainPage.Navigation.ShowPopupAsync(popup);
            BindableLayout.SetItemsSource(blSavedWebhooks, References.WebhookList);
        }

        private async void WebhookSelect_Tapped(object sender, EventArgs e)
        {
            Loodinglayout.IsVisible = true;
            var selected = References
                .WebhookList.Where(x => x.ID == Convert.ToInt32((sender as Frame).AutomationId))
                .FirstOrDefault();
            hook.Url = selected.url;
            selectedUrl = selected.url;
            selectedId = selected.ID;
            lblSelectedUrl.Text = selected.name;
            webhookSelected = true;

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(hook.Url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<JObject>(content);

                    var avatar = json["avatar"].ToString();
                    if (!string.IsNullOrEmpty(avatar))
                    {
                        webhookImageUrl =
                            $"https://cdn.discordapp.com/avatars/{json["id"]}/{avatar}.png";
                        imgWebhookImage.Source = webhookImageUrl;
                        webhookProfileAvatar = true;
                    }
                    else
                    {
                        webhookImageUrl = "dcdemoimage.png";
                        imgWebhookImage.Source = webhookImageUrl;
                        webhookProfileAvatar = false;
                    }

                    var name = json["name"].ToString();
                    if (!string.IsNullOrEmpty(name))
                    {
                        entryWebhookName.Text = name;
                        webhookName = name;
                        webhookProfileName = true;
                    }
                    else
                    {
                        entryWebhookName.Text = "";
                        webhookProfileName = false;
                    }
                }
            }
            catch { }

            Loodinglayout.IsVisible = false;
        }

        private async void webhookEditDelete_Clicked(object sender, EventArgs e)
        {
            if (webhookSelected)
            {
                Popup popup = new WebhookAddEditPopup(selectedId);
                var res = await App.Current.MainPage.Navigation.ShowPopupAsync(popup);
                Debug.WriteLine(res);
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
                if (res == "delete")
                {
                    selectedId = -1;
                    selectedUrl = "";
                    webhookSelected = false;
                    lblSelectedUrl.Text = "none";
                }
                else
                {
                    var selected = References
                        .WebhookList.Where(x => x.ID == selectedId)
                        .FirstOrDefault();
                    hook.Url = selected.url;
                    selectedUrl = selected.url;
                    selectedId = selected.ID;
                    lblSelectedUrl.Text = selected.name;
                    webhookSelected = true;
                }
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
            }
            BindableLayout.SetItemsSource(blSavedWebhooks, References.WebhookList);
        }
        #endregion

        #region Webhook Profile
        private async void webhookSaveLoad_Clicked(object sender, EventArgs e)
        {
            #region easterEgg
            if (entryWebhookName.Text == "{zenandshriokossecret}")
            {
                if (!Preferences.Get("{zenandshriokossecret}", false))
                {
                    Preferences.Set("{zenandshriokossecret}", true);
                    ChangeAppTheme.ForDenizTheme();
                }
                else
                {
                    Preferences.Set("{zenandshriokossecret}", false);
                    switch (AppInfo.RequestedTheme)
                    {
                        case AppTheme.Unspecified:
                            ChangeAppTheme.DarkTheme();
                            break;
                        case AppTheme.Light:
                            ChangeAppTheme.LightTheme();
                            break;
                        case AppTheme.Dark:
                            ChangeAppTheme.DarkTheme();
                            break;
                        default:
                            break;
                    }
                }
                App.Current.MainPage = new MainPage();
                return;
            }
            #endregion
            try
            {
                Popup popup = new WebhookProfileSaveEditPopup(webhookImageUrl, webhookName);
                var res = await App.Current.MainPage.Navigation.ShowPopupAsync(popup);

                if (((webhookProfileItems)res) != null)
                {
                    webhookImageUrl = ((webhookProfileItems)res).image;
                    imgWebhookImage.Source = webhookImageUrl;
                    webhookProfileAvatar = true;

                    webhookName = ((webhookProfileItems)res).name;
                    entryWebhookName.Text = webhookName;
                    webhookProfileName = true;
                }
            }
            catch { }
        }

        private async void WebhookImage_Tapped(object sender, EventArgs e)
        {
            try
            {
                string oldAvatarUrl = "";
                if (webhookProfileAvatar)
                    oldAvatarUrl = webhookImageUrl;
                string result = await DisplayPromptAsync(
                    "Webhook Image",
                    "Only \".jpg\", \".jpeg\", \".png\", \".gif\" Supported!",
                    accept: "Ok",
                    cancel: "Cancel",
                    placeholder: "Image url.",
                    initialValue: oldAvatarUrl
                );
                Debug.WriteLine(result);
                if (!string.IsNullOrEmpty(result.Trim()))
                {
                    string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    bool isImageUrl = imageExtensions.Any(result.Contains);

                    if (isImageUrl)
                    {
                        webhookImageUrl = result.Trim();
                        imgWebhookImage.Source = result.Trim();
                        webhookProfileAvatar = true;
                    }
                    else
                    {
                        _ = DisplayAlert("Error!", "This is not an image URL.", "Ok");
                    }
                }
                else
                {
                    webhookImageUrl = "";
                    imgWebhookImage.Source = "dcdemoimage.png";
                    webhookProfileAvatar = false;
                }
            }
            catch { }
        }

        private void entryWebhookName_Completed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(entryWebhookName.Text))
            {
                Debug.WriteLine("name empty");
                webhookName = "";
                webhookProfileName = false;
            }
            else
            {
                Debug.WriteLine(entryWebhookName.Text);
                webhookName = entryWebhookName.Text;
                webhookProfileName = true;
            }
        }

        private void entryWebhookName_Unfocused(object sender, FocusEventArgs e) =>
            entryWebhookName_Completed(null, null);
        #endregion

        #region Embed
        private async void embedBodyColorPicker_Tapped(object sender, EventArgs e)
        {
            var oldColor = embedBodyColorPicker.Color;
            Popup popup = new ColorPickerPopup(this, oldColor);
            var res = await App.Current.MainPage.Navigation.ShowPopupAsync(popup);
            var t = embedBodyColorPicker.Color.ToHex();
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
            if (res == null || res == "cancel")
            {
                embedBodyColorPicker.Color = oldColor;
            }
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
        }

        private async void EmbedAuthorImage_Tapped(object sender, EventArgs e)
        {
            try
            {
                string oldAvatarUrl = embedAuthorIconUrl;
                string result = await DisplayPromptAsync(
                    "Author Icon Image",
                    "Only \".jpg\", \".jpeg\", \".png\", \".gif\" Supported!",
                    accept: "Ok",
                    cancel: "Cancel",
                    placeholder: "Image url.",
                    initialValue: oldAvatarUrl
                );
                Debug.WriteLine(result);
                if (!string.IsNullOrEmpty(result.Trim()))
                {
                    bool isImageUrl = imageExtensions.Any(result.EndsWith);

                    if (isImageUrl)
                    {
                        embedAuthorIconUrl = result.Trim();
                        ImgEmbedAuthor.Source = result.Trim();
                    }
                    else
                    {
                        _ = DisplayAlert("Error!", "This is not an image URL.", "Ok");
                    }
                }
                else
                {
                    embedAuthorIconUrl = "";
                    imgWebhookImage.Source = "dcdemoimage.png";
                }
            }
            catch { }
        }

        private async void EmbedFooterAuthorImage_Tapped(object sender, EventArgs e)
        {
            try
            {
                string oldAvatarUrl = embedFooterIconUrl;
                string result = await DisplayPromptAsync(
                    "Author Icon Image",
                    "Only \".jpg\", \".jpeg\", \".png\", \".gif\" Supported!",
                    accept: "Ok",
                    cancel: "Cancel",
                    placeholder: "Image url.",
                    initialValue: oldAvatarUrl
                );
                Debug.WriteLine(result);
                if (!string.IsNullOrEmpty(result.Trim()))
                {
                    bool isImageUrl = imageExtensions.Any(result.EndsWith);

                    if (isImageUrl)
                    {
                        embedFooterIconUrl = result.Trim();
                        ImgEmbedFooterIconUrl.Source = result.Trim();
                    }
                    else
                    {
                        _ = DisplayAlert("Error!", "This is not an image URL.", "Ok");
                    }
                }
                else
                {
                    embedFooterIconUrl = "";
                    ImgEmbedFooterIconUrl.Source = "dcdemoimage.png";
                }
            }
            catch { }
        }

        private async void EmbedFooterSelectTimebtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                DateTime? selectedFooterDate = await DateTimePopups.PickDateAsync(DateTime.Now);
                if (selectedFooterDate != null)
                {
                    TimeSpan? selectedFooterTime = await DateTimePopups.PickTimeAsync(
                        DateTime.Now.TimeOfDay
                    );
                    if (selectedFooterTime != null)
                    {
                        var selectedFooterDateTime = new DateTime(
                            selectedFooterDate.Value.Year,
                            selectedFooterDate.Value.Month,
                            selectedFooterDate.Value.Day,
                            selectedFooterTime.Value.Hours,
                            selectedFooterTime.Value.Minutes,
                            0
                        );
                        EmbedFooterTimestampSelectedLbl.SavedDateTimeData = selectedFooterDateTime;
                        ToastController.ShowLongToast(
                            $"{selectedFooterDate.Value.ToLongDateString()} selected"
                        );
                    }
                    else
                        throw null;
                }
                else
                    throw null;
            }
            catch
            {
                ToastController.ShowShortToast("Footer timestamp select error!");
            }
        }

        private void cbFooterSendInstantTime_IsCheckedChanged(object sender, bool e)
        {
            EmbedFooterSelectTimebtn.IsEnabled = !e;
            if (e)
                EmbedFooterTimestampSelectedLbl.SavedDateTimeData = null;
        }

        private void EmbedFooterTimestampClearbtn_Clicked(object sender, EventArgs e)
        {
            EmbedFooterTimestampSelectedLbl.SavedDateTimeData = null;
            cbFooterSendInstantTime.IsChecked = false;
        }
        #endregion

        #region Send File
        private async void btnFileSelect_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickMultipleAsync(
                    new PickOptions { PickerTitle = "Select Max 10 Images", }
                );

                if (result.Count() > 0)
                {
                    double filesizeCounter = 0;
                    if (result.Count() > 10)
                    {
                        ToastController.ShowShortToast("Please select max 10 files!");
                        return;
                    }
                    else
                    {
                        lblSelectedFile.Text = "";
                        SelectedFiles = new List<FileInfo>();
                        foreach (var _file in result)
                        {
                            var fileinfo = new FileInfo(_file.FullPath);
                            filesizeCounter += fileinfo.Length;
                            if (fileinfo.Length <= 18874400)
                                SelectedFiles.Add(new FileInfo(_file.FullPath));
                            else
                            {
                                _ = DisplayAlert("Error", "File can only be 25mb maximum", "Ok");
                                return;
                            }
                        }
                        if (filesizeCounter > 26214400)
                        {
                            ToastController.ShowLongToast("Please select total max 25mb files!");
                            lblSelectedFile.Text = "";
                            SelectedFiles = new List<FileInfo>();
                            return;
                        }
                        else if (filesizeCounter > 16000000)
                            _ = DisplayAlert(
                                "Warning!",
                                "Discord may not accept file sizes over 16mb!",
                                "ok"
                            );
                        lblSelectedFile.Text =
                            (filesizeCounter / 1024 / 1024).ToString("##")
                            + "mb\n"
                            + string.Join(", ", SelectedFiles.ConvertAll(x => x.Name.ToString()));
                    }
                }
                else
                {
                    SelectedFiles = new List<FileInfo>();
                    lblSelectedFile.Text = "";
                }
            }
            catch
            {
                // The user canceled or something went wrong
            }
        }

        private void btnFileClear_Clicked(object sender, EventArgs e)
        {
            lblSelectedFile.Text = "";
            SelectedFiles = new List<FileInfo>();
        }
        #endregion

        #region ADSControls
        //async Task adsCheck()
        //{
        //    try
        //    {
        //        if (fRes != null)
        //        {
        //            if (fRes.Settings.TestingMode.IsOnTest && References.Version == fRes.Settings.TestingMode.Version)
        //            {
        //                adsTop.IsVisible = false;
        //                adsBottom.IsVisible = false;
        //            }
        //            else
        //            {
        //                //adsTop.IsVisible = adsTopLoaded;
        //                adsBottom.IsVisible = adsBottomLoaded;
        //            }
        //        }
        //        else
        //        {
        //            fRes = await Database.FirebaseDatabase.GetData();
        //            await adsCheck();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _ = DisplayAlert("Ads Error!", ex.Message, "Ok");
        //    }
        //}
        #endregion

        //---------------------------------------------------------------------------------------------------

        private async void clearContent_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert(
                "Warning!",
                "Are you sure about to clear content?",
                "Yes",
                "No"
            );
            Debug.WriteLine("Answer: " + answer);
            if (answer)
            {
                App.Current.MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = ThemeColors.StatusBarColor,
                    BarTextColor = ThemeColors.TextColor,
                };
                //expEmbedAuthor.IsVisible = false;
                //expEmbedBody.IsVisible = false;
                //expEmbedContent.IsVisible = false;
                //expEmbedFields.IsVisible = false;
                //expEmbedFooter.IsVisible = false;
                //expEmbedImages.IsVisible = false;
                //expFileSend.IsVisible = false;
                //expMessageContent.IsVisible = false;
                //entryContentMessage.Text = "";
                //filepath = "";
                //lblSelectedFile.Text = "none";
                //entryWebhookName.Text = "";
                //imgWebhookImage.Source = "dcdemoimage.png";
            }
        }

        private async void ExportWebhooks_Clicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(
                ConvertStringToBase64(JsonConvert.SerializeObject(References.WebhookList))
            );

            ToastController.ShowShortToast("Webhooks copied to clipboard.");
        }

        public static string ConvertStringToBase64(string originalString)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(originalString);
            string base64String = Convert.ToBase64String(bytes);
            return base64String;
        }

        private void Preview_Clicked(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3881259676793306/5327145658");

            //DiscordMessage message = new DiscordMessage();

            //if (webhookProfileName)
            //    message.Username = webhookName;

            //if (webhookProfileAvatar)
            //    message.AvatarUrl = webhookImageUrl;

            //if (expMessageContent.IsExpanded)
            //{
            //    if (!string.IsNullOrEmpty(entryContentMessage.Text))
            //    {
            //        message.Content = entryContentMessage.Text;
            //    }
            //}

            //if (expEmbedContent.IsExpanded)
            //{
            //    message.Embeds = new List<DiscordEmbed>();
            //    var embed = embedBuilder();
            //    //if (embed.Description != "{Embed31Build31Hataya31Dustu31}")
            //    if (embed.Description != "{Empty31Embed31Builded31}")
            //        message.Embeds.Add(embed);
            //}

            //if (expFileSend.IsExpanded && SelectedFiles.Count() > 0)
            //{

            //}
            //var messagejson = JsonConvert.SerializeObject(message);
        }

        private void btnSaveLoadMessage_Clicked(object sender, EventArgs e)
        {
            try
            {
                var message = GetMessage();
                var jsonMessage = JsonConvert.SerializeObject(message.Result, Formatting.Indented);

                //await Navigation.PushAsync(new TextEditorPage(jsonMessage), false);
                MessageSaveLoadCV.IsVisible = true;
            }
            catch { }
            Loodinglayout.IsVisible = false;
        }

        private void MessageSaveLoadViewClose(object sender, EventArgs e)
        {
            MessageSaveLoadCV.IsVisible = false;
        }

        private async void sendContent_Clicked(object sender, EventArgs e)
        {
            if (webhookSelected)
            {
                try
                {
                    var message = await GetMessage();
                    if (expFileSend.IsExpanded && SelectedFiles.Count() > 0)
                        await hook.Send(message, SelectedFiles.ToArray());
                    else
                    {
                        await hook.Send(message);
                        var json = JsonConvert.SerializeObject(message);
                    }

                    ToastController.ShowShortToast("Submitted Successfully.");
                    if (new Random().Next(1, 100) % 10 == 0)
                        popupInfoBack.IsVisible = true;
                }
                catch (Exception ex)
                {
#if DEBUG
                    _ = DisplayAlert("Send Error!!", $"Message:\n{ex.Message}", "Ok");
#endif
                    Debug.WriteLine("Send Error!!");
                    ToastController.ShowShortToast("Send Error!!");
                }
                Loodinglayout.IsVisible = false;
            }
            else
                //_ = DisplayAlert("Warning!", "First select Webhook!", "Ok");
                ToastController.ShowShortToast("First select Webhook!");
        }

        private async Task<DiscordMessage> GetMessage()
        {
            Loodinglayout.IsVisible = true;
            DiscordMessage message = new DiscordMessage();

            if (webhookProfileName)
                message.Username = webhookName;

            if (webhookProfileAvatar)
                message.AvatarUrl = webhookImageUrl;

            if (expMessageContent.IsExpanded)
            {
                if (!string.IsNullOrEmpty(entryContentMessage.Text))
                {
                    message.Content = entryContentMessage.Text;
                }
            }

            if (expEmbedContent.IsExpanded)
            {
                message.Embeds = new List<DiscordEmbed>();
                var embed = embedBuilder();
                //if (embed.Description != "{Embed31Build31Hataya31Dustu31}")
                if (embed.Description != "{Empty31Embed31Builded31}")
                    message.Embeds.Add(embed);
            }

            return message;
        }

        #region testArea
        private void Button_Clicked(object sender, EventArgs e)
        {
            //DiscordMessage message = new DiscordMessage();
            //message.Content = "Example message, ping @everyone, <@272665050672660501> <1072939523148882000>";
            ////message.TTS = true; //read message to everyone on the channel
            //message.Username = "Webhook";
            //message.AvatarUrl = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif";

            ////embeds
            //DiscordEmbed embed = new DiscordEmbed();
            //embed.Title = "Embed title";
            //embed.Description = "Embed description";
            ////embed.Url = "Embed Url";
            //embed.Timestamp = DateTime.Now;
            //embed.Color = Color.Red; //alpha will be ignored, you can use any RGB color
            //embed.Footer = new EmbedFooter() { Text = "Footer Text", IconUrl = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif" };
            //embed.Image = new EmbedMedia() { Url = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif", Width = 150, Height = 150 }; //valid for thumb and video
            //embed.Thumbnail = new EmbedMedia() { Url = "https://cdn.discordapp.com/avatars/272665050672660501/a_25129c94f0e743d18fd9dc276ff05606.gif" };                                                                                                                                    //embed.Provider = new EmbedProvider() { Name = "Provider Name", Url = "Provider Url" };
            //embed.Author = new EmbedAuthor() { Name = "Author Name", Url = "https://google.com/", IconUrl = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif" };

            ////fields
            //embed.Fields = new List<EmbedField>();
            //embed.Fields.Add(new EmbedField() { Name = "Field Name", Value = "Field Value", InLine = true });
            //embed.Fields.Add(new EmbedField() { Name = "Field Name 2", Value = "Field Value 2", InLine = true });
            ////embed2s
            //DiscordEmbed embed2 = new DiscordEmbed();
            //embed2.Title = "Embed title";
            //embed2.Description = "Embed description";
            ////embed2.Url = "Embed Url";
            //embed2.Timestamp = DateTime.Now;
            //embed2.Color = Color.Red; //alpha will be ignored, you can use any RGB color
            //embed2.Footer = new EmbedFooter() { Text = "Footer Text", IconUrl = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif" };
            //embed2.Image = new EmbedMedia() { Url = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif", Width = 150, Height = 150 }; //valid for thumb and video
            //                                                                                                                                                                     //embed2.Provider = new EmbedProvider() { Name = "Provider Name", Url = "Provider Url" };
            //embed2.Author = new EmbedAuthor() { Name = "Author Name", Url = "https://google.com/", IconUrl = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif" };

            ////set embed
            //message.Embeds = new List<DiscordEmbed>();
            //message.Embeds.Add(embed);
            //message.Embeds.Add(embed2);

            ////message
            //_ = hook.Send(message);

            ////file
            ////hook.Send(message, new FileInfo("C:/File/Path.file"));
        }

        private void Button_Clicked2(object sender, EventArgs e)
        {
            //try
            //{
            //    DiscordMessage message = new DiscordMessage();

            //    var result = await FilePicker.PickAsync();
            //    if (result != null)
            //    {
            //        var Text = $"File Name: {result.FileName}";
            //        if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
            //            result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
            //        {
            //            var stream = await result.OpenReadAsync();
            //            var Image = ImageSource.FromStream(() => stream);

            //            //testImg.Source = Image;
            //        }
            //        var filest = new FileInfo(result.FullPath);
            //        message.Content = filest.Length.ToString();
            //        //discord max file size
            //        if (filest.Length < 8388235)
            //        {
            //            //file
            //            filepath = result.FullPath;
            //        }
            //        else _ = DisplayAlert("Error", "File can only be 8mb maximum", "Ok");
            //    }

            //}
            //catch
            //{
            //    // The user canceled or something went wrong
            //}
        }

        private void Button_Clicked_1(object sender, EventArgs e) { }
        #endregion


        private DiscordEmbed embedBuilder()
        {
            /*
             *görüntü olarak video eklenicek
             *tüm resim urllerini kontrol edip hatalı olanları
                   -bir string veride ekleyip display alertte
                   -göstericek ve hatalı resimler olmadan gönderip
                   -gönderilemeyeceği sorulacak.
             *
             */
            try
            {
                //embeds
                DiscordEmbed embed = new DiscordEmbed();
                bool IsEmpty = true;

                if (expEmbedAuthor.IsExpanded)
                {
                    var author = new EmbedAuthor();

                    //Embed Author Title
                    if (!string.IsNullOrEmpty(EntryEmbedAuthorName.Text))
                        author.Name = EntryEmbedAuthorName.Text;

                    //Embed Author Url
                    if (IsULCCheck(EntryEmbedAuthorUrl.Text))
                        author.Url = EntryEmbedAuthorUrl.Text;

                    //Embed Author Icon Url
                    if (ImgEmbedAuthor.Source.ToString() != "File: dcdemoimage.png")
                        author.IconUrl = embedAuthorIconUrl;

                    //Embed author empty check
                    if (author.Name != null || author.Url != null || author.IconUrl != null)
                        IsEmpty = false;
                    embed.Author = author;
                }

                if (expEmbedBody.IsExpanded)
                {
                    //Body Title
                    if (!string.IsNullOrEmpty(EntryBodyTitle.Text))
                        embed.Title = EntryBodyTitle.Text;

                    //Body Description
                    if (!string.IsNullOrEmpty(entryBodyContent.Text))
                        embed.Description = entryBodyContent.Text;

                    //Body Url
                    if (IsULCCheck(entryBodyUrl.Text))
                        embed.Url = entryBodyUrl.Text;

                    //Embed body empty check
                    embed.Color = embedBodyColorPicker.Color;
                    if (embed.Title != null || embed.Description != null || embed.Url != null)
                        IsEmpty = false;
                }

                if (expEmbedFields.IsExpanded)
                {
                    var fields = new List<EmbedField>();

                    if (expEmbedField1.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField1.IsChecked,
                                Name = EntryFieldName1.Text,
                                Value = entryFieldValue1.Text,
                            }
                        );
                    }

                    if (expEmbedField2.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField2.IsChecked,
                                Name = EntryFieldName2.Text,
                                Value = entryFieldValue2.Text,
                            }
                        );
                    }

                    if (expEmbedField3.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField3.IsChecked,
                                Name = EntryFieldName3.Text,
                                Value = entryFieldValue3.Text,
                            }
                        );
                    }

                    if (expEmbedField4.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField4.IsChecked,
                                Name = EntryFieldName4.Text,
                                Value = entryFieldValue4.Text,
                            }
                        );
                    }

                    if (expEmbedField5.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField5.IsChecked,
                                Name = EntryFieldName5.Text,
                                Value = entryFieldValue5.Text,
                            }
                        );
                    }

                    if (expEmbedField6.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField6.IsChecked,
                                Name = EntryFieldName6.Text,
                                Value = entryFieldValue6.Text,
                            }
                        );
                    }

                    if (expEmbedField7.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField7.IsChecked,
                                Name = EntryFieldName7.Text,
                                Value = entryFieldValue7.Text,
                            }
                        );
                    }

                    if (expEmbedField8.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField8.IsChecked,
                                Name = EntryFieldName8.Text,
                                Value = entryFieldValue8.Text,
                            }
                        );
                    }

                    if (expEmbedField9.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField9.IsChecked,
                                Name = EntryFieldName9.Text,
                                Value = entryFieldValue9.Text,
                            }
                        );
                    }

                    if (expEmbedField10.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField10.IsChecked,
                                Name = EntryFieldName10.Text,
                                Value = entryFieldValue10.Text,
                            }
                        );
                    }

                    if (expEmbedField11.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField11.IsChecked,
                                Name = EntryFieldName11.Text,
                                Value = entryFieldValue11.Text,
                            }
                        );
                    }

                    if (expEmbedField12.IsExpanded)
                    {
                        fields.Add(
                            new EmbedField
                            {
                                InLine = cboxInlineField12.IsChecked,
                                Name = EntryFieldName12.Text,
                                Value = entryFieldValue12.Text,
                            }
                        );
                    }

                    //Embed fields empty check
                    if (fields.Count != 0)
                        foreach (var item in fields)
                        {
                            if (
                                !string.IsNullOrEmpty(item.Name)
                                && !string.IsNullOrEmpty(item.Value)
                            )
                            {
                                IsEmpty = false;
                                break;
                            }
                        }

                    embed.Fields = fields;
                }

                if (expEmbedImages.IsExpanded)
                {
                    //Image
                    if (
                        !string.IsNullOrEmpty(EntryEmbedImagesImageUrl.Text)
                        && IsULCCheck(EntryEmbedImagesImageUrl.Text)
                    )
                        embed.Image = new EmbedMedia { Url = EntryEmbedImagesImageUrl.Text };

                    //Thumbnail
                    if (
                        !string.IsNullOrEmpty(EntryEmbedImagesThumbnailUrl.Text)
                        && IsULCCheck(EntryEmbedImagesThumbnailUrl.Text)
                    )
                        embed.Thumbnail = new EmbedMedia
                        {
                            Url = EntryEmbedImagesThumbnailUrl.Text
                        };

                    //Embed image empty check
                    if (embed.Image != null || embed.Thumbnail != null)
                        IsEmpty = false;
                }

                if (expEmbedFooter.IsExpanded)
                {
                    var footer = new EmbedFooter();

                    //Embed Footer Url
                    if (IsULCCheck(embedFooterIconUrl))
                        footer.IconUrl = embedFooterIconUrl;

                    //Embed Footer Text
                    if (!string.IsNullOrEmpty(EntryFooterText.Text))
                        footer.Text = EntryFooterText.Text;

                    //Embed footer timestamp
                    if (cbFooterSendInstantTime.IsChecked)
                        embed.Timestamp = DateTime.Now;
                    else if (EmbedFooterTimestampSelectedLbl.SavedDateTimeData != null)
                        embed.Timestamp = EmbedFooterTimestampSelectedLbl.SavedDateTimeData.Value;

                    //Embed footer empty check
                    if (footer.Text != null || footer.IconUrl != null)
                        IsEmpty = false;

                    embed.Footer = footer;
                }

                if (IsEmpty)
                    return new DiscordEmbed { Description = "{Empty31Embed31Builded31}" };
                else
                    return embed;
            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Embed Error!", $"Message: {ex.Message}", "Ok");
                return new DiscordEmbed { Description = "{Embed31Build31Hataya31Dustu31}" };
            }
        }

        private bool IsULCCheck(string url)
        {
            Uri uriResult;
            bool isUrl =
                Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (
                    uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps
                );
            return isUrl;
        }

        private void btnSupportCancel_Clicked(object sender, EventArgs e)
        {
            popupInfoBack.IsVisible = false;
            Preferences.Set("SupportPopupDate", DateTime.Now);
        }

        private async void btnSupport_Clicked(object sender, EventArgs e)
        {
            popupInfoBack.IsVisible = false;
            Preferences.Set("SupportPopupDate", DateTime.Now);
            try
            {
                await Browser.OpenAsync(
                    new Uri("https://bit.ly/3pR7H0W"),
                    BrowserLaunchMode.External
                );
            }
            catch
            {
                Debug.WriteLine("Support Browser Error");
            }
        }

        private void DiscordButton_Clicked(object sender, EventArgs e)
        {
            References.discordClicked();
        }

        private async void FeedbackButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Popup popup = new FeedbackPopupPage();
                var res = await App.Current.MainPage.Navigation.ShowPopupAsync(popup);
                if (res.ToString() == "counterror")
                {
                    await DisplayAlert("Warning!", "You reached daily feedback limit.", "Ok");
                }
                else if (res.ToString() == "catcherror")
                {
                    await DisplayAlert("Error!", "Something went wrong try again later.", "Ok");
                }
            }
            catch { }
        }
    }
}
