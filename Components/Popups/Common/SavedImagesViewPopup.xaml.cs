using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using static DiscordWebhookRemoteApp.Components.Popups.Common.SavedImagesEditOrViewPopup;

namespace DiscordWebhookRemoteApp.Components.Popups.Common;

public partial class SavedImagesViewPopup : Popup
{
    private ObservableCollection<SavedImagesItems> imageList;
    public ObservableCollection<SavedImagesItems> ImageList
    {
        get { return imageList ?? new ObservableCollection<SavedImagesItems>(); }
        set
        {
            SavedImagesService.SavedImages = value.ToList();
            imageList = value;
            OnPropertyChanged(nameof(ImageList));
        }
    }

    public SavedImagesViewPopup()
    {
        InitializeComponent();
        ImageList = SavedImagesService.SavedImages.ToObservableCollection();

        BindingContext = this;
    }

    private void Dismiss_Tapped(object sender, TappedEventArgs e)
    {
        Close();
    }

    private async void AddNew_Tapped(object sender, TappedEventArgs e)
    {
        addNewBtn.IsEnabled = false;
        var res = await ApplicationService.ShowPopupAsync(new ImageEditAndViewPopup(string.Empty));

        if (res == null || string.IsNullOrWhiteSpace((string)res))
        {
            addNewBtn.IsEnabled = true;
            return;
        }

        var _list = ImageList.ToList();
        _list.Add(
            new SavedImagesItems
            {
                Id = (_list.Count > 0) ? _list.Last().Id + 1 : 1,
                ImageUrl = res.ToString()
            }
        );
        ;
        ImageList = _list.ToObservableCollection();
        addNewBtn.IsEnabled = true;
    }

    private async void ImageSelect_Tapped(object sender, TappedEventArgs e)
    {
        var selected = (Frame)sender;
        selected.IsEnabled = false;
        var _list = ImageList.ToList();

        var res = await ApplicationService.ShowPopupAsync(
            new SavedImagesEditOrViewPopup(
                _list.First(x => x.Id == int.Parse(selected.AutomationId)).ImageUrl
            )
        );
        if (res == null)
        {
            selected.IsEnabled = true;
            return;
        }

        var resultType = ((SavedImagesEditOrViewPopupResult)res).ResultType;

        if (resultType == SavedImagesEditOrViewPopupResultTypes.Delete)
        {
            _list.Remove(_list.First(x => x.Id == int.Parse(selected.AutomationId)));
            ImageList = _list.ToObservableCollection();
            selected.IsEnabled = true;
            return;
        }
        if (resultType == SavedImagesEditOrViewPopupResultTypes.Select)
        {
            Close(_list.First(x => x.Id == int.Parse(selected.AutomationId)).ImageUrl);
            selected.IsEnabled = true;
            return;
        }
        if (resultType == SavedImagesEditOrViewPopupResultTypes.Save)
        {
            _list.First(x => x.Id == int.Parse(selected.AutomationId)).ImageUrl = (
                (SavedImagesEditOrViewPopupResult)res
            ).ImageUrl;
            ImageList = _list.ToObservableCollection();
            selected.IsEnabled = true;
            return;
        }

        selected.IsEnabled = true;
    }
}
