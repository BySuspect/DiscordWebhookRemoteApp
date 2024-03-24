using System.Windows.Input;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.SavedWebhooksView;

public partial class SavedWebhookView : ContentView
{
    #region WebhookId Binding
    public static readonly BindableProperty WebhookIdProperty = BindableProperty.Create(
        nameof(WebhookId),
        typeof(int),
        typeof(SavedWebhookView),
        defaultValue: 0,
        BindingMode.TwoWay
    );

    public int WebhookId
    {
        get { return (int)GetValue(WebhookIdProperty); }
        set { SetValue(WebhookIdProperty, value); }
    }
    #endregion

    #region Name Binding
    public static readonly BindableProperty NameProperty = BindableProperty.Create(
        nameof(Name),
        typeof(string),
        typeof(SavedWebhookView),
        defaultValue: "",
        BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((SavedWebhookView)bindable).UpdateNameLabel((string)newValue);
        }
    );
    public string Name
    {
        get { return (string)GetValue(NameProperty); }
        set { SetValue(NameProperty, value); }
    }

    private void UpdateNameLabel(string newValue)
    {
        lblName.Text = newValue;
    }
    #endregion

    #region ImageSource Binding
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
        nameof(ImageSource),
        typeof(string),
        typeof(SavedWebhookView),
        defaultValue: "discordlogo.png",
        BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((SavedWebhookView)bindable).UpdateImage((string)newValue);
        }
    );
    public string ImageSource
    {
        get { return (string)GetValue(ImageSourceProperty); }
        set
        {
            SetValue(ImageSourceProperty, value);
            OnPropertyChanged(nameof(ImageSource));
        }
    }

    private void UpdateImage(string newValue)
    {
        img.Source = newValue;
    }
    #endregion

    #region IsSelected Binding
    public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
        nameof(IsSelected),
        typeof(bool),
        typeof(SavedWebhookView),
        defaultValue: false,
        BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (newValue != oldValue)
                ((SavedWebhookView)bindable).UpdateIsSelected();
        }
    );
    public bool IsSelected
    {
        get { return (bool)GetValue(IsSelectedProperty); }
        set { SetValue(IsSelectedProperty, value); }
    }

    private void UpdateIsSelected()
    {
        //Console.WriteLine("------------------\n" + IsSelected + " " + WebhookId);
        if (IsSelected)
        {
            WebhookViewFrame.Scale = 0.9;
            WebhookViewFrame.Opacity = 0.8;
        }
        else
        {
            WebhookViewFrame.Scale = 1;
            WebhookViewFrame.Opacity = 1;
        }
    }
    #endregion

    #region WebhookUrl Binding
    public static readonly BindableProperty WebhookUrlProperty = BindableProperty.Create(
        nameof(WebhookUrl),
        typeof(string),
        typeof(SavedWebhookView),
        defaultValue: ""
    );
    public string WebhookUrl
    {
        get { return (string)GetValue(WebhookUrlProperty); }
        set
        {
            SetValue(WebhookUrlProperty, value);
            OnPropertyChanged(nameof(WebhookUrl));
        }
    }
    #endregion

    public ICommand LongPressCommand { get; set; }

    public SavedWebhookView()
    {
        InitializeComponent();
        LongPressCommand = new Command(() =>
        {
            OnLongPressed(null, null);
        });
    }

    private void WebhookSelect_Tapped(object sender, TappedEventArgs e)
    {
        OnWebhookSelectTapped(this, e);
    }

    public event EventHandler<TappedEventArgs> WebhookSelectTapped;

    protected virtual void OnWebhookSelectTapped(object sender, TappedEventArgs e)
    {
        EventHandler<TappedEventArgs> handler = WebhookSelectTapped;
        handler?.Invoke(sender, e);
    }

    public event EventHandler<EventArgs> LongPressed;

    protected virtual void OnLongPressed(object sender, EventArgs e)
    {
        EventHandler<EventArgs> handler = LongPressed;
        handler?.Invoke(this, EventArgs.Empty);
    }
}

public class SavedWebhookViewItems
{
    public required int WebhookId { get; set; }
    public required string Name { get; set; }
    public required string ImageSource { get; set; }
    public required string WebhookUrl { get; set; }
}
