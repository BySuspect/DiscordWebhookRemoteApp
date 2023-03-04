using DiscordWebhookRemoteApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace DiscordWebhookRemoteApp.CustomItems
{
    public class CustomExpander : Expander
    {
        IntelliAbb.Xamarin.Controls.Checkbox _checkbox;
        Label _label;
        StackLayout _imgStackLayout;
        Image _image;

        string _headerText;
        public string HeaderText
        {
            get
            {
                return _headerText;
            }
            set
            {
                if (_headerText != value)
                {
                    _headerText = value;
                    OnPropertyChanged(nameof(HeaderText));
                }
            }
        }

        public CustomExpander() : base()
        {
            BindingContext = this;

            _checkbox = new IntelliAbb.Xamarin.Controls.Checkbox();
            _checkbox.SetBinding(IntelliAbb.Xamarin.Controls.Checkbox.IsCheckedProperty, new Binding("IsExpanded"));
            _checkbox.VerticalOptions = LayoutOptions.Center;
            _checkbox.OutlineColor = ThemeColors.TextColor;
            _checkbox.FillColor = ThemeColors.TextColor;
            _checkbox.CheckColor = ThemeColors.BackColor;
            _checkbox.Shape = IntelliAbb.Xamarin.Controls.Shape.Circle;
            _checkbox.IsCheckedChanged += (s, e) =>
            {
                this.IsExpanded = _checkbox.IsChecked;
            };

            _label = new Label();
            _label.Margin = new Thickness(0, 0, 0, 0);
            _label.FontSize = Device.GetNamedSize(NamedSize.Body, typeof(Label));
            _label.HorizontalOptions = LayoutOptions.Start;
            _label.SetBinding(Label.TextProperty, new Binding("HeaderText"));
            _label.TextColor = ThemeColors.TextColor;
            _label.VerticalOptions = LayoutOptions.Center;

            _imgStackLayout = new StackLayout();
            _imgStackLayout.HorizontalOptions = LayoutOptions.EndAndExpand;
            _imgStackLayout.Rotation = 90;
            _imgStackLayout.VerticalOptions = LayoutOptions.Center;

            _image = new Image();
            _image.Source = "downarrow32px.png";
            _image.Scale = 0.8;

            _imgStackLayout.Children.Add(_image);

            this.PropertyChanged += (s, e) =>
            {
                _checkbox.IsChecked = this.IsExpanded;
                if (this.IsExpanded)
                {
                    _imgStackLayout.RotateTo(0, 100);
                }
                else _imgStackLayout.RotateTo(90, 100);
            };

            this.Header = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(5, 0, 0, 0),
                Children =
                {
                    _checkbox,
                    _label,
                    _imgStackLayout,
                }
            };
        }
    }
}
