/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-maccatalyst)'
Before:
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.CustomGestureRecognizers;
using DiscordWebhookRemoteApp.Components.Popups;
using Microsoft.Maui.Controls;
After:
using System.Windows.Input;

using CommunityToolkit.Maui.Views;

using DiscordWebhookRemoteApp.Components.Partials.CustomGestureRecognizers;
using DiscordWebhookRemoteApp.Components.Popups;

using Microsoft.Maui.Controls;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-ios)'
Before:
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.CustomGestureRecognizers;
using DiscordWebhookRemoteApp.Components.Popups;
using Microsoft.Maui.Controls;
After:
using System.Windows.Input;

using CommunityToolkit.Maui.Views;

using DiscordWebhookRemoteApp.Components.Partials.CustomGestureRecognizers;
using DiscordWebhookRemoteApp.Components.Popups;

using Microsoft.Maui.Controls;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-windows10.0.19041.0)'
Before:
using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Partials.CustomGestureRecognizers;
using DiscordWebhookRemoteApp.Components.Popups;
using Microsoft.Maui.Controls;
After:
using System.Windows.Input;

using CommunityToolkit.Maui.Views;

using DiscordWebhookRemoteApp.Components.Partials.CustomGestureRecognizers;
using DiscordWebhookRemoteApp.Components.Popups;

using Microsoft.Maui.Controls;
*/
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
        defaultValue: string.Empty,
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

            WebhookViewMainGrid.WidthRequest = 120;
            buttonsLayout.TranslateTo(40, 0, 100, Easing.CubicIn);
        }
        else
        {
            WebhookViewFrame.Scale = 1;
            WebhookViewFrame.Opacity = 1;

            buttonsLayout.TranslateTo(0, 0, 100, Easing.CubicOut);
            WebhookViewMainGrid.WidthRequest = 75;
        }
    }
    #endregion

    #region WebhookUrl Binding
    public static readonly BindableProperty WebhookUrlProperty = BindableProperty.Create(
        nameof(WebhookUrl),
        typeof(string),
        typeof(SavedWebhookView),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
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

    public SavedWebhookView()
    {
        InitializeComponent();
    }

    private void WebhookSelect_Tapped(object sender, TappedEventArgs e)
    {
        OnWebhookSelected(this, e);
    }

    private void Edit_Tapped(object sender, TappedEventArgs e)
    {
        OnSavedWebhookPropertyChanged(
            EditBtnFrame,
            new SavedWebhookPropertyChangedEventArgs(this, this)
        );
    }

    private async void Delete_Tapped(object sender, TappedEventArgs e)
    {
        DeleteBtnFrame.IsEnabled = false;
        var res = await ApplicationService.ShowCustomAlertAsync(
            "Warning!",
            $"Are you sure about to delete {Name}?",
            "Yes",
            "No"
        );
        if (res)
            OnSavedWebhookPropertyChanged(
                DeleteBtnFrame,
                new SavedWebhookPropertyChangedEventArgs(this, null)
            );

        DeleteBtnFrame.IsEnabled = true;
    }

    #region Events

    #region OnWebhookSelected
    public event EventHandler<TappedEventArgs> WebhookSelected;

    protected virtual void OnWebhookSelected(object sender, TappedEventArgs e)
    {
        EventHandler<TappedEventArgs> handler = WebhookSelected;
        handler?.Invoke(sender, e);
    }
    #endregion

    #region SavedWebhookPropertyChanged
    public event EventHandler<SavedWebhookPropertyChangedEventArgs> SavedWebhookPropertyChanged;

    protected virtual void OnSavedWebhookPropertyChanged(
        object sender,
        SavedWebhookPropertyChangedEventArgs e
    )
    {
        EventHandler<SavedWebhookPropertyChangedEventArgs> handler = SavedWebhookPropertyChanged;
        handler?.Invoke(sender, e);
    }
    #endregion

    #endregion
}

public class SavedWebhookViewItems
{
    public int WebhookId { get; set; }
    public required string Name { get; set; }
    public required string ImageSource { get; set; }
    public required string WebhookUrl { get; set; }
}

public class SavedWebhookPropertyChangedEventArgs : EventArgs
{
    public SavedWebhookPropertyChangedEventArgs(SavedWebhookView oldItem, SavedWebhookView? neItem)
    {
        OldItem = oldItem;
        NewItem = neItem;
    }

    public SavedWebhookView OldItem { get; private set; }
    public SavedWebhookView? NewItem { get; private set; }
}
