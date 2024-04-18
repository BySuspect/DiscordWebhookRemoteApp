using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using Discord;
using DiscordWebhookRemoteApp.Components.Popups.Embed;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView;

public partial class EmbedsView : ContentView
{
    private ObservableCollection<EmbedView> embeds;

    public ObservableCollection<EmbedView> Embeds
    {
        get { return embeds ?? new ObservableCollection<EmbedView>(); }
        set
        {
            embeds = value;
            EmbedsCount = $"{value.Count}/10";
            OnPropertyChanged(nameof(Embeds));
        }
    }
    private string embedsCount;

    public string EmbedsCount
    {
        get { return embedsCount ?? "0/10"; }
        set
        {
            embedsCount = value;
            OnPropertyChanged(nameof(EmbedsCount));
        }
    }

    public EmbedsView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public Task ClearEmbeds()
    {
        Embeds = new ObservableCollection<EmbedView>();
        return Task.CompletedTask;
    }

    private async void EmbedEdit_Tapped(object sender, TappedEventArgs e)
    {
        var selected = (EmbedView)sender;

        var res = await ApplicationService.ShowPopupAsync(
            new EmbedEditAndNewPopup(
                selected.AuthorIcon,
                selected.AuthorName,
                selected.AuthorUrl,
                selected.BodyTitle,
                selected.BodyContent,
                selected.BodyUrl,
                selected.BodyColor,
                selected.Fields,
                selected.ImagesImageUrl,
                selected.ImagesThumbnailUrl,
                selected.FooterIcon,
                selected.FooterTitle,
                selected.FooterTimestamp
            )
        );
        if (res is null)
        {
            addNewBtn.IsEnabled = true;
            return;
        }
        if (res is "delete")
        {
            await DeleteEmbed(selected);
            addNewBtn.IsEnabled = true;
            return;
        }
        var editedEmbed = (EmbedView)res;
        var _list = Embeds.ToList();

        _list.First(x => x.ID == selected.ID).AuthorIcon = editedEmbed.AuthorIcon;
        _list.First(x => x.ID == selected.ID).AuthorName = editedEmbed.AuthorName;
        _list.First(x => x.ID == selected.ID).AuthorUrl = editedEmbed.AuthorUrl;
        _list.First(x => x.ID == selected.ID).BodyTitle = editedEmbed.BodyTitle;
        _list.First(x => x.ID == selected.ID).BodyContent = editedEmbed.BodyContent;
        _list.First(x => x.ID == selected.ID).BodyUrl = editedEmbed.BodyUrl;
        _list.First(x => x.ID == selected.ID).BodyColor = editedEmbed.BodyColor;
        _list.First(x => x.ID == selected.ID).Fields = editedEmbed.Fields;
        _list.First(x => x.ID == selected.ID).ImagesImageUrl = editedEmbed.ImagesImageUrl;
        _list.First(x => x.ID == selected.ID).ImagesThumbnailUrl = editedEmbed.ImagesThumbnailUrl;
        _list.First(x => x.ID == selected.ID).FooterIcon = editedEmbed.FooterIcon;
        _list.First(x => x.ID == selected.ID).FooterTitle = editedEmbed.FooterTitle;
        _list.First(x => x.ID == selected.ID).FooterTimestamp = editedEmbed.FooterTimestamp;
        _list.First(x => x.ID == selected.ID).IsEmpty = editedEmbed.IsEmpty;

        Embeds = _list.ToObservableCollection();

        addNewBtn.IsEnabled = true;
    }

    private async void EmbedDelete_Tapped(object sender, TappedEventArgs e)
    {
        var selected = (EmbedView)sender;
        selected.IsEnabled = false;

        var res = await ApplicationService.ShowCustomAlertAsync(
            "Warning!",
            $"Are you sure you want to delete Embed #{selected.Order}?",
            "Yes",
            "No"
        );
        if (!res)
        {
            selected.IsEnabled = true;
            return;
        }
        await DeleteEmbed(selected);
        selected.IsEnabled = true;
    }

    private Task DeleteEmbed(EmbedView selected)
    {
        var _list = Embeds.ToList();
        _list.Remove(_list.First(x => x.ID == selected.ID));
        Embeds = _list.ToObservableCollection();
        ReOrderList();
        return Task.CompletedTask;
    }

