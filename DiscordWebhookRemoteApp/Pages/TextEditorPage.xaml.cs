using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextEditorPage : ContentPage
    {
        public TextEditorPage(string text)
        {
            InitializeComponent();
            BindingContext = this;
            Content.Text = text;
            Content.Unfocus();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void OnBackButtonClicked(object sender, EventArgs e) => OnBackButtonPressed();
        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {

        }

        private void Content_Focused(object sender, FocusEventArgs e)
        {
            //settingsBtn.IsVisible = false;
        }

        private void Content_Unfocused(object sender, FocusEventArgs e)
        {
            //settingsBtn.IsVisible = true;
        }

        private void Content_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Content_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}