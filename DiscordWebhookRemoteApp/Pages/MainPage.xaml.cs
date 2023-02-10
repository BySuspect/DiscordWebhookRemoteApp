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

        bool sendFile = false, sendMessage = false, sendEmbed = false, webhookSelected = false;
        string selectedUrl = "", webhookImageUrl = "", webhookName = "", filepath = "";
        int selectedId = -1;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            BindableLayout.SetItemsSource(blSavedWebhooks, References.WebhookList);
            DisplayAlert("Hello!", "This is the first version of my app. Please post your feedback in google play comments.", "Ok");
            Theme.ThemeChanged += (s, e) =>
            {
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
            };
        }

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
            //embed.Provider = new EmbedProvider() { Name = "Provider Name", Url = "Provider Url" };
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
            hook.Send(message);

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
                        hook.Send(message, new FileInfo(result.FullPath));
                    }
                    else _ = DisplayAlert("Error", "File can only be 8mb maximum", "Ok");
                }

            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }
        }

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
            }
            BindableLayout.SetItemsSource(blSavedWebhooks, References.WebhookList);
        }
        private void webhookSaveLoad_Clicked(object sender, EventArgs e)
        {

        }
        private void clearContent_Clicked(object sender, EventArgs e)
        {

        }
        private void sendContent_Clicked(object sender, EventArgs e)
        {
            DiscordMessage message = new DiscordMessage();
            message.Content = entryContentMessage.Text;
            hook.Send(message);
        }

        private void cbMessageContent_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            expMessageContent.IsExpanded = cbMessageContent.IsChecked;
        }

        private void expMessageContent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            cbMessageContent.IsChecked = expMessageContent.IsExpanded;
            sendMessage = cbMessageContent.IsChecked;
            //Debug.WriteLine(sendMessage);
            if (expMessageContent.IsExpanded)
            {
                (imgMCExpand.Parent as StackLayout).RotateTo(0, 100);
            }
            else (imgMCExpand.Parent as StackLayout).RotateTo(90, 100);
        }

        //---------------------------------------------------------------------------------------------------
        private void cbFileSend_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            expFileSend.IsExpanded = cbFileSend.IsChecked;
        }
        private void expFileSend_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            cbFileSend.IsChecked = expFileSend.IsExpanded;
            sendFile = cbFileSend.IsChecked;
            //Debug.WriteLine(sendFile);
            if (expFileSend.IsExpanded)
            {
                (imgFExpand.Parent as StackLayout).RotateTo(0, 100);
            }
            else (imgFExpand.Parent as StackLayout).RotateTo(90, 100);
        }


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
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }
        }
    }
}
