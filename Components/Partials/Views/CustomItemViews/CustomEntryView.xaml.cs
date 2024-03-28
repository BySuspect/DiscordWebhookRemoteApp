namespace DiscordWebhookRemoteApp.Components.Partials.Views.CustomItemViews;

public partial class CustomEntryView : ContentView
{
    #region Text Binding
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(CustomEntryView),
        defaultValue: ""
    );
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set
        {
            SetValue(TextProperty, value);
            OnPropertyChanged(nameof(Text));
        }
    }
    #endregion

    #region Placeholder Binding
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(Placeholder),
        typeof(string),
        typeof(CustomEntryView),
        defaultValue: ""
    );
    public string Placeholder
    {
        get { return (string)GetValue(PlaceholderProperty); }
        set
        {
            SetValue(PlaceholderProperty, value);
            OnPropertyChanged(nameof(Placeholder));
        }
    }
    #endregion

    #region MaxLength Binding
    public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
        nameof(MaxLength),
        typeof(string),
        typeof(CustomEntryView),
        defaultValue: ""
    );
    public string MaxLength
    {
        get { return (string)GetValue(MaxLengthProperty); }
        set
        {
            SetValue(MaxLengthProperty, value);
            OnPropertyChanged(nameof(MaxLength));
        }
    }
    #endregion

    #region CornerRadius Binding
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(int),
        typeof(CustomEntryView),
        defaultValue: 15
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

    #region CornerRadius Binding
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Color),
        typeof(CustomEntryView),
        defaultValue: Colors.White
    );
    public Color BorderColor
    {
        get { return (Color)GetValue(BorderColorProperty); }
        set
        {
            SetValue(BorderColorProperty, value);
            OnPropertyChanged(nameof(BorderColor));
        }
    }
    #endregion

    #region ValidationType Binding
    private ValidationType validationType;
    public ValidationType ValidationType
    {
        get { return validationType; }
        set
        {
            if (validationType != value)
            {
                validationType = value;

                if (value == ValidationType.ColorHex)
                {
                    Input.Behaviors.Clear();
                    Input.Behaviors.Add(new InputBehaviors.ColorHexValidatorBehaviour());
                }
                else if (value == ValidationType.Url)
                {
                    Input.Behaviors.Clear();
                    Input.Behaviors.Add(new InputBehaviors.UrlValidatorBehaviour());
                }
                else if (value == ValidationType.ImageUrl)
                {
                    Input.Behaviors.Clear();
                    Input.Behaviors.Add(new InputBehaviors.ImageUrlValidatorBehaviour());
                }
                else if (value == ValidationType.WebhookUrl)
                {
                    Input.Behaviors.Clear();
                    Input.Behaviors.Add(new InputBehaviors.WebhookUrlValidatorBehaviour());
                }
                else
                {
                    Input.Behaviors.Clear();
                }
            }
        }
    }
    #endregion

    #region HasCharCounterText Binding
    public static readonly BindableProperty HasCharCounterTextProperty = BindableProperty.Create(
        nameof(HasCharCounterText),
        typeof(bool),
        typeof(CustomEntryView),
        defaultValue: true
    );
    public bool HasCharCounterText
    {
        get { return (bool)GetValue(HasCharCounterTextProperty); }
        set
        {
            SetValue(HasCharCounterTextProperty, value);
            OnPropertyChanged(nameof(HasCharCounterText));
        }
    }
    #endregion

    public CustomEntryView()
    {
        InitializeComponent();
        BindingContext = this;

        if (this.Text.Length > 0)
        {
            lblTitle.TextColor = AppThemeColors.TextColor;
            titleView.BorderColor = AppThemeColors.BorderColor;
            titleView.BackgroundColor = AppThemeColors.BackgroundColor;
            titleView.TranslationX = 15;
            titleView.TranslationY = -8;
        }
        else
        {
            lblTitle.TextColor = AppThemeColors.PlaceholderTextColor;
            titleView.BorderColor = Colors.Transparent;
            titleView.BackgroundColor = Colors.Transparent;
            titleView.TranslationX = 0;
            titleView.TranslationY = 13;
        }

        Input.Completed += (s, e) => OnTextComplated(s, e);
        Input.Unfocused += (s, e) => OnTextComplated(s, null);
    }

    private void titleTapped(object sender, EventArgs e)
    {
        Input.Focus();
    }

    private void Input_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.Length >= Input.MaxLength)
            lblInputLenght.TextColor = Colors.Red;
        else
            lblInputLenght.TextColor = AppThemeColors.TextColor;

        spCharacterCount.Text = e.NewTextValue.Length.ToString();

        if (e.NewTextValue.Length > 0)
        {
            titleView.CancelAnimations();
            lblTitle.TextColor = AppThemeColors.TextColor;
            titleView.BorderColor = AppThemeColors.BorderColor;
            titleView.BackgroundColor = AppThemeColors.BackgroundColor;
            titleView.TranslateTo(15, -9, 150);
        }
        else
        {
            titleView.CancelAnimations();
            lblTitle.TextColor = AppThemeColors.PlaceholderTextColor;
            titleView.BorderColor = Colors.Transparent;
            titleView.BackgroundColor = Colors.Transparent;
            titleView.TranslateTo(5, 13, 150);
        }
        OnTextChanged(sender, e);
    }

    public event EventHandler<TextChangedEventArgs> TextChanged;

    protected virtual void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        EventHandler<TextChangedEventArgs> handler = TextChanged;
        handler?.Invoke(sender, e);
    }

    public event EventHandler<EventArgs> TextComplated;

    private void OnTextComplated(object sender, EventArgs e)
    {
        EventHandler<EventArgs> handler = TextComplated;
        handler?.Invoke(sender, e);
    }
}

public enum ValidationType
{
    None,
    ColorHex,
    Url,
    ImageUrl,
    WebhookUrl,
}
