using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView;

public partial class FileSendView : ContentView
{
    #region SelectedFiles Binding
    private ObservableCollection<FileSendViewItems> selectedFiles;

    public ObservableCollection<FileSendViewItems> SelectedFiles
    {
        get { return selectedFiles ?? new ObservableCollection<FileSendViewItems>(); }
        set
        {
            selectedFiles = value;
            OnPropertyChanged(nameof(SelectedFiles));
        }
    }
    #endregion

    #region TotalFileCount Binding
    public int totalFileCount;
    public int TotalFileCount
    {
        get { return totalFileCount; }
        set
        {
            totalFileCount = value;
            OnPropertyChanged(nameof(TotalFileCount));
        }
    }
    #endregion

    #region TotalFileSize Binding
    private string totalFileSizeText;

    public string TotalFileSizeText
    {
        get { return totalFileSizeText ?? "0"; }
        set
        {
            totalFileSizeText = value;
            OnPropertyChanged(nameof(TotalFileSizeText));
        }
    }

    #endregion

    public FileSendView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void Select_Clicked(object sender, EventArgs e)
    {
        btnSelect.IsEnabled = false;
        ApplicationService.ShowLoadingView();
        try
        {
            var _list = new List<FileSendViewItems>();

            var result = await FilePicker.PickMultipleAsync(
                new PickOptions { PickerTitle = "Select Files", }
            );

            if (result == null)
            {
                btnSelect.IsEnabled = true;
                ApplicationService.HideLoadingView();
                return;
            }

            if (result.Count() > 10)
            {
                ApplicationService.ShowCustomAlert("Warning!", "Please select max 10 files!", "OK");
                ApplicationService.HideLoadingView();
                btnSelect.IsEnabled = true;
                return;
            }

            long TotalFileSize = 0;

            foreach (var file in result)
            {
                var fileinfo = new FileInfo(file.FullPath);
                TotalFileSize += fileinfo.Length;

                _list.Add(
                    new FileSendViewItems
                    {
                        Id = (_list.Count > 0) ? _list.Last().Id + 1 : 1,
                        FileName = fileinfo.Name,
                        Extension = fileinfo.Extension,
                        Path = file.FullPath,
                        FileSize = fileinfo.Length,
                        FileSizeText = GetSizeString(fileinfo.Length),
                    }
                );
            }
            if (TotalFileSize > 26214400)
            {
                ApplicationService.ShowCustomAlert(
                    "Warning!",
                    "Please select total less than 25mb files!",
                    "OK"
                );
                btnSelect.IsEnabled = true;
                ApplicationService.HideLoadingView();
                return;
            }
            SelectedFiles = _list.ToObservableCollection();
            TotalFileSizeText = GetSizeString(TotalFileSize);
            TotalFileCount = selectedFiles.Count;
        }
        catch (Exception ex)
        {
            ApplicationService.ShowCustomAlert("File Select Error!", ex.Message, "OK");
        }

        btnSelect.IsEnabled = true;
        ApplicationService.HideLoadingView();
    }

    public string GetSizeString(long bytes)
    {
        const int KB = 1024;
        const int MB = KB * 1024;

        if (bytes < KB)
        {
            return $"{bytes} bytes";
        }
        else if (bytes < MB)
        {
            double kbSize = (double)bytes / KB;
            return $"{kbSize:N2} KB";
        }
        else
        {
            double mbSize = (double)bytes / MB;
            return $"{mbSize:N2} MB";
        }
    }

    private async void Delete_Tapped(object sender, TappedEventArgs e)
    {
        var selected = (Frame)sender;
        selected.IsEnabled = false;
        var _list = SelectedFiles.ToList();
        var selectedFile = _list.First(x => x.Id == Convert.ToInt32(selected.AutomationId));
        var res = await ApplicationService.ShowCustomAlertAsync(
            "Warning!",
            $"Are you sure you want to delete {selectedFile.FileName}?",
            "Yes",
            "No"
        );
        if (!res)
        {
            selected.IsEnabled = true;
            return;
        }
        _list.Remove(_list.First(x => x.Id == selectedFile.Id));
        SelectedFiles = _list.ToObservableCollection();
        refreshList();
        selected.IsEnabled = true;
    }

    private void refreshList()
    {
        var _list = selectedFiles.ToList();
        long totalFileSize = 0;
        foreach (var item in _list)
        {
            totalFileSize += item.FileSize;
        }

        TotalFileCount = _list.Count;
        TotalFileSizeText = GetSizeString(totalFileSize);
    }
}

public class FileSendViewItems
{
    public required int Id { get; set; }
    public required string FileName { get; set; }
    public required string Extension { get; set; }
    public required string Path { get; set; }
    public long FileSize { get; set; }
    public required string FileSizeText { get; set; }
}
