using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.BodyView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BodyView : ContentView, INotifyPropertyChanged
    {
        #region BodyTitle Binding
        public string BodyTitle
        {
            get { return entryBodyTitle.Text; }
            set
            {
                if (entryBodyTitle.Text != value)
                {
                    entryBodyTitle.Text = value;
                }
            }
        }
        #endregion

        #region BodyContent Binding
        public string BodyContent
        {
            get { return editorBodyContent.Text; }
            set
            {
                if (editorBodyContent.Text != value)
                {
                    editorBodyContent.Text = value;
                }
            }
        }
        #endregion

        #region BodyUrl Binding
        public string BodyUrl
        {
            get { return entryBodyUrl.Text; }
            set
            {
                if (entryBodyUrl.Text != value)
                {
                    entryBodyUrl.Text = value;
                }
            }
        }
        #endregion

        #region BodyColor Binding
        public Color BodyColor
        {
            get { return Color.FromHex(entryBodyColor.Text); }
            set
            {
                if (Color.FromHex(entryBodyColor.Text) != value)
                {
                    entryBodyColor.Text = value.ToHex();
                }
            }
        }
        #endregion
        public BodyView()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
