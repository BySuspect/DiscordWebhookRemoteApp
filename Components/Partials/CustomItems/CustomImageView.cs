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
            image.Source = ImageService.isImage(Source) ? Source : ErrorSource;

            this.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Source))
                {
                    image.Source = ImageService.isImage(Source) ? Source : ErrorSource;
                }
                if (e.PropertyName == nameof(this.WidthRequest))
                {
                    image.WidthRequest = this.WidthRequest;
                }
                if (e.PropertyName == nameof(this.HeightRequest))
                {
                    image.HeightRequest = this.HeightRequest;
                }
            };

            this.BackgroundColor = Colors.Transparent;
            this.Padding = 0;
            this.Content = image;
        }
    }
}
