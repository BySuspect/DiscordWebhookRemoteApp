namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FieldsView;

public partial class FieldView : ContentView
{
    #region ID Binding
    public static readonly BindableProperty IDProperty = BindableProperty.Create(
        nameof(ID),
        typeof(int),
        typeof(FieldView),
        defaultValue: -1,
        defaultBindingMode: BindingMode.TwoWay
    );
    public int ID
    {
        get { return (int)GetValue(IDProperty); }
        set
        {
            SetValue(IDProperty, value);
            OnPropertyChanged(nameof(ID));
        }
    }
    #endregion

    #region Order Binding
    public static readonly BindableProperty OrderProperty = BindableProperty.Create(
        nameof(Order),
        typeof(int),
        typeof(FieldView),
        defaultValue: -1,
        defaultBindingMode: BindingMode.TwoWay
    );
    public int Order
    {
        get { return (int)GetValue(OrderProperty); }
        set
        {
            SetValue(OrderProperty, value);
            OnPropertyChanged(nameof(Order));
        }
    }
    #endregion

    #region Name Binding
    public static readonly BindableProperty NameProperty = BindableProperty.Create(
        nameof(Name),
        typeof(string),
        typeof(FieldView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string Name
    {
        get { return (string)GetValue(NameProperty); }
        set
        {
            SetValue(NameProperty, value);
            OnPropertyChanged(nameof(Name));
        }
    }
    #endregion

    #region Value Binding
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
        nameof(Value),
        typeof(string),
        typeof(FieldView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string Value
    {
        get { return (string)GetValue(ValueProperty); }
        set
        {
            SetValue(ValueProperty, value);
            OnPropertyChanged(nameof(Value));
        }
    }
    #endregion

    #region InLine Binding
    public static readonly BindableProperty InLineProperty = BindableProperty.Create(
        nameof(InLine),
        typeof(bool),
        typeof(FieldView),
        defaultValue: false,
        defaultBindingMode: BindingMode.TwoWay
    );
    public bool InLine
    {
        get { return (bool)GetValue(InLineProperty); }
        set
        {
            SetValue(InLineProperty, value);
            OnPropertyChanged(nameof(InLine));
        }
    }
    #endregion

    #region IsEmpty Binding
    public static readonly BindableProperty IsEmptyProperty = BindableProperty.Create(
        nameof(IsEmpty),
        typeof(bool),
        typeof(FieldView),
        defaultValue: true,
        defaultBindingMode: BindingMode.TwoWay
    );
    public bool IsEmpty
    {
        get { return (bool)GetValue(IsEmptyProperty); }
        set
        {
            SetValue(IsEmptyProperty, value);
            OnPropertyChanged(nameof(IsEmpty));
        }
    }
    #endregion

    public FieldView(int id, int order, string name, string value, bool inLine, bool isEmpty = true)
    {
        ID = id;
        Order = order;
        Name = name;
        Value = value;
        InLine = inLine;
        IsEmpty = isEmpty;
    }

    public FieldView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void Edit_Tapped(object sender, TappedEventArgs e)
    {
        OnEditTapped(e);
    }

    private void Delete_Tapped(object sender, TappedEventArgs e)
    {
        OnDeleteTapped(e);
    }

    #region Events

    #region Edit

    public event EventHandler<TappedEventArgs> EditTapped;

    protected virtual void OnEditTapped(TappedEventArgs e)
    {
        EventHandler<TappedEventArgs> handler = EditTapped;
        handler?.Invoke(this, e);
    }
    #endregion

    #region Delete

    public event EventHandler<TappedEventArgs> DeleteTapped;

    protected virtual void OnDeleteTapped(TappedEventArgs e)
    {
        EventHandler<TappedEventArgs> handler = DeleteTapped;
        handler?.Invoke(this, e);
    }
    #endregion

    #endregion
}
