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
    public partial class GooglePlayViewPopup : Popup
    {
        public GooglePlayViewPopup()
        {
            InitializeComponent();
        }
    }
}