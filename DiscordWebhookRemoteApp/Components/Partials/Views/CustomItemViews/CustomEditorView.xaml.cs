using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEditorView : ContentView
    {
        #region Text Binding
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CustomEditorView),
            defaultBindingMode: BindingMode.TwoWay,
            defaultValue: ""
        );
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged(nameof(Text));
            }
        }
        #endregion

        #region Placeholder Binding
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(CustomEditorView),
            defaultBindingMode: BindingMode.TwoWay,
            defaultValue: ""
        );
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set
            {
                SetValue(PlaceholderProperty, value);
                OnPropertyChanged(nameof(Placeholder));
            }
        }
        #endregion

        #region MaxLength Binding
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
            nameof(MaxLength),
            typeof(string),
            typeof(CustomEditorView),
            defaultBindingMode: BindingMode.TwoWay,
            defaultValue: ""
        );
        public string MaxLength
        {
            get { return (string)GetValue(MaxLengthProperty); }
            set
            {
                SetValue(MaxLengthProperty, value);
                OnPropertyChanged(nameof(MaxLength));
            }
        }
        #endregion

        public CustomEditorView()
        {
            InitializeComponent();
            BindingContext = this;

            if (this.Text.Length > 0)
            {
                lblTitle.TextColor = Color.White;
                titleView.BorderColor = Color.White;
                titleView.BackgroundColor = Color.Black;
                titleView.TranslationX = 15;
                titleView.TranslationY = -9;
            }
            else
            {
                lblTitle.TextColor = Color.LightGray;
                titleView.BorderColor = Color.Transparent;
                titleView.BackgroundColor = Color.Transparent;
                titleView.TranslationX = 0;
                titleView.TranslationY = 13;
            }
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length >= Input.MaxLength)
                lblInputLenght.TextColor = Color.Red;
            else
                lblInputLenght.TextColor = Color.White;

            spCharacterCount.Text = e.NewTextValue.Length.ToString();

            if (e.NewTextValue.Length > 0)
            {
                titleView.CancelAnimations();
                lblTitle.TextColor = Color.White;
                titleView.BorderColor = Color.White;
                titleView.BackgroundColor = Color.Black;
                titleView.TranslateTo(15, -9, 150);
            }
            else
            {
                titleView.CancelAnimations();
                lblTitle.TextColor = Color.LightGray;
                titleView.BorderColor = Color.Transparent;
                titleView.BackgroundColor = Color.Transparent;
                titleView.TranslateTo(0, 13, 150);
            }
        }

        private void titleTapped(object sender, EventArgs e)
        {
            Input.Focus();
        }
    }
}
