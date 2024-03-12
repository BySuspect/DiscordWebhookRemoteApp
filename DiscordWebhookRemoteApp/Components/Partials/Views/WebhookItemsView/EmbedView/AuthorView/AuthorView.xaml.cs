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
        public static readonly BindableProperty AuthorIconUrlProperty = BindableProperty.Create(
            nameof(AuthorIconUrl),
            typeof(string),
            typeof(AuthorView),
            defaultBindingMode: BindingMode.TwoWay
        );
        public string AuthorIconUrl
        {
            get { return (string)GetValue(AuthorIconUrlProperty); }
            set
            {
                SetValue(AuthorIconUrlProperty, value);
                OnPropertyChanged(nameof(AuthorIconUrl));
            }
        }
        #endregion

        #region AuthorName Binding
        public static readonly BindableProperty AuthorNameProperty = BindableProperty.Create(
            nameof(AuthorName),
            typeof(string),
            typeof(AuthorView),
            defaultBindingMode: BindingMode.TwoWay
        );
        public string AuthorName
        {
            get { return (string)GetValue(AuthorNameProperty); }
            set
            {
                SetValue(AuthorNameProperty, value);
                OnPropertyChanged(nameof(AuthorName));
            }
        }
        #endregion

        #region AuthorUrl Binding
        public static readonly BindableProperty AuthorUrlProperty = BindableProperty.Create(
            nameof(AuthorUrl),
            typeof(string),
            typeof(AuthorView),
            defaultBindingMode: BindingMode.TwoWay
        );
        public string AuthorUrl
        {
            get { return (string)GetValue(AuthorUrlProperty); }
            set
            {
                SetValue(AuthorUrlProperty, value);
                OnPropertyChanged(nameof(AuthorUrl));
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
