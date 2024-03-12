using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Xamarin.Forms.Color;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.WebhookProfileView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebhookProfileView : ContentView, INotifyPropertyChanged
    {
        public static readonly BindableProperty AvatarImageSourceProperty = BindableProperty.Create(
            nameof(AvatarImageSource),
            typeof(string),
            typeof(WebhookProfileView),
            defaultBindingMode: BindingMode.TwoWay
        );
        public string AvatarImageSource
        {
            get { return (string)GetValue(AvatarImageSourceProperty); }
            set
            {
                SetValue(AvatarImageSourceProperty, value);
                OnPropertyChanged(nameof(AvatarImageSource));
            }
        }

        public static readonly BindableProperty UsernameProperty = BindableProperty.Create(
            nameof(Username),
            typeof(string),
            typeof(WebhookProfileView),
            defaultBindingMode: BindingMode.TwoWay
        );
        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set
            {
                SetValue(UsernameProperty, value);
                OnPropertyChanged(nameof(Username));
            }
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

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= Input.MaxLength)
                lblInputLenght.TextColor = Color.Red;
            else
                lblInputLenght.TextColor = Color.White;

            spCharacterCount.Text = e.NewTextValue.Length.ToString();
        }
    }
}
