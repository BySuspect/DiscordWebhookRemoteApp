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
            get { return _authorView.AuthorIconUrl; }
            set { _authorView.AuthorIconUrl = value; }
        }
        public string AuthorName
        {
            get { return _authorView.AuthorName; }
            set { _authorView.AuthorName = value; }
        }
        public string AuthorUrl
        {
            get { return _authorView.AuthorUrl; }
            set { _authorView.AuthorUrl = value; }
        }
        #endregion

        #region BodyView
        public string BodyTitle
        {
            get { return _bodyView.BodyTitle; }
            set { _bodyView.BodyTitle = value; }
        }
        public string BodyContent
        {
            get { return _bodyView.BodyContent; }
            set { _bodyView.BodyContent = value; }
        }
        public string BodyUrl
        {
            get { return _bodyView.BodyUrl; }
            set { _bodyView.BodyUrl = value; }
        }
        public Color BodyColor
        {
            get { return _bodyView.BodyColor; }
            set { _bodyView.BodyColor = value; }
        }
        #endregion

        #region FooterView
        public string FooterText
        {
            get { return _footerView.FooterText; }
            set { _footerView.FooterText = value; }
        }

        public string FooterIconUrl
        {
            get { return _footerView.FooterIconUrl; }
            set { _footerView.FooterIconUrl = value; }
        }

        public bool FooterTimestamp
        {
            get { return _footerView.FooterTimestamp; }
            set { _footerView.FooterTimestamp = value; }
        }
        #endregion

        public EmbedView()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
