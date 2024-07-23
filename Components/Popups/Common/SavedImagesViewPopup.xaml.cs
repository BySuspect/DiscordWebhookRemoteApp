using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;

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

    private readonly string? type = null;

    public SavedImagesViewPopup(string? type = null)
    {
        InitializeComponent();
        ImageList = SavedImagesService.SavedImages.ToObservableCollection();
        BindingContext = this;
        this.type = type;
    }

    private void Dismiss_Tapped(object sender, TappedEventArgs e)
    {
        Close();
    }

    private async void AddNew_Tapped(object sender, TappedEventArgs e)
    {
        addNewBtn.IsEnabled = false;
        var res = await ApplicationService.ShowPopupAsync(
            new ImageEditAndViewPopup(string.Empty, "New")
        );

        if (res is null || string.IsNullOrWhiteSpace((string)res))
        {
            addNewBtn.IsEnabled = true;
            return;
        }

        var _list = ImageList.ToList();
        _list.Add(
            new SavedImagesItems
            {
                Id = (_list.Count > 0) ? _list.Last().Id + 1 : 1,
                ImageUrl = (string)res
            }
        );
        ImageList = _list.ToObservableCollection();
        addNewBtn.IsEnabled = true;
    }

    private async void ImageSelect_Tapped(object sender, TappedEventArgs e)
    {
        var selected = (Frame)sender;
        selected.IsEnabled = false;
        var _list = ImageList.ToList();

        var res = await ApplicationService.ShowPopupAsync(
            new ImageEditAndViewPopup(
                _list.First(x => x.Id == int.Parse(selected.AutomationId)).ImageUrl,
                type ?? "Edit"
            )
        );
        if (res is null)
        {
            selected.IsEnabled = true;
            return;
        }

        var resultType = ((ImagesEditOrViewPopupResult)res).ResultType;

        if (resultType is ImagesEditOrViewPopupResultTypes.Delete)
        {
            if (_list.Count == 1)
            {
                var resDelete = await ApplicationService.ShowCustomAlertAsync(
                    "Warning!",
                    "You are deleting the last image, For now the application is instantly crashing after deleting all the images. I cant figure out why it is crashing, so I added this alert. Are you sure about that?",
                    "Yes",
                    "No"
                );
                if (!resDelete)
                {
                    selected.IsEnabled = true;
                    return;
                }
            }
            _list.Remove(_list.First(x => x.Id == int.Parse(selected.AutomationId)));
            ImageList = _list.ToObservableCollection();
            return;
        }
        if (resultType is ImagesEditOrViewPopupResultTypes.Select)
        {
            Close(_list.First(x => x.Id == int.Parse(selected.AutomationId)).ImageUrl);
            selected.IsEnabled = true;
            return;
        }
        if (resultType is ImagesEditOrViewPopupResultTypes.Save)
        {
            _list.First(x => x.Id == int.Parse(selected.AutomationId)).ImageUrl = (
                (ImagesEditOrViewPopupResult)res
            ).ImageUrl;
            ImageList = _list.ToObservableCollection();
            selected.IsEnabled = true;
            return;
        }

        selected.IsEnabled = true;
    }
}