    private async void AddNew_Tapped(object sender, TappedEventArgs e)
    {
        if (Embeds.Count >= 10)
            return;
        addNewBtn.IsEnabled = false;
        var resNewOrSelect = await ApplicationService.ShowPopupAsync(new EmbedNewAndSelectPopup());
        if (resNewOrSelect is "Load")
        {
            var resSelected = await ApplicationService.ShowPopupAsync(new SavedEmbedsViewPopup());
            if (resSelected is not null)
            {
                var newEmbed = (EmbedView)resSelected;
                ReOrderList();

                var _list = Embeds.ToList();
                _list.Add(
                    new EmbedView(
                        (_list.Count > 0) ? _list.Last().ID + 1 : 0,
                        (_list.Count > 0) ? _list.Last().Order + 1 : 1,
                        newEmbed.AuthorIcon,
                        newEmbed.AuthorName,
                        newEmbed.AuthorUrl,
                        newEmbed.BodyTitle,
                        newEmbed.BodyContent,
                        newEmbed.BodyUrl,
                        newEmbed.BodyColor,
                        newEmbed.Fields,
                        newEmbed.ImagesImageUrl,
                        newEmbed.ImagesThumbnailUrl,
                        newEmbed.FooterIcon,
                        newEmbed.FooterTitle,
                        newEmbed.FooterTimestamp,
                        newEmbed.IsEmpty
                    )
                );
                Embeds = _list.ToObservableCollection();
            }
        }
        else if (resNewOrSelect is "New")
        {
            var resNew = await ApplicationService.ShowPopupAsync(new EmbedEditAndNewPopup());
            if (resNew is null || resNew is "delete")
            {
                addNewBtn.IsEnabled = true;
                return;
            }
            var newEmbed = (EmbedView)resNew;
            ReOrderList();

            var _list = Embeds.ToList();
            _list.Add(
                new EmbedView(
                    (_list.Count > 0) ? _list.Last().ID + 1 : 0,
                    (_list.Count > 0) ? _list.Last().Order + 1 : 1,
                    newEmbed.AuthorIcon,
                    newEmbed.AuthorName,
                    newEmbed.AuthorUrl,
                    newEmbed.BodyTitle,
                    newEmbed.BodyContent,
                    newEmbed.BodyUrl,
                    newEmbed.BodyColor,
                    newEmbed.Fields,
                    newEmbed.ImagesImageUrl,
                    newEmbed.ImagesThumbnailUrl,
                    newEmbed.FooterIcon,
                    newEmbed.FooterTitle,
                    newEmbed.FooterTimestamp,
                    newEmbed.IsEmpty
                )
            );
            Embeds = _list.ToObservableCollection();
        }
        addNewBtn.IsEnabled = true;
    }

    private void ReOrderList()
    {
        var _list = Embeds.ToList();
        _list.OrderBy(x => x.ID);
        for (int i = 0; i < _list.Count; i++)
        {
            _list[i].Order = i + 1;
        }
        Embeds = _list.ToObservableCollection();
    }
}

/*
//TODO: ObservableCollection ile ve herbir embed viewi harici popup yada sayfada gösterilicek
    public static BindableProperty EmbedsProperty = BindableProperty.Create(
        nameof(Embeds),
        typeof(IEnumerable<Discord.Embed>),
        typeof(EmbedsView),
        default(IEnumerable<Discord.Embed>),
        BindingMode.TwoWay,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var view = (EmbedsView)bindable;
            //view.lvEmbeds.ItemsSource = (IEnumerable<Discord.Embed>)newValue;
        },
        validateValue: (bindable, value) =>
        {
            return value != null;
        },
        defaultValueCreator: (bindable) =>
        {
            return new List<Discord.Embed>();
        },
        coerceValue: (bindable, value) =>
        {
            return value;
        },
        propertyChanging: (bindable, oldValue, newValue) =>
        {
            var view = (EmbedsView)bindable;
            //view.lvEmbeds.ItemsSource = (IEnumerable<Discord.Embed>)newValue;
        }
    );

 
 //var embed = new Discord.EmbedBuilder
        //{
        //    Author = new Discord.EmbedAuthorBuilder
        //    {
        //        Name = "Author",
        //        IconUrl = "https://www.google.com",
        //        Url = "https://shiroko.dev"
        //    },

        //    Title = "Title",
        //    Description = "Description",
        //    Color = new Discord.Color(255, 0, 0),

        //    Url = "https://shiroko.dev",
        //    ImageUrl = "https://i.imgur.com/niLjyNS.jpg",
        //    ThumbnailUrl = "https://i.imgur.com/niLjyNS.jpg",
        //    Timestamp = DateTime.Now,

        //    Embeds = new List<Discord.EmbeditedEmbedBuilder>()
        //    {
        //        new Discord.EmbeditedEmbedBuilder
        //        {
        //            Name = "Embed 1",
        //            Value = "Value 1",
        //            IsInline = true
        //        },
        //        new Discord.EmbeditedEmbedBuilder
        //        {
        //            Name = "Embed 2",
        //            Value = "Value 2",
        //            IsInline = true
        //        },
        //        new Discord.EmbeditedEmbedBuilder
        //        {
        //            Name = "Embed 3",
        //            Value = "Value 3",
        //            IsInline = false
        //        }
        //    },
        //    Footer = new Discord.EmbedFooterBuilder
        //    {
        //        Text = "Footer",
        //        IconUrl = "https://i.imgur.com/niLjyNS.jpg"
        //    },
        //}.Build();

        var embed = new Discord.EmbedBuilder
        {
            Author = new Discord.EmbedAuthorBuilder
            {
                Name = "Author",
                IconUrl = "https://i.imgur.com/niLjyNS.jpg",
                Url = "https://shiroko.dev"
            },
            Title = "Title",
            Description = "Description",
            Color = new Discord.Color(255, 0, 0),
        }.Build();

        List<Discord.Embed> tempList = Embeds.ToList();
        tempList.Add(embed);
        Embeds = tempList;

        // Option 2: Create a new IEnumerable with the added item using Concat method
        //Embeds = Embeds.Concat(new[] { embed });

        // Option 3: Create a new IEnumerable with the added item using Union method
        //Embeds = Embeds.Union(new[] { embed });

        //spEmbedCount.Text = Embeds.Count().ToString();
 */
