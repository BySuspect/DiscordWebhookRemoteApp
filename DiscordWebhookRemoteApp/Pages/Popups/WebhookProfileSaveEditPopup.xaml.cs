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
    public partial class WebhookProfileSaveEditPopup : Popup
    {
        //List<webhookItems> webhookList = new List<webhookItems>();
        //int selectedID = -1;
        //bool IsEditOrDelete = false;
        public WebhookProfileSaveEditPopup(string url, string name)
        {
            InitializeComponent();
        }
    }
}