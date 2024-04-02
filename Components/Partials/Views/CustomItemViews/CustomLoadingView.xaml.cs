namespace DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;

public partial class CustomLoadingView : ContentView
{
    #region LoadingViewVisible Binding
    public static readonly BindableProperty LoadingViewVisibleProperty = BindableProperty.Create(
        nameof(LoadingViewVisible),
        typeof(bool),
        typeof(CustomEntryView),
        defaultValue: false,
        defaultBindingMode: BindingMode.TwoWay
    );
    public bool LoadingViewVisible
    {
        get { return (bool)GetValue(LoadingViewVisibleProperty); }
        set
        {
            SetValue(LoadingViewVisibleProperty, value);
            OnPropertyChanged(nameof(LoadingViewVisible));
        }
    }
    #endregion
    public CustomLoadingView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public void ShowLoadingLayout()
    {
        LoadingViewVisible = true;
    }

    public void HideLoadingLayout()
    {
        LoadingViewVisible = false;
    }
}
