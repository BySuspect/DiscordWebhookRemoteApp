using System.ComponentModel;

namespace DiscordWebhookRemoteApp.Components.Partials.CustomItems
{
    public class CustomImage : Image
    {
        #region IsLoaded Binding
        public static readonly BindableProperty IsLoadedProperty = BindableProperty.Create(
            nameof(IsLoaded),
            typeof(bool),
            typeof(CustomImage),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay
        );
        public bool IsLoaded
        {
            get { return (bool)GetValue(IsLoadedProperty); }
            set
            {
                SetValue(IsLoadedProperty, value);
                OnPropertyChanged(nameof(IsLoaded));
            }
        }
        #endregion

        #region IsLoading Binding
        public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(CustomImage),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay
        );
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set
            {
                SetValue(IsLoadingProperty, value);
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        #endregion

        public CustomImage()
        {
            IsLoading = true;
            this.Loaded += (object? sender, EventArgs e) =>
            {
                this.IsAnimationPlaying = false;

                IsLoaded = true;
                IsLoading = false;

                this.IsAnimationPlaying = true;
            };

            this.Unloaded += (object? sender, EventArgs e) =>
            {
                IsLoaded = false;
            };

            this.PropertyChanged += (object? sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == nameof(Source))
                {
                    var test = Source.ToString();
                }
            };
        }
    }
}
