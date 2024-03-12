using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Components.Partials.Views.WebhookItemsView.EmbedView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmbedsView : ContentView
    {
        //TODO: ObservableCollection deneyebilirm
        public static BindableProperty EmbedsProperty = BindableProperty.Create(
            nameof(Embeds),
            typeof(IEnumerable<Discord.Embed>),
            typeof(EmbedsView),
            default(IEnumerable<Discord.Embed>),
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var view = (EmbedsView)bindable;
                view.lvEmbeds.ItemsSource = (IEnumerable<Discord.Embed>)newValue;
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
                view.lvEmbeds.ItemsSource = (IEnumerable<Discord.Embed>)newValue;
            }
        );
        public IEnumerable<Discord.Embed> Embeds
        {
            get { return (IEnumerable<Discord.Embed>)GetValue(EmbedsProperty); }
            set { SetValue(EmbedsProperty, value); }
        }

        public EmbedsView()
        {
            InitializeComponent();
            BindingContext = this;

            Embeds = new List<Discord.Embed>();
        }

        private void btnNewEmbed_Clicked(object sender, EventArgs e)
        {
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

            //    Fields = new List<Discord.EmbedFieldBuilder>()
            //    {
            //        new Discord.EmbedFieldBuilder
            //        {
            //            Name = "Field 1",
            //            Value = "Value 1",
            //            IsInline = true
            //        },
            //        new Discord.EmbedFieldBuilder
            //        {
            //            Name = "Field 2",
            //            Value = "Value 2",
            //            IsInline = true
            //        },
            //        new Discord.EmbedFieldBuilder
            //        {
            //            Name = "Field 3",
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

            spEmbedCount.Text = Embeds.Count().ToString();
        }

        private void ListView_PropertyChanged(
            object sender,
            System.ComponentModel.PropertyChangedEventArgs e
        )
        {
            Console.WriteLine(".n.nProperty Changed: " + e.PropertyName + ".n.n");
        }
    }

    public class WebhookMessageEmbedsViewItems { }
}
