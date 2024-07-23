using Plugin.MauiMTAdmob.Controls;

namespace DiscordWebhookRemoteApp.Components.Partials.CustomItems
{
    public class CustomAdsView : MTAdView
    {
        private bool isLoaded;

        public new bool IsLoaded
        {
            get { return isLoaded; }
            set
            {
                if (isLoaded != value)
                {
                    isLoaded = value;
                    OnPropertyChanged(nameof(IsLoaded));
                }
            }
        }

        public CustomAdsView()
            : base()
        {
            this.IsVisible = IsLoaded;
            this.AdsFailedToLoad += (s, e) =>
            {
                IsVisible = false;
                IsLoaded = false;
            };
            this.AdsLoaded += (s, e) =>
            {
                IsVisible = true;
                IsLoaded = true;
            };
        }
    }
}
