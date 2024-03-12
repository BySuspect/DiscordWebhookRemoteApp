using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmbedView : ContentView
    {
        #region AuthorView
        public string AuthorIconUrl
        {
            get { return AuthorView.AuthorIconUrl; }
            set { AuthorView.AuthorIconUrl = value; }
        }
        public string AuthorName
        {
            get { return AuthorView.AuthorName; }
            set { AuthorView.AuthorName = value; }
        }
        public string AuthorUrl
        {
            get { return AuthorView.AuthorUrl; }
            set { AuthorView.AuthorUrl = value; }
        }
        #endregion

        public EmbedView()
        {
            InitializeComponent();
        }
    }
}
