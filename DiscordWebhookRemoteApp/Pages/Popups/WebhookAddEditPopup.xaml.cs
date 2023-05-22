using DiscordWebhookRemoteApp.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebhookAddEditPopup : Popup
    {
        List<webhookItems> webhookList = new List<webhookItems>();
        int selectedID = -1;
        bool IsEditOrDelete = false;
        public WebhookAddEditPopup()
        {
            InitializeComponent();
#if DEBUG
            //string hookurl = "";
            //entryUrl.Text = hookurl;
#endif
        }
        public WebhookAddEditPopup(int id)
        {
            InitializeComponent();
            selectedID = id;
            webhookList = References.WebhookList;
            var selected = webhookList.Where(x => x.ID == selectedID).FirstOrDefault();
            entryName.Text = selected.name;
            entryUrl.Text = selected.url;
            IsEditOrDelete = true;
        }

        private void DeleteWebhook_Clicked(object sender, EventArgs e)
        {
            if (IsEditOrDelete)
            {
                webhookList = References.WebhookList;
                webhookList.Remove(webhookList.Where(x => x.ID == selectedID).FirstOrDefault());
                References.WebhookList = webhookList;
                this.Dismiss("delete");
            }
            else
            {
                Dismiss("");
            }
        }
        private void SaveWebhook_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(entryName.Text.Trim()) || string.IsNullOrEmpty(entryUrl.Text.Trim())) return; webhookList = References.WebhookList;
                if (!entryUrl.Text.Trim().StartsWith("https://discord.com") || !entryUrl.Text.Trim().Contains("webhook"))
                {
                    entryUrl.BackgroundColor = Color.Red;
                }
                else
                {
                    webhookList = References.WebhookList;
                    if (!IsEditOrDelete)
                    {
                        int id = 0;

                        while (true)
                        {
                            Debug.WriteLine(id + " " + webhookList.Exists(x => x.ID == id));
                            if (!webhookList.Exists(x => x.ID == id)) break;
                            id++;
                        }
                        webhookList.Add(new webhookItems
                        {
                            ID = id,
                            name = entryName.Text,
                            url = entryUrl.Text,
                        });
                        References.WebhookList = webhookList;
                        this.Dismiss("");
                    }
                    else
                    {
                        webhookList = References.WebhookList;
                        webhookList[webhookList.FindIndex(x => x.ID == selectedID)].name = entryName.Text;
                        webhookList[webhookList.FindIndex(x => x.ID == selectedID)].url = entryUrl.Text;
                        References.WebhookList = webhookList;
                        Dismiss("");
                    }
                }
            }
            catch
            {

            }
        }

        private void entryUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            entryUrl.BackgroundColor = Color.Transparent;
        }
    }
}