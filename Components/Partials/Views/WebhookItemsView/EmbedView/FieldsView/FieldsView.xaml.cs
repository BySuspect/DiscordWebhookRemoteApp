using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using DiscordWebhookRemoteApp.Components.Popups.Embed;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView.FieldsView;

public partial class FieldsView : ContentView
{
    private ObservableCollection<FieldView> fields;

    public ObservableCollection<FieldView> Fields
    {
        get { return fields ?? new ObservableCollection<FieldView>(); }
        set
        {
            fields = value;
            FieldsCount = $"{value.Count}/25";
            OnPropertyChanged(nameof(Fields));
        }
    }
    private string fieldsCount;

    public string FieldsCount
    {
        get { return fieldsCount ?? "0/25"; }
        set
        {
            fieldsCount = value;
            OnPropertyChanged(nameof(FieldsCount));
        }
    }

    public FieldsView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void FieldEdit_Tapped(object sender, TappedEventArgs e)
    {
        var selected = (FieldView)sender;

        var res = await ApplicationService.ShowPopupAsync(
            new EmbedFieldsEditAndNewPopup(selected.Name, selected.Value, selected.InLine)
        );
        if (res == null)
        {
            addNewBtn.IsEnabled = true;
            return;
        }
        if (res == "delete")
        {
            await DeleteField(selected);
            addNewBtn.IsEnabled = true;
            return;
        }

        var editedield = (FieldView)res;
        var _list = Fields.ToList();
        _list.First(x => x.ID == selected.ID).Name = editedield.Name;
        _list.First(x => x.ID == selected.ID).Value = editedield.Value;
        _list.First(x => x.ID == selected.ID).InLine = editedield.InLine;
        Fields = _list.ToObservableCollection();
        addNewBtn.IsEnabled = true;
    }

    private async void FieldDelete_Tapped(object sender, TappedEventArgs e)
    {
        var selected = (FieldView)sender;
        selected.IsEnabled = false;

        var res = await ApplicationService.ShowCustomAlertAsync(
            "Warning!",
            $"Are you sure you want to delete Field #{selected.Order}?",
            "Yes",
            "No"
        );
        if (!res)
        {
            selected.IsEnabled = true;
            return;
        }
        await DeleteField(selected);
        selected.IsEnabled = true;
    }

    private Task DeleteField(FieldView selected)
    {
        var _list = Fields.ToList();
        _list.Remove(_list.First(x => x.ID == selected.ID));
        Fields = _list.ToObservableCollection();
        ReOrderList();
        return Task.CompletedTask;
    }

    private async void AddNew_Tapped(object sender, TappedEventArgs e)
    {
        if (Fields.Count >= 25)
            return;
        addNewBtn.IsEnabled = false;
        var res = await ApplicationService.ShowPopupAsync(new EmbedFieldsEditAndNewPopup());
        if (res == null || res == "delete")
        {
            addNewBtn.IsEnabled = true;
            return;
        }
        var newField = (FieldView)res;
        ReOrderList();
        var _list = Fields.ToList();
        _list.Add(
            new FieldView(
                (_list.Count > 0) ? _list.Last().ID + 1 : 0,
                (_list.Count > 0) ? _list.Last().Order + 1 : 1,
                newField.Name,
                newField.Value,
                newField.InLine,
                newField.IsEmpty
            )
        );
        Fields = _list.ToObservableCollection();
        addNewBtn.IsEnabled = true;
    }

    private void ReOrderList()
    {
        var _list = Fields.ToList();
        _list.OrderBy(x => x.ID);
        for (int i = 0; i < _list.Count; i++)
        {
            _list[i].Order = i + 1;
        }
        Fields = _list.ToObservableCollection();
    }
}
