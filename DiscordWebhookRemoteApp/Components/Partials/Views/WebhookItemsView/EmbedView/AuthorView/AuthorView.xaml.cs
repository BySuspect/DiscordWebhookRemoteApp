using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.AuthorView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorView : ContentView
    {
        #region AuthorIconUrl Binding
        public string AuthorIconUrl
        {
            get { return entryAuthorIconUrl.Text; }
            set
            {
                if (entryAuthorIconUrl.Text != value)
                {
                    entryAuthorIconUrl.Text = value;
                }
            }
        }

        #endregion

        #region AuthorName Binding

        public string AuthorName
        {
            get { return entryAuthorName.Text; }
            set
            {
                if (entryAuthorName.Text != value)
                {
                    entryAuthorName.Text = value;
                }
            }
        }

        #endregion

        #region AuthorUrl Binding
        public string AuthorUrl
        {
            get { return entryAuthorUrl.Text; }
            set
            {
                if (entryAuthorUrl.Text != value)
                {
                    entryAuthorUrl.Text = value;
                }
            }
        }

        #endregion

        public AuthorView()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
