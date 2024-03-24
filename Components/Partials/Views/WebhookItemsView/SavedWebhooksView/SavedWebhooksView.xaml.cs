using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Popups;
using DiscordWebhookRemoteApp.Services;

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
            SavedWebhooksService.SavedWebhookList = _list;
        }
    }

    public SavedWebhookView selectedWebhook;

    public SavedWebhooksView()
    {
        InitializeComponent();
        BindingContext = this;

        PropertyChanged += (sender, e) =>
        {
            Console.WriteLine(e.PropertyName);
        };
        var _list = new List<SavedWebhookView>();
        foreach (var item in SavedWebhooksService.SavedWebhookList)
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

        //var list = new List<SavedWebhookView>()
        //{
        //    new SavedWebhookView()
        //    {
        //        WebhookId = 1,
        //        Name = "test1",
        //        IsSelected = false,
        //        ImageSource = "https://i.imgur.com/niLjyNS.jpg",
        //        WebhookUrl = "test",
        //    },
        //};
        //SavedWebhooks = list.ToObservableCollection();
    }

    private void DeleteWebhook_Clicked(object sender, EventArgs e)
    {
        if (selectedWebhook == null)
            return;

        var _list = SavedWebhooks.ToList();
        _list.Remove(_list.Where(x => x.WebhookId == selectedWebhook.WebhookId).First());
        SavedWebhooks = _list.ToObservableCollection();
    }

    private async void AddNewWebhookTapped(object sender, TappedEventArgs e)
    {
        try
        {
            //var popup = new SavedWebhookAddOrEditPopup();
            //var result = await Application.Current.MainPage.ShowPopupAsync(
            //    popup,
            //    CancellationToken.None
            //);

            var res = await Application.Current.MainPage.DisplayPromptAsync(
                "Add New Webhook",
                "Enter the url of the webhook",
                "Add",
                "Cancel"
            );

            if (res != null)
            {
                var _list = SavedWebhooks.ToList();
                _list.Add(
                    new SavedWebhookView
                    {
                        WebhookId = (_list.Count > 0) ? _list.Last().WebhookId + 1 : 1,
                        ImageSource = "discordlogo.png",
                        Name = $"Webhook{((_list.Count > 0) ? _list.Last().WebhookId + 1 : 1)}",
                        WebhookUrl = res,
                    }
                );
                SavedWebhooks = _list.ToObservableCollection();
            }
        }
        catch (Exception ex)
        {
            _ = Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "ok");
        }
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
}
