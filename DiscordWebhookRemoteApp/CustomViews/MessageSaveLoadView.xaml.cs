using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageSaveLoadView : ContentView
    {
        private SavedMessageItems _selectedItem;

        public SavedMessageItems SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; }
        }

        public ObservableCollection<SavedMessageItems> SavedMessages { get; set; }

        public MessageSaveLoadView()
        {
            InitializeComponent();
            BindingContext = this;
            SavedMessages = new ObservableCollection<SavedMessageItems>()
            {
                new SavedMessageItems(){ID="1",Name="Message 1",Content="Message 1 Content"},
            };
        }

        private void deleteItem_Tapped(object sender, EventArgs e)
        {

        }
        private void EditItem_Tapped(object sender, EventArgs e)
        {

        }
        private void selectItem_Tapped(object sender, EventArgs e)
        {

        }
    }
    public class SavedMessageItems
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}