using System.Collections.ObjectModel;
using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FieldsView;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView;

public partial class EmbedView : ContentView
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
        typeof(EmbedView),
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

    #region AuthorView

    #region AuthorIcon Binding
    public static readonly BindableProperty AuthorIconProperty = BindableProperty.Create(
        nameof(AuthorIcon),
        typeof(string),
        typeof(EmbedView),
        string.Empty,
        BindingMode.TwoWay
    );
    public string AuthorIcon
    {
        get { return (string)GetValue(AuthorIconProperty); }
        set
        {
            SetValue(AuthorIconProperty, value);
            OnPropertyChanged(nameof(AuthorIcon));
        }
    }

    #endregion

    #region AuthorName Binding

    public static readonly BindableProperty AuthorNameProperty = BindableProperty.Create(
        nameof(AuthorName),
        typeof(string),
        typeof(EmbedView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string AuthorName
    {
        get { return (string)GetValue(AuthorNameProperty); }
        set
        {
            SetValue(AuthorNameProperty, value);
            OnPropertyChanged(nameof(AuthorName));
        }
    }

    #endregion

    #region AuthorUrl Binding

    public static readonly BindableProperty AuthorUrlProperty = BindableProperty.Create(
        nameof(AuthorUrl),
        typeof(string),
        typeof(EmbedView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string AuthorUrl
    {
        get { return (string)GetValue(AuthorUrlProperty); }
        set
        {
            SetValue(AuthorUrlProperty, value);
            OnPropertyChanged(nameof(AuthorUrl));
        }
    }

    #endregion

    #endregion

    #region BodyView

    #region BodyTitle Binding
    public static readonly BindableProperty BodyTitleProperty = BindableProperty.Create(
        nameof(BodyTitle),
        typeof(string),
        typeof(EmbedView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string BodyTitle
    {
        get { return (string)GetValue(BodyTitleProperty); }
        set
        {
            SetValue(BodyTitleProperty, value);
            OnPropertyChanged(nameof(BodyTitle));
        }
    }
    #endregion

    #region BodyContent Binding
    public static readonly BindableProperty BodyContentProperty = BindableProperty.Create(
        nameof(BodyContent),
        typeof(string),
        typeof(EmbedView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string BodyContent
    {
        get { return (string)GetValue(BodyContentProperty); }
        set
        {
            SetValue(BodyContentProperty, value);
            OnPropertyChanged(nameof(BodyContent));
        }
    }
    #endregion

    #region BodyUrl Binding
    public static readonly BindableProperty BodyUrlProperty = BindableProperty.Create(
        nameof(BodyUrl),
        typeof(string),
        typeof(EmbedView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string BodyUrl
    {
        get { return (string)GetValue(BodyUrlProperty); }
        set
        {
            SetValue(BodyUrlProperty, value);
            OnPropertyChanged(nameof(BodyUrl));
        }
    }
    #endregion

    #region BodyColor Binding
    public static readonly BindableProperty BodyColorProperty = BindableProperty.Create(
        nameof(BodyColor),
        typeof(Color),
        typeof(EmbedView),
        defaultValue: new Color(
            Discord.Color.Default.R,
            Discord.Color.Default.G,
            Discord.Color.Default.B
        ),
        defaultBindingMode: BindingMode.TwoWay
    );
    public Color BodyColor
    {
        get { return (Color)GetValue(BodyColorProperty); }
        set
        {
            SetValue(BodyColorProperty, value);
            OnPropertyChanged(nameof(BodyColor));
        }
    }
    #endregion

    #endregion

    #region FieldsView

    #region Fields Binding
    public static readonly BindableProperty FieldsProperty = BindableProperty.Create(
        nameof(Fields),
        typeof(ObservableCollection<FieldView>),
        typeof(EmbedView),
        new ObservableCollection<FieldView>(),
        BindingMode.TwoWay
    );
    public ObservableCollection<FieldView> Fields
    {
        get { return (ObservableCollection<FieldView>)GetValue(FieldsProperty); }
        set
        {
            SetValue(FieldsProperty, value);
            OnPropertyChanged(nameof(Fields));
        }
    }
    #endregion

    #endregion

    #region ImagesView

    #region ImagesImageUrl Binding
    public static readonly BindableProperty ImagesImageUrlProperty = BindableProperty.Create(
        nameof(ImagesImageUrl),
        typeof(string),
        typeof(EmbedView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string ImagesImageUrl
    {
        get { return (string)GetValue(ImagesImageUrlProperty); }
        set
        {
            SetValue(ImagesImageUrlProperty, value);
            OnPropertyChanged(nameof(ImagesImageUrl));
        }
    }
    #endregion

    #region ImagesThumbnailUrl Binding
    public static readonly BindableProperty ImagesThumbnailUrlProperty = BindableProperty.Create(
        nameof(ImagesThumbnailUrl),
        typeof(string),
        typeof(EmbedView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string ImagesThumbnailUrl
    {
        get { return (string)GetValue(ImagesThumbnailUrlProperty); }
        set
        {
            SetValue(ImagesThumbnailUrlProperty, value);
            OnPropertyChanged(nameof(ImagesThumbnailUrl));
        }
    }
    #endregion

    #endregion

    #region FooterView

    #region FooterIcon Binding
    public static readonly BindableProperty FooterIconProperty = BindableProperty.Create(
        nameof(FooterIcon),
        typeof(string),
        typeof(EmbedView),
        string.Empty,
        BindingMode.TwoWay
    );
    public string FooterIcon
    {
        get { return (string)GetValue(FooterIconProperty); }
        set
        {
            SetValue(FooterIconProperty, value);
            OnPropertyChanged(nameof(FooterIcon));
        }
    }

    #endregion

    #region FooterTitle Binding
    public static readonly BindableProperty FooterTitleProperty = BindableProperty.Create(
        nameof(FooterTitle),
        typeof(string),
        typeof(EmbedView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );
    public string FooterTitle
    {
        get { return (string)GetValue(FooterTitleProperty); }
        set
        {
            SetValue(FooterTitleProperty, value);
            OnPropertyChanged(nameof(FooterTitle));
        }
    }
    #endregion

    #region FooterTimestamp Binding
    public static readonly BindableProperty FooterTimestampProperty = BindableProperty.Create(
        nameof(FooterTimestamp),
        typeof(bool),
        typeof(EmbedView),
        defaultValue: false,
        defaultBindingMode: BindingMode.TwoWay
    );
    public bool FooterTimestamp
    {
        get { return (bool)GetValue(FooterTimestampProperty); }
        set
        {
            SetValue(FooterTimestampProperty, value);
            OnPropertyChanged(nameof(FooterTimestamp));
        }
    }
    #endregion

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

    public EmbedView(
        int id,
        int order,
        string authorIcon,
        string authorName,
        string authorUrl,
        string bodyTitle,
        string bodyContent,
        string bodyUrl,
        Color bodyColor,
        ObservableCollection<FieldView> fields,
        string imagesImageUrl,
        string imagesThumbnailUrl,
        string footerIcon,
        string footerTitle,
        bool footerTimestamp,
        bool isEmpty = true
    )
    {
        ID = id;
        Order = order;
        AuthorIcon = authorIcon;
        AuthorName = authorName;
        AuthorUrl = authorUrl;
        BodyTitle = bodyTitle;
        BodyContent = bodyContent;
        BodyUrl = bodyUrl;
        BodyColor = bodyColor;
        Fields = fields;
        ImagesImageUrl = imagesImageUrl;
        ImagesThumbnailUrl = imagesThumbnailUrl;
        FooterIcon = footerIcon;
        FooterTitle = footerTitle;
        FooterTimestamp = footerTimestamp;
        IsEmpty = isEmpty;
        BindingContext = this;
    }

    public EmbedView()
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
