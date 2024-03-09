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
    public partial class WebhookMessageContentView : ContentView
    {
        public static readonly BindableProperty MessageContentProperty = BindableProperty.Create(
            nameof(MessageContent),
            typeof(string),
            typeof(WebhookProfileView)
        );
        public string MessageContent
        {
            get { return (string)GetValue(MessageContentProperty); }
            set { SetValue(MessageContentProperty, value); }
        }

        public WebhookMessageContentView()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
