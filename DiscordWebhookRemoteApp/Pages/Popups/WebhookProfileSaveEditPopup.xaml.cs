using DiscordWebhookRemoteApp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace DiscordWebhookRemoteApp.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebhookProfileSaveEditPopup : Popup
    {
        ObservableCollection<webhookProfileItems> savedProfiles = new ObservableCollection<webhookProfileItems>();
        public WebhookProfileSaveEditPopup(string url, string name)
        {
            InitializeComponent();
            BindingContext = this;

            savedProfiles = References.WebhookProfileList;

            BindableLayout.SetItemsSource(ProfileList, savedProfiles);
        }
        private void btnSelect_Clicked(object sender, EventArgs e)
        {
            var selected = sender as Button;
            int a = savedProfiles.IndexOf(savedProfiles.Where(x => x.ID == int.Parse(selected.AutomationId)).FirstOrDefault());
        }
        private void btnEdit_Clicked(object sender, EventArgs e)
        {
            var selected = sender as Button;
            int a = savedProfiles.IndexOf(savedProfiles.Where(x => x.ID == int.Parse(selected.AutomationId)).FirstOrDefault());
        }
        private void deleteItem_Tapped(object sender, EventArgs e)
        {
            var selected = sender as Xamarin.Forms.Image;
            savedProfiles.Remove(savedProfiles.Where(x => x.ID == int.Parse(selected.AutomationId)).FirstOrDefault());
            //References.WebhookProfileList = savedProfiles;
            OnPropertyChanged(nameof(savedProfiles));
        }

    }
}