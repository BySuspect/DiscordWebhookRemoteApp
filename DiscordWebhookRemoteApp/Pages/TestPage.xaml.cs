using DiscordWebhookRemoteApp.Pages.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
        }
        public TestPage(string content)
        {
            InitializeComponent();
            testlbl.Text = content;
        }
        [Obsolete]
        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("http://192.168.114.1:5000/DCToolsWebhook"));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            testlbl.Text = "test31";
        }
    }
}