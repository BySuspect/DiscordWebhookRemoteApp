using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;

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

    private void FieldEdit_Tapped(object sender, TappedEventArgs e)
    {
        var selected = (FieldView)sender;
        Console.WriteLine("Edit tapped " + selected.Order);
    }

    private async void FieldDelete_Tapped(object sender, TappedEventArgs e)
    {
        var selected = (FieldView)sender;
        selected.IsEnabled = false;

        var res = await Application.Current.MainPage.DisplayAlert(
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
        var _list = Fields.ToList();
        _list.Remove(_list.First(x => x.ID == selected.ID));
        Fields = _list.ToObservableCollection();
        ReOrderList();

        selected.IsEnabled = true;
    }

    private void AddNew_Tapped(object sender, TappedEventArgs e)
    {
        if (Fields.Count >= 25)
            return;
        addNewBtn.IsEnabled = false;
        ReOrderList();
        var _list = Fields.ToList();
        //before adding asks on popup for field content
        _list.Add(
            new FieldView(
                (_list.Count > 0) ? _list.Last().ID + 1 : 0,
                (_list.Count > 0) ? _list.Last().Order + 1 : 1,
                "",
                "",
                false
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
