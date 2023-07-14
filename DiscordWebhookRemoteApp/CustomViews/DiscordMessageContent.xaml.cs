using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiscordMessageContent : ContentView
    {
        public DiscordMessageContent()
        {
            InitializeComponent();
            BindingContext = this;
        }

        #region Image Section
        private string profileImage;
        public string ProfileImage
        {
            get
            {
                return profileImage;
            }
            set
            {
                if (profileImage != value)
                {
                    profileImage = value;
                    OnPropertyChanged(nameof(ProfileImage));
                }
            }
        }
        #endregion

        #region Name Section
        private string profileName;
        public string ProfileName
        {
            get
            {
                return profileName;
            }
            set
            {
                if (profileName != value)
                {
                    profileName = value;
                    OnPropertyChanged(nameof(ProfileName));
                }
            }
        }

        //Message Send Time
        private string messageTime;
        public string MessageTime
        {
            get
            {
                return messageTime;
            }
            set
            {
                if (messageTime != value)
                {
                    messageTime = value;
                    OnPropertyChanged(nameof(MessageTime));
                }
            }
        }
        #endregion

        #region Content Section
        private string messageContent;
        public string MessageContent
        {
            get
            {
                return messageContent;
            }
            set
            {
                if (messageContent != value)
                {
                    messageContent = value;
                    OnPropertyChanged(nameof(MessageContent));
                }
            }
        }
        #endregion

        #region Images Section
        private List<ImagesItems> imagesList;
        public List<ImagesItems> ImagesList
        {
            get
            {
                return imagesList;
            }
            set
            {
                if (value != imagesList)
                {
                    imagesList = value;
                    ImagesLayout.SetBinding(BindableLayout.ItemsSourceProperty, "ImagesList");
                    OnPropertyChanged(nameof(ImagesItems));
                }
            }
        }

        public class ImagesItems
        {
            public string url { get; set; }
        }

        #endregion
    }
}