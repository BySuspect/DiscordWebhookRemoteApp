using DiscordWebhookRemoteApp.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace DiscordWebhookRemoteApp.CustomItems
{
    public class CustomBoxView : BoxView
    {
        Color _color;

        string _colorHexText;
        public string ColorHexText
        {
            get
            {
                return _colorHexText;
            }
            set
            {
                if (_colorHexText != value)
                {
                    _colorHexText = value;
                    OnPropertyChanged(nameof(ColorHexText));
                }
            }
        }
        Color _embedReverseColor;
        public Color EmbedReverseColor
        {
            get
            {
                return _embedReverseColor;
            }
            set
            {
                if (_embedReverseColor != value)
                {
                    _embedReverseColor = value;
                    OnPropertyChanged(nameof(EmbedReverseColor));
                }
            }
        }
        public CustomBoxView() : base()
        {
            BindingContext = this;
            ColorHexText = this.Color.ToHex();
            EmbedReverseColor = Color.FromRgb(1 - this.Color.R, 1 - this.Color.G, 1 - this.Color.B);
            _color = this.Color;
            this.PropertyChanged += (s, e) =>
            {
                if (_color != Color) OnColorChanged(new ColorChangedEventArgs(_color, Color));
            };
            this.ColorChanged += (s, e) =>
            {
                Debug.WriteLine($"OldColor: {e.OldColor.ToHex()} NewColor: {e.NewColor.ToHex()}");
                _color = e.NewColor;
                ColorHexText = this.Color.ToHex();
                Color inverseColor = Color.FromRgb(1 - this.Color.R, 1 - this.Color.G, 1 - this.Color.B);
                int colorBrightness = (int)Math.Sqrt(
                inverseColor.R * inverseColor.R * .241 +
                    inverseColor.G * inverseColor.G * .691 +
                    inverseColor.B * inverseColor.B * .068);
                EmbedReverseColor = colorBrightness > 130 ? Color.Black : Color.White;

                Debug.WriteLine($"reverse color: {EmbedReverseColor.ToHex()} color hexText: {ColorHexText}");
            };
        }
        public event EventHandler<ColorChangedEventArgs> ColorChanged;
        protected virtual void OnColorChanged(ColorChangedEventArgs e)
        {
            EventHandler<ColorChangedEventArgs> handler = ColorChanged;
            handler?.Invoke(this, e);
        }
    }
    public class ColorChangedEventArgs : EventArgs
    {
        public ColorChangedEventArgs(Color oldColor, Color newColor)
        {
            OldColor = oldColor;
            NewColor = newColor;
        }

        public Color OldColor { get; private set; }
        public Color NewColor { get; private set; }
    }
}
