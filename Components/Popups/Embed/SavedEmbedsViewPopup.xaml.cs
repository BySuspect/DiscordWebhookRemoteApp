using System.Collections.ObjectModel;
using System.ComponentModel.Design;

using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;

using DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView;
using DiscordWebhookRemoteApp.Components.Popups.Common;

namespace DiscordWebhookRemoteApp.Components.Popups.Embed;

public partial class SavedEmbedsViewPopup : Popup
{
    private ObservableCollection<SavedEmbedsItems> embedList;
    public ObservableCollection<SavedEmbedsItems> EmbedList
    {
        get { return embedList ?? new ObservableCollection<SavedEmbedsItems>(); }
        set
        {
            SavedEmbedsService.SavedEmbeds = value.ToList();
            embedList = value;
            OnPropertyChanged(nameof(EmbedList));
        }
    }

    private string type;

    public SavedEmbedsViewPopup(string type = null)
    {
        InitializeComponent();
        EmbedList = SavedEmbedsService.SavedEmbeds.ToObservableCollection();
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
        var resNew = await ApplicationService.ShowPopupAsync(new EmbedEditAndNewPopup());
        if (resNew is null || resNew is "delete")
        {
            addNewBtn.IsEnabled = true;
            return;
        }
        var newEmbed = (EmbedView)resNew;
        var _list = EmbedList.ToList();

        var resTitle = await ApplicationService.ShowPopupAsync(
            new CustomInputPopup("Embed Name?", "", "name", "Ok", "Cancel", 24, true, true)
        );
        if (resTitle is null)
            resTitle = "Embed";

        _list.Add(
            new SavedEmbedsItems
            {
                ID = (_list.Count > 0) ? _list.Last().ID + 1 : 0,
                Title = (string)resTitle,
                AuthorIcon = newEmbed.AuthorIcon,
                AuthorName = newEmbed.AuthorName,
                AuthorUrl = newEmbed.AuthorUrl,
                BodyTitle = newEmbed.BodyTitle,
                BodyContent = newEmbed.BodyContent,
                BodyUrl = newEmbed.BodyUrl,
                BodyColor = newEmbed.BodyColor,
                Fields = newEmbed
                    .Fields.Select(x => new SavedFieldsItems
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Value = x.Value,
                        InLine = x.InLine,
                        IsEmpty = x.IsEmpty
                    })
                    .ToList(),
                ImagesImageUrl = newEmbed.ImagesImageUrl,
                ImagesThumbnailUrl = newEmbed.ImagesThumbnailUrl,
                FooterIcon = newEmbed.FooterIcon,
                FooterTitle = newEmbed.FooterTitle,
                FooterTimestamp = newEmbed.FooterTimestamp,
                IsEmpty = newEmbed.IsEmpty
            }
        );
        EmbedList = _list.ToObservableCollection();
        addNewBtn.IsEnabled = true;
    }

    private async void Edit_Tapped(object sender, TappedEventArgs e)
    {
        var view = (Frame)sender;
        var selected = EmbedList.First(x => x.ID == int.Parse(view.AutomationId));

        var res = await ApplicationService.ShowPopupAsync(
            new EmbedEditAndNewPopup(
                selected.AuthorIcon,
                selected.AuthorName,
                selected.AuthorUrl,
                selected.BodyTitle,
                selected.BodyContent,
                selected.BodyUrl,
                selected.BodyColor,
                selected
                    .Fields.Select(
                        x => new Partials.Views.WebhookItemsView.EmbedView.FieldsView.FieldView(
                            x.ID,
                            -1,
                            x.Name,
                            x.Value,
                            x.InLine,
                            x.IsEmpty
                        )
                    )
                    .ToObservableCollection(),
                selected.ImagesImageUrl,
                selected.ImagesThumbnailUrl,
                selected.FooterIcon,
                selected.FooterTitle,
                selected.FooterTimestamp,
                true,
                true,
                type
            )
        );
        if (res is null)
        {
            addNewBtn.IsEnabled = true;
            return;
        }
        else if (res is "delete")
        {
            await DeleteEmbed(selected);
            addNewBtn.IsEnabled = true;
            return;
        }
        else if (res is "select")
        {
            await CloseAsync(
                new EmbedView(
                    selected.ID,
                    -1,
                    selected.AuthorIcon,
                    selected.AuthorName,
                    selected.AuthorUrl,
                    selected.BodyTitle,
                    selected.BodyContent,
                    selected.BodyUrl,
                    selected.BodyColor,
                    selected
                        .Fields.Select(
                            x => new Partials.Views.WebhookItemsView.EmbedView.FieldsView.FieldView(
                                x.ID,
                                -1,
                                x.Name,
                                x.Value,
                                x.InLine,
                                x.IsEmpty
                            )
                        )
                        .ToObservableCollection(),
                    selected.ImagesImageUrl,
                    selected.ImagesThumbnailUrl,
                    selected.FooterIcon,
                    selected.FooterTitle,
                    selected.FooterTimestamp,
                    selected.IsEmpty
                )
            );
            addNewBtn.IsEnabled = true;
            return;
        }
        var editedEmbed = (EmbedView)res;
        var _list = EmbedList.ToList();

        var resTitle = await ApplicationService.ShowPopupAsync(
            new CustomInputPopup(
                "Embed Name?",
                _list.First(x => x.ID == selected.ID).Title,
                "name",
                "Ok",
                "Cancel",
                24,
                true,
                true
            )
        );
        if (resTitle is null)
            resTitle = _list.First(x => x.ID == selected.ID).Title;

        _list.First(x => x.ID == selected.ID).Title = (string)resTitle;
        _list.First(x => x.ID == selected.ID).AuthorIcon = editedEmbed.AuthorIcon;
        _list.First(x => x.ID == selected.ID).AuthorName = editedEmbed.AuthorName;
        _list.First(x => x.ID == selected.ID).AuthorUrl = editedEmbed.AuthorUrl;
        _list.First(x => x.ID == selected.ID).BodyTitle = editedEmbed.BodyTitle;
        _list.First(x => x.ID == selected.ID).BodyContent = editedEmbed.BodyContent;
        _list.First(x => x.ID == selected.ID).BodyUrl = editedEmbed.BodyUrl;
        _list.First(x => x.ID == selected.ID).BodyColor = editedEmbed.BodyColor;
        _list.First(x => x.ID == selected.ID).Fields = editedEmbed
            .Fields.Select(f => new SavedFieldsItems
            {
                ID = f.ID,
                Name = f.Name,
                Value = f.Value,
                InLine = f.InLine,
                IsEmpty = f.IsEmpty
            })
            .ToList();
        _list.First(x => x.ID == selected.ID).ImagesImageUrl = editedEmbed.ImagesImageUrl;
        _list.First(x => x.ID == selected.ID).ImagesThumbnailUrl = editedEmbed.ImagesThumbnailUrl;
        _list.First(x => x.ID == selected.ID).FooterIcon = editedEmbed.FooterIcon;
        _list.First(x => x.ID == selected.ID).FooterTitle = editedEmbed.FooterTitle;
        _list.First(x => x.ID == selected.ID).FooterTimestamp = editedEmbed.FooterTimestamp;
        _list.First(x => x.ID == selected.ID).IsEmpty = editedEmbed.IsEmpty;

        EmbedList = _list.ToObservableCollection();
        addNewBtn.IsEnabled = true;
    }

    private Task DeleteEmbed(SavedEmbedsItems selected)
    {
        var _list = EmbedList.ToList();
        _list.Remove(_list.First(x => x.ID == selected.ID));
        EmbedList = _list.ToObservableCollection();
        return Task.CompletedTask;
    }
}
