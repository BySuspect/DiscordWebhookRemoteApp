using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageContentView : ContentView
    {
        public static readonly BindableProperty MessageContentProperty = BindableProperty.Create(
            nameof(MessageContent),
            typeof(string),
            typeof(MessageContentView)
        );
        public string MessageContent
        {
            get { return (string)GetValue(MessageContentProperty); }
            set { SetValue(MessageContentProperty, value); }
        }

        public MessageContentView()
        {
            InitializeComponent();
            BindingContext = this;
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
