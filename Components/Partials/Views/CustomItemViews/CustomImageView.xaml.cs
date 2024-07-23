namespace DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;

public partial class CustomImageView : ContentView
{
    #region WidthRequest Binding
    public static new readonly BindableProperty WidthRequestProperty = BindableProperty.Create(
        nameof(WidthRequest),
        typeof(int),
        typeof(CustomImageView),
        defaultValue: -1,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = bindable as CustomImageView;
        }
    );
    public new int WidthRequest
    {
        get { return (int)GetValue(WidthRequestProperty); }
        set
        {
            SetValue(WidthRequestProperty, value);
            OnPropertyChanged(nameof(WidthRequest));
        }
    }
    #endregion

    #region HeightRequest Binding
    public static new readonly BindableProperty HeightRequestProperty = BindableProperty.Create(
        nameof(HeightRequest),
        typeof(int),
        typeof(CustomImageView),
        defaultValue: -1,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = bindable as CustomImageView;
        }
    );
    public new int HeightRequest
    {
        get { return (int)GetValue(HeightRequestProperty); }
        set
        {
            SetValue(HeightRequestProperty, value);
            OnPropertyChanged(nameof(HeightRequest));
        }
    }
    #endregion

    #region CornerRadius Binding
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(int),
        typeof(CustomImageView),
        defaultValue: 0,
        defaultBindingMode: BindingMode.TwoWay
    );
    public int CornerRadius
    {
        get { return (int)GetValue(CornerRadiusProperty); }
        set
        {
            SetValue(CornerRadiusProperty, value);
            OnPropertyChanged(nameof(CornerRadius));
        }
    }
    #endregion

    #region Source Binding
    public static readonly BindableProperty SourceProperty = BindableProperty.Create(
        nameof(Source),
        typeof(string),
        typeof(CustomImageView),
        default(string)
    );

    public string Source
    {
        get { return (string)GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); }
    }
    #endregion

    #region BorderColor Binding
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Color),
        typeof(CustomImageView),
        Colors.Transparent
    );

    public Color BorderColor
    {
        get { return (Color)GetValue(BorderColorProperty); }
        set { SetValue(BorderColorProperty, value); }
    }
    #endregion

    public CustomImageView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    #region Clicked Event
    public event EventHandler<ClickedEventArgs> Clicked;

    protected virtual void OnClicked(ClickedEventArgs? e)
    {
        EventHandler<ClickedEventArgs> handler = Clicked;
        handler?.Invoke(this, e);
    }
    #endregion

    #region Tapped Event
    public event EventHandler<TappedEventArgs> Tapped;

    protected virtual void OnTapped(TappedEventArgs e)
    {
        EventHandler<TappedEventArgs> handler = Tapped;
        handler?.Invoke(this, e);
    }
    #endregion

    private void Image_Tapped(object sender, TappedEventArgs e)
    {
        OnClicked(null);
        OnTapped(e);
    }
}
