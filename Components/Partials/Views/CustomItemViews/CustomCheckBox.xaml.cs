namespace DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;

public partial class CustomCheckBox : ContentView
{
    #region Color Binding
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(
        nameof(Color),
        typeof(Color),
        typeof(CustomCheckBox),
        AppThemeColors.TextColor,
        BindingMode.TwoWay
    );
    public Color Color
    {
        get { return (Color)GetValue(ColorProperty); }
        set
        {
            SetValue(ColorProperty, value);
            OnPropertyChanged(nameof(Color));
        }
    }
    #endregion

    #region Title Binding
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(CustomCheckBox),
        "",
        BindingMode.TwoWay
    );
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set
        {
            SetValue(TitleProperty, value);
            OnPropertyChanged(nameof(Title));
        }
    }
    #endregion

    #region IsChecked Binding
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
        nameof(IsChecked),
        typeof(bool),
        typeof(CustomCheckBox),
        false,
        BindingMode.TwoWay
    );
    public bool IsChecked
    {
        get { return (bool)GetValue(IsCheckedProperty); }
        set
        {
            SetValue(IsCheckedProperty, value);
            OnPropertyChanged(nameof(IsChecked));
        }
    }
    #endregion

    public CustomCheckBox()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void CheckBox_Tapped(object sender, TappedEventArgs e)
    {
        IsChecked = !IsChecked;
    }
}
