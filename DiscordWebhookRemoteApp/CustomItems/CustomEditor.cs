using DiscordWebhookRemoteApp.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace DiscordWebhookRemoteApp.CustomItems
{

    public class CustomEditor : BorderlessEditor
    {
        int _textLenght;
        public int TextLenght
        {
            get
            {
                return _textLenght;
            }
            set
            {
                if (_textLenght != value)
                {
                    _textLenght = value;
                    OnPropertyChanged(nameof(TextLenght));
                }
            }
        }
        Color _sizeLabelTextColor;
        public Color SizeLabelTextColor
        {
            get
            {
                return _sizeLabelTextColor;
            }
            set
            {
                if (_sizeLabelTextColor != value)
                {
                    _sizeLabelTextColor = value;
                    OnPropertyChanged(nameof(SizeLabelTextColor));
                }
            }
        }
        public CustomEditor() : base()
        {
            BindingContext = this;
            TextLenght = 0;
            SizeLabelTextColor = ThemeColors.TransparentTextColor;
            this.TextChanged += (s, e) =>
            {
                TextLenght = e.NewTextValue.Length;
                if (e.NewTextValue.Length >= MaxLength) SizeLabelTextColor = Color.Red;
                else SizeLabelTextColor = ThemeColors.TransparentTextColor;
                Debug.WriteLine(e.NewTextValue.Length + "/" + MaxLength);
            };
        }
    }
}
