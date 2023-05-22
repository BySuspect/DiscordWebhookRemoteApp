using DiscordWebhookRemoteApp.Helpers;
using IntelliAbb.Xamarin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DiscordWebhookRemoteApp.CustomItems
{
    public class CustomCheckBox : StackLayout
    {
        IntelliAbb.Xamarin.Controls.Checkbox _checkbox;
        Label _label;

        string _titleText;
        public string TitleText
        {
            get
            {
                return _titleText;
            }
            set
            {
                if (_titleText != value)
                {
                    _titleText = value;
                    OnPropertyChanged(nameof(TitleText));
                }
            }
        }

        bool _isChecked;
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                    OnPropertyChanged(nameof(IsCheckedOpposite));

                    IsCheckedChanged?.Invoke(this, _isChecked);
                }
            }
        }
        public bool IsCheckedOpposite
        {
            get
            {
                return !IsChecked;
            }
            set { }
        }

        Color _checkColor;
        public Color CheckColor
        {
            get
            {
                return _checkColor;
            }
            set
            {
                if (_checkColor != value)
                {
                    _checkColor = value;
                    OnPropertyChanged(nameof(CheckColor));
                }
            }
        }
        Color _fillColor;
        public Color FillColor
        {
            get
            {
                return _fillColor;
            }
            set
            {
                if (_fillColor != value)
                {
                    _fillColor = value;
                    OnPropertyChanged(nameof(FillColor));
                }
            }
        }
        Color _outlineColor;
        public Color OutlineColor
        {
            get
            {
                return _outlineColor;
            }
            set
            {
                if (_outlineColor != value)
                {
                    _outlineColor = value;
                    OnPropertyChanged(nameof(OutlineColor));
                }
            }
        }
        IntelliAbb.Xamarin.Controls.Shape _shape;
        public IntelliAbb.Xamarin.Controls.Shape Shape
        {
            get
            {
                return _shape;
            }
            set
            {
                if (_shape != value)
                {
                    _shape = value;
                    OnPropertyChanged(nameof(Shape));
                }
            }
        }
        public CustomCheckBox() : base()
        {
            BindingContext = this;

            _checkbox = new IntelliAbb.Xamarin.Controls.Checkbox();
            _checkbox.SetBinding(IntelliAbb.Xamarin.Controls.Checkbox.IsCheckedProperty, new Binding("IsChecked"));
            _checkbox.SetBinding(IntelliAbb.Xamarin.Controls.Checkbox.CheckColorProperty, new Binding("CheckColor"));
            _checkbox.SetBinding(IntelliAbb.Xamarin.Controls.Checkbox.FillColorProperty, new Binding("FillColor"));
            _checkbox.SetBinding(IntelliAbb.Xamarin.Controls.Checkbox.OutlineColorProperty, new Binding("OutlineColor"));
            _checkbox.SetBinding(IntelliAbb.Xamarin.Controls.Checkbox.ShapeProperty, new Binding("Shape"));
            _checkbox.VerticalOptions = LayoutOptions.Center;
            _checkbox.OutlineColor = ThemeColors.TextColor;
            _checkbox.FillColor = ThemeColors.TextColor;
            _checkbox.CheckColor = ThemeColors.BackColor;
            _checkbox.Shape = IntelliAbb.Xamarin.Controls.Shape.Circle;

            _label = new Label();
            _label.Margin = new Thickness(0, 0, 0, 0);
            _label.SetBinding(Label.TextProperty, new Binding("TitleText"));
            _label.FontSize = Device.GetNamedSize(NamedSize.Body, typeof(Label));
            _label.HorizontalOptions = LayoutOptions.Start;
            _label.TextColor = ThemeColors.TextColor;
            _label.VerticalOptions = LayoutOptions.Center;


            var _lbltapped = new TapGestureRecognizer();
            _lbltapped.Tapped += (s, e) =>
            {
                _checkbox.IsChecked = !_checkbox.IsChecked;
            };

            _label.GestureRecognizers.Add(_lbltapped);
            this.GestureRecognizers.Add(_lbltapped);

            this.Orientation = StackOrientation.Horizontal;

            this.Children.Add(_checkbox);
            this.Children.Add(_label);
        }
        public event EventHandler<bool> IsCheckedChanged;
    }
}
