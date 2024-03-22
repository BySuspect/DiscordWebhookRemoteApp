using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Popups;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.SavedWebhooksView;

public partial class SavedWebhooksView : ContentView
{
    private ObservableCollection<SavedWebhookView> _savedWebhooks;

    public ObservableCollection<SavedWebhookView> SavedWebhooks
    {
        get { return _savedWebhooks ?? new ObservableCollection<SavedWebhookView>(); }
        set
        {
            if (value != _savedWebhooks)
            {
                _savedWebhooks = value;
                OnPropertyChanged(nameof(SavedWebhooks));
            }
        }
    }

    private SavedWebhookView SelectedWebhook;

    public SavedWebhooksView()
    {
        InitializeComponent();
        BindingContext = this;
        var list = new List<SavedWebhookView>()
        {
            new SavedWebhookView()
            {
                WebhookId = 1,
                Name = "test1",
                IsSelected = false,
                ImageSource = "https://i.imgur.com/niLjyNS.jpg"
            },
        };
        SavedWebhooks = list.ToObservableCollection();
    }

    private async void AddNewWebhookTapped(object sender, TappedEventArgs e)
    {
        var popup = new SavedWebhookAddOrEditPopup();
        var result = await Application.Current.MainPage.ShowPopupAsync(
            popup,
            CancellationToken.None
        );
        //var _list = SavedWebhooks.ToList();
        //_list.Add(
        //    new SavedWebhookView
        //    {
        //        WebhookId = _list.Last().WebhookId + 1,
        //        Name = $"test{_list.Last().WebhookId + 1}",
        //    }
        //);
        //SavedWebhooks = _list.ToObservableCollection();
    }

    private void OnSavedWebhookViewTapped(object sender, TappedEventArgs e)
    {
        if (((SavedWebhookView)sender).IsSelected)
            return;

        ((SavedWebhookView)sender).IsSelected = true;
        if (SelectedWebhook != null)
            SelectedWebhook.IsSelected = false;

        SelectedWebhook = ((SavedWebhookView)sender);
    }
}
