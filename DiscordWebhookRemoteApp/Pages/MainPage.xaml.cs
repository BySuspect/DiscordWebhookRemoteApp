using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Discord;
using Discord.Webhook;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using Xamarin.Forms.PlatformConfiguration;
using static System.Net.Mime.MediaTypeNames;
using static Xamarin.Essentials.AppleSignInAuthenticator;
using Xamarin.Essentials;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using DiscordWebhookRemoteApp.Helpers;
using System.Diagnostics;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using DiscordWebhookRemoteApp.Pages.Popups;

namespace DiscordWebhookRemoteApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        List<webhookItems> webhookList = new List<webhookItems>();

        DiscordWebhook hook = new DiscordWebhook();

        bool
            webhookSelected = false,
            webhookProfileAvatar = false,
            webhookProfileName = false;

        string selectedUrl = "",
            webhookImageUrl = "",
            webhookName = "",
            filepath = "",
            embedAuthorIconUrl = "";

        int selectedId = -1;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            BindableLayout.SetItemsSource(blSavedWebhooks, References.WebhookList);
            //DisplayAlert("Hello!", "This is the first version of my app. Please post your feedback in google play comments.", "Ok");
            Theme.ThemeChanged += (s, e) =>
            {
                Debug.WriteLine("theme changed: " + e.NewTheme);
            };
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var fRes = await Database.FirebaseDatabase.GetData();
            if (fRes != null)
            {
                if (fRes.Settings.TestingMode.IsOnTest && References.Version == fRes.Settings.TestingMode.Version)
                {
                    adsBottom.IsVisible = false;
                }
            }

        }
        private async void sendContent_Clicked(object sender, EventArgs e)
        {
            if (webhookSelected)
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
                    message.Embeds.Add(await embedBuilder());
                }

                try
                {
                    if (expFileSend.IsExpanded && !string.IsNullOrEmpty(filepath))
                        await hook.Send(message, new FileInfo(filepath));
                    else
                    {
                        await hook.Send(message);
                    }
                }
                catch (Exception ex)
                {
                    _ = DisplayAlert("Send Error!!", $"Message:\n{ex.Message}", "Ok");
                    Debug.WriteLine("Send Error!!");
                }
                await Task.Delay(600);
                Loodinglayout.IsVisible = false;
            }
            else
                _ = DisplayAlert("Warning!", "First select Webhook!", "Ok");
        }
        private async void clearContent_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Warning!", "Are you sure about to clear content?", "Yes", "No");
            Debug.WriteLine("Answer: " + answer);
            if (answer)
            {
                expEmbedAuthor.IsVisible = false;
                expEmbedBody.IsVisible = false;
                expEmbedContent.IsVisible = false;
                expEmbedFields.IsVisible = false;
                expEmbedFooter.IsVisible = false;
                expEmbedImages.IsVisible = false;
                expFileSend.IsVisible = false;
                expMessageContent.IsVisible = false;
                entryContentMessage.Text = "";
                filepath = "";
                lblSelectedFile.Text = "none";
                entryWebhookName.Text = "";
                imgWebhookImage.Source = "demoimage.png";
            }
        }
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
                string result = await DisplayPromptAsync("Author Icon Image", "Only \".jpg\", \".jpeg\", \".png\", \".gif\" Supported!", accept: "Ok", cancel: "Cancel", placeholder: "Image url.", initialValue: oldAvatarUrl);
                Debug.WriteLine(result);
                if (!string.IsNullOrEmpty(result.Trim()))
                {
                    string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

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
                    imgWebhookImage.Source = "demoimage.png";
                }
            }
            catch { }
        }

        #region testArea
        private void Button_Clicked(object sender, EventArgs e)
        {
            DiscordMessage message = new DiscordMessage();
            message.Content = "Example message, ping @everyone, <@272665050672660501> <1072939523148882000>";
            //message.TTS = true; //read message to everyone on the channel
            message.Username = "Webhook";
            message.AvatarUrl = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif";

            //embeds
            DiscordEmbed embed = new DiscordEmbed();
            embed.Title = "Embed title";
            embed.Description = "Embed description";
            //embed.Url = "Embed Url";
            embed.Timestamp = DateTime.Now;
            embed.Color = Color.Red; //alpha will be ignored, you can use any RGB color
            embed.Footer = new EmbedFooter() { Text = "Footer Text", IconUrl = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif" };
            embed.Image = new EmbedMedia() { Url = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif", Width = 150, Height = 150 }; //valid for thumb and video
            embed.Thumbnail = new EmbedMedia() { Url = "https://cdn.discordapp.com/avatars/272665050672660501/a_25129c94f0e743d18fd9dc276ff05606.gif" };                                                                                                                                    //embed.Provider = new EmbedProvider() { Name = "Provider Name", Url = "Provider Url" };
            embed.Author = new EmbedAuthor() { Name = "Author Name", Url = "https://google.com/", IconUrl = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif" };

            //fields
            embed.Fields = new List<EmbedField>();
            embed.Fields.Add(new EmbedField() { Name = "Field Name", Value = "Field Value", InLine = true });
            embed.Fields.Add(new EmbedField() { Name = "Field Name 2", Value = "Field Value 2", InLine = true });
            //embed2s
            DiscordEmbed embed2 = new DiscordEmbed();
            embed2.Title = "Embed title";
            embed2.Description = "Embed description";
            //embed2.Url = "Embed Url";
            embed2.Timestamp = DateTime.Now;
            embed2.Color = Color.Red; //alpha will be ignored, you can use any RGB color
            embed2.Footer = new EmbedFooter() { Text = "Footer Text", IconUrl = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif" };
            embed2.Image = new EmbedMedia() { Url = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif", Width = 150, Height = 150 }; //valid for thumb and video
                                                                                                                                                                                 //embed2.Provider = new EmbedProvider() { Name = "Provider Name", Url = "Provider Url" };
            embed2.Author = new EmbedAuthor() { Name = "Author Name", Url = "https://google.com/", IconUrl = "https://cdn.discordapp.com/avatars/901826325940154388/a_ccb07997406647294aa254aaabaa36fc.gif" };

            //set embed
            message.Embeds = new List<DiscordEmbed>();
            message.Embeds.Add(embed);
            message.Embeds.Add(embed2);

            //message
            _ = hook.Send(message);

            //file
            //hook.Send(message, new FileInfo("C:/File/Path.file"));

        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            try
            {
                DiscordMessage message = new DiscordMessage();

                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    var Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        var Image = ImageSource.FromStream(() => stream);

                        //testImg.Source = Image;
                    }
                    var filest = new FileInfo(result.FullPath);
                    message.Content = filest.Length.ToString();
                    //discord max file size
                    if (filest.Length < 8388235)
                    {
                        //file
                        filepath = result.FullPath;
                    }
                    else _ = DisplayAlert("Error", "File can only be 8mb maximum", "Ok");
                }

            }
            catch
            {
                // The user canceled or something went wrong
            }
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
        #endregion

        private async void DiscordButton_Clicked(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://discord.gg/aX4unxzZek");
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task<bool> IsULCCheck(string url)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            Uri uriResult;
            bool isUrl = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return isUrl;
        }

        private async Task<DiscordEmbed> embedBuilder()
        {
            /*
             *thumbtail url embedin sağ üstündeki eklenicek
             *görüntü olarak video eklenicek
             *görüntü olarak resim eklenicek
             *footer eklenicek
             */
            try
            {
                //embeds
                DiscordEmbed embed = new DiscordEmbed();

                if (expEmbedAuthor.IsExpanded)
                {
                    var author = new EmbedAuthor();

                    //Setting Embed Author Title
                    if (!string.IsNullOrEmpty(EntryEmbedAuthorName.Text))
                        author.Name = EntryEmbedAuthorName.Text;

                    //Setting Embed Author Url
                    if (await IsULCCheck(EntryEmbedAuthorUrl.Text))
                        author.Url = EntryEmbedAuthorUrl.Text;

                    //Setting Embed Author Icon Url
                    if (ImgEmbedAuthor.Source.ToString() != "File: demoimage.png")
                        author.IconUrl = embedAuthorIconUrl;

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
                    if (await IsULCCheck(entryBodyUrl.Text))
                        embed.Url = entryBodyUrl.Text;

                    embed.Color = embedBodyColorPicker.Color;
                }

                return embed;
            }
            catch (Exception ex)
            {
                _ = DisplayAlert("Embed Error!", $"Message: {ex.Message}", "Ok");
                return new DiscordEmbed();
            }
        }

        //---------------------------------------------------------------------------------------------------

        #region Send File
        private async void btnFileSelect_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    var filest = new FileInfo(result.FullPath);
                    //discord max file size
                    if (filest.Length < 8388235)
                    {
                        lblSelectedFile.Text = result.FileName;
                        filepath = result.FullPath;
                    }
                    else
                    {
                        _ = DisplayAlert("Error", "File can only be 8mb maximum", "Ok");
                        lblSelectedFile.Text = "";
                        filepath = "";
                    }
                }

            }
            catch
            {
                // The user canceled or something went wrong
            }
        }
        #endregion

        #region Webhook Profile
        private void webhookSaveLoad_Clicked(object sender, EventArgs e)
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

            _ = DisplayAlert("Hello!", "This feature is coming soon...", "Ok");
            //Popup popup = new WebhookProfileSaveEditPopup(imgWebhookImage.Source.ToString(), entryWebhookName.Text);
            //var res = await App.Current.MainPage.Navigation.ShowPopupAsync(popup);
        }
        private async void WebhookImage_Tapped(object sender, EventArgs e)
        {
            try
            {
                string oldAvatarUrl = "";
                if (webhookProfileAvatar) oldAvatarUrl = webhookImageUrl;
                string result = await DisplayPromptAsync("Webhook Image", "Only \".jpg\", \".jpeg\", \".png\", \".gif\" Supported!", accept: "Ok", cancel: "Cancel", placeholder: "Image url.", initialValue: oldAvatarUrl);
                Debug.WriteLine(result);
                if (!string.IsNullOrEmpty(result.Trim()))
                {
                    string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    bool isImageUrl = imageExtensions.Any(result.EndsWith);

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
                    imgWebhookImage.Source = "demoimage.png";
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

        #region Webhook Url
        private async void WebhookAdd_Tapped(object sender, EventArgs e)
        {
            Popup popup = new WebhookAddEditPopup();
            await App.Current.MainPage.Navigation.ShowPopupAsync(popup);
            BindableLayout.SetItemsSource(blSavedWebhooks, References.WebhookList);
        }
        private void WebhookSelect_Tapped(object sender, EventArgs e)
        {
            var selected = References.WebhookList.Where(x => x.ID == Convert.ToInt32((sender as Frame).AutomationId)).FirstOrDefault();
            hook.Url = selected.url;
            selectedUrl = selected.url;
            selectedId = selected.ID;
            lblSelectedUrl.Text = selected.name;
            webhookSelected = true;
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
                    var selected = References.WebhookList.Where(x => x.ID == selectedId).FirstOrDefault();
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
    }
}
