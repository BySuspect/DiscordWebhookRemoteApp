using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.ImagesView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagesView : ContentView
    {
        #region ImagesImageUrl Binding
        public string ImagesImageUrl
        {
            get { return entryImagesImageUrl.Text; }
            set
            {
                if (entryImagesImageUrl.Text != value)
                {
                    entryImagesImageUrl.Text = value;
                }
            }
        }
        #endregion

        #region ImagesThumbnailUrl Binding
        public string ImagesThumbnailUrl
        {
            get { return entryImagesThumbnailUrl.Text; }
            set
            {
                if (entryImagesThumbnailUrl.Text != value)
                {
                    entryImagesThumbnailUrl.Text = value;
                }
            }
        }
        #endregion
        public ImagesView()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
