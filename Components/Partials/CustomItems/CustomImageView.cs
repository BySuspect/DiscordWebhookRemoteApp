using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordWebhookRemoteApp.Components.Partials.CustomItems
{
    public class CustomImageView : Frame
    {
        #region Source Binding
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source),
            typeof(string),
            typeof(CustomImageView),
            default(string),
            propertyChanged: OnSourcePropertyChanged
        );

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        private static void OnSourcePropertyChanged(
            BindableObject bindable,
            object oldValue,
            object newValue
        )
        {
            var customImageView = (CustomImageView)bindable;
            customImageView.OnPropertyChanged(nameof(Source));
        }
        #endregion

        private string errorSource;
        public string ErrorSource
        {
            get { return errorSource; }
            set
            {
                errorSource = value;
                OnPropertyChanged(nameof(ErrorSource));
            }
        }

        public CustomImageView()
        {
            var image = new Image();

            this.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Source))
                {
                    image.Source = ImageService.isImage(Source) ? Source : ErrorSource;
                }
            };

            this.BackgroundColor = Colors.Transparent;
            this.Padding = 0;
            this.Content = image;
        }
    }
}
