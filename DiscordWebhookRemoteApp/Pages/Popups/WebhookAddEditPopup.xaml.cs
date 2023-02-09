using DiscordWebhookRemoteApp.Helpers;
using System;
using System.Collections.Generic;
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
        public WebhookAddEditPopup()
        {
            InitializeComponent();
        }
        public WebhookAddEditPopup(int id)
        {
            InitializeComponent();
            selectedID = id;
        }

        private void DeleteWebhook_Clicked(object sender, EventArgs e)
        {

        }
        private void SaveWebhook_Clicked(object sender, EventArgs e)
        {
            webhookList = References.WebhookList;
            webhookList.Add(new webhookItems
            {
                ID = new Random().Next(0, 10000),
                name = entryName.Text,
                url = entryUrl.Text,
            });
            References.WebhookList = webhookList;

        }
    }
}