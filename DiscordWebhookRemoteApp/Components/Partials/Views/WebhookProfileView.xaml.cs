using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Components.Partials.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebhookProfileView : ContentView
    {
        public static readonly BindableProperty AvatarImageSourceProperty = BindableProperty.Create(
            nameof(AvatarImageSource),
            typeof(string),
            typeof(WebhookProfileView)
        );
        public string AvatarImageSource
        {
            get { return (string)GetValue(AvatarImageSourceProperty); }
            set { SetValue(AvatarImageSourceProperty, value); }
        }

        public static readonly BindableProperty UsernameProperty = BindableProperty.Create(
            nameof(Username),
            typeof(string),
            typeof(WebhookProfileView)
        );
        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public WebhookProfileView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void Avatar_Tapped(object sender, EventArgs e)
        {
            var res = await Application.Current.MainPage.DisplayPromptAsync(
                title: "Change Avatar",
                message: "Enter a new URL for the avatar",
                accept: "OK",
                cancel: "Cancel",
                placeholder: "https://example.com/image.jpg",
                initialValue: AvatarImageSource
            );
            if (res != null)
            {
                AvatarImageSource = res;
            }
        }
    }
}
