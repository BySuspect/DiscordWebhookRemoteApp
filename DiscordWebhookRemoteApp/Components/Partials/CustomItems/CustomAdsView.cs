using MarcTron.Plugin.Controls;

namespace DiscordWebhookRemoteApp.Components.Partials.CustomItems
{
    public class CustomAdsView : MTAdView
    {
        private bool _isLoaded;

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                if (_isLoaded != value)
                {
                    _isLoaded = value;
                    OnPropertyChanged(nameof(IsLoaded));
                }
            }
        }

        public CustomAdsView()
            : base()
        {
            IsVisible = IsLoaded;
            AdsFailedToLoad += (s, e) =>
            {
                IsVisible = false;
                IsLoaded = false;
            };
            AdsLoaded += (s, e) =>
            {
                IsVisible = true;
                IsLoaded = true;
            };
        }
    }
}
