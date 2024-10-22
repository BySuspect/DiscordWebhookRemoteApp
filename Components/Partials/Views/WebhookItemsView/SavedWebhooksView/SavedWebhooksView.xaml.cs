using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Popups.Webhook;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.SavedWebhooksView;

public partial class SavedWebhooksView : ContentView
{
    private ObservableCollection<SavedWebhookView> savedWebhooks;

    public ObservableCollection<SavedWebhookView> SavedWebhooks
    {
        get { return savedWebhooks; }
        set
        {
            savedWebhooks = value;
            OnPropertyChanged(nameof(SavedWebhooks));

            var _list = new List<SavedWebhookViewItems>();
            foreach (var item in value)
            {
                _list.Add(
                    new SavedWebhookViewItems
                    {
                        WebhookId = item.WebhookId,
                        ImageSource = item.ImageSource,
                        Name = item.Name,
                        WebhookUrl = item.WebhookUrl,
                    }
                );
            }
            WebhookService.SavedWebhookList = _list;
        }
    }

    public SavedWebhookView selectedWebhook;

    SavedWebhookAddOrEditPopup savedWebhookAddOrEditPopup = new SavedWebhookAddOrEditPopup();

    public SavedWebhooksView()
    {
        InitializeComponent();
        BindingContext = this;

        PropertyChanged += (sender, e) =>
        {
            Console.WriteLine(e.PropertyName);
        };

        RefreshList();
    }

    public Task RefreshList()
    {
        var _list = new List<SavedWebhookView>();
        foreach (var item in WebhookService.SavedWebhookList)
        {
            _list.Add(
                new SavedWebhookView
                {
                    WebhookId = item.WebhookId,
                    ImageSource = item.ImageSource,
                    Name = item.Name,
                    WebhookUrl = item.WebhookUrl,
                    IsSelected = false,
                }
            );
        }
        SavedWebhooks = _list.ToObservableCollection();
        return Task.CompletedTask;
    }

    private async void AddNewWebhookTapped(object sender, TappedEventArgs e)
    {
        try
        {
            addNewBtn.IsEnabled = false;
            savedWebhookAddOrEditPopup = new SavedWebhookAddOrEditPopup();
            savedWebhookAddOrEditPopup.NewMode();
            var res =
                await ApplicationService.ShowPopupAsync(savedWebhookAddOrEditPopup)
                as SavedWebhookViewItems;

            if (res != null)
            {
                var _list = SavedWebhooks.ToList();
                _list.Add(
                    new SavedWebhookView
                    {
                        WebhookId = (_list.Count > 0) ? _list.Last().WebhookId + 1 : 1,
                        ImageSource = res.ImageSource,
                        Name = res.Name,
                        WebhookUrl = res.WebhookUrl,
                    }
                );
                SavedWebhooks = _list.ToObservableCollection();
            }
        }
        catch (Exception ex)
        {
            ApplicationService.ShowCustomAlert("Error!", ex.Message, "ok");
        }

        addNewBtn.IsEnabled = true;
    }

    private void OnSavedWebhookViewTapped(object sender, TappedEventArgs e)
    {
        if (((SavedWebhookView)sender).IsSelected)
            return;

        ((SavedWebhookView)sender).IsSelected = true;
        if (selectedWebhook != null)
            selectedWebhook.IsSelected = false;

        selectedWebhook = ((SavedWebhookView)sender);
        Console.WriteLine(selectedWebhook.WebhookUrl);
    }

    private async void SavedWebhookPropertyChanged(
        object sender,
        SavedWebhookPropertyChangedEventArgs e
    )
    {
        ApplicationService.ShowLoadingView();
        if (e.NewItem is null)
        {
            //deleting
            var _list = SavedWebhooks.ToList();
            _list.Remove(_list.Where(x => x.WebhookId == e.OldItem.WebhookId).First());
            SavedWebhooks = _list.ToObservableCollection();
        }
        else
        {
            //editing
            var btn = (Frame)sender;
            btn.IsEnabled = false;
            savedWebhookAddOrEditPopup = new SavedWebhookAddOrEditPopup();
            savedWebhookAddOrEditPopup.EditMode(
                new SavedWebhookViewItems
                {
                    WebhookUrl = e.OldItem.WebhookUrl,
                    ImageSource = e.OldItem.ImageSource,
                    Name = e.OldItem.Name
                }
            );
            var res =
                await PopupExtensions.ShowPopupAsync(
                    Application.Current.MainPage,
                    savedWebhookAddOrEditPopup
                ) as SavedWebhookViewItems;

            if (res != null)
            {
                var _list = SavedWebhooks.ToList();
                var item = _list.Where(x => x.WebhookId == e.OldItem.WebhookId).First();
                item.ImageSource = res.ImageSource;
                item.Name = res.Name;
                item.WebhookUrl = res.WebhookUrl;
                _list.Remove(_list.Where(x => x.WebhookId == e.OldItem.WebhookId).First());
                _list.Add(item);
                _list = _list.OrderBy(x => x.WebhookId).ToList();
                SavedWebhooks = _list.ToObservableCollection();
            }
            btn.IsEnabled = true;
        }

        ApplicationService.HideLoadingView();
    }
}
