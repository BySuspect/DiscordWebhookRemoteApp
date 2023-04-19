using DiscordWebhookRemoteApp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace DiscordWebhookRemoteApp.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebhookProfileSaveEditPopup : Popup
    {
        /* 
         *avatara tıkalndında eğer seçim modu deilse expanderın açılıp kapanması deiştirilcek
         *edit butonuna basınca seçilen autamationid ile entry enabledi true yapılıp aynı yerin edit butonu saveye çevrilicek
         *avatara tıklayınca eğer edit modundaysa resim deiştirme olayı ayarlanıcak
         */
        ObservableCollection<webhookProfileItems> _savedProfiles;
        public ObservableCollection<webhookProfileItems> SavedProfiles
        {
            get
            {
                return _savedProfiles ?? new ObservableCollection<webhookProfileItems>();
            }
            set
            {
                //if (_savedProfiles != value)
                //{
                _savedProfiles = value;
                OnPropertyChanged(nameof(SavedProfiles));
                //}
            }
        }
        public WebhookProfileSaveEditPopup(string url, string name)
        {
            InitializeComponent();
            BindingContext = this;
#if DEBUG
            var gecicilist = new ObservableCollection<webhookProfileItems>()
            {
                new webhookProfileItems
                {
                    ID=0,
                    editMode=false,
                    name="Test1",
                    image="https://cdn.discordapp.com/avatars/510531111647051796/1996bd9b3b369c8405378e405d049a88.png?size=4096",
                },
                new webhookProfileItems
                {
                    ID=1,
                    editMode=false,
                    name="Test2",
                    image="https://cdn.discordapp.com/avatars/410054920600027147/a_50693c392bcc8694970249cecc61be01.png?size=4096",
                },
                new webhookProfileItems
                {
                    ID=2,
                    editMode=false,
                    name="Test3",
                    image="https://cdn.discordapp.com/avatars/272665050672660501/f6b1c6bd7376412852e6b54a3f223987.png?size=4096",
                },
                new webhookProfileItems
                {
                    ID=3,
                    editMode=false,
                    name="Test4",
                    image="https://cdn.discordapp.com/avatars/410054920600027147/a_50693c392bcc8694970249cecc61be01.png?size=4096",
                },
                new webhookProfileItems
                {
                    ID=4,
                    editMode=false,
                    name="Test5",
                    image="https://cdn.discordapp.com/avatars/272665050672660501/f6b1c6bd7376412852e6b54a3f223987.png?size=4096",
                },
            };
            //References.WebhookProfileList = gecicilist;
#endif
            SavedProfiles = References.WebhookProfileList;
            //ProfileList.ItemsSource = savedProfiles;
            SavedProfiles.CollectionChanged += SavedProfiles_CollectionChanged;
        }

        private void btnSelect_Clicked(object sender, EventArgs e)
        {
            var selecteditem = sender as Button;
            int selectedIndex = SavedProfiles.IndexOf(SavedProfiles.Where(x => x.ID == int.Parse(selecteditem.AutomationId)).FirstOrDefault());
            var selected = SavedProfiles[selectedIndex];



            Debug.WriteLine($"selectbtn selected index: {selectedIndex}");
        }
        private void btnEdit_Clicked(object sender, EventArgs e)
        {
            var selecteditem = sender as Button;
            int selectedIndex = SavedProfiles.IndexOf(SavedProfiles.Where(x => x.ID == int.Parse(selecteditem.AutomationId)).FirstOrDefault());

            var itemToEdit = SavedProfiles.FirstOrDefault(x => x.ID == int.Parse(selecteditem.AutomationId));
            if (itemToEdit != null)
            {
                itemToEdit.name = "New Test" + selecteditem.AutomationId; // öğenin adını değiştir
                itemToEdit.image = "https://cdn.discordapp.com/avatars/354256550082248704/4588715e0cf7b5970ec362015f61210b.png?size=4096"; // öğenin resmini değiştir
            }

            ProfileList.ItemsSource = null;
            ProfileList.ItemsSource = SavedProfiles;


            Debug.WriteLine($"editbtn selected index: {selectedIndex} {SavedProfiles[selectedIndex].editMode}");
        }
        private void imageEdit_Tapped(object sender, EventArgs e)
        {
            var selecteditem = sender as Xamarin.Forms.Image;
            int selectedIndex = SavedProfiles.IndexOf(SavedProfiles.Where(x => x.ID == int.Parse(selecteditem.AutomationId)).FirstOrDefault());
            var selected = SavedProfiles[selectedIndex];




            Debug.WriteLine($"avatar selected index: {selectedIndex}");
        }
        private void deleteItem_Tapped(object sender, EventArgs e)
        {
            var selected = sender as Xamarin.Forms.Image;
            SavedProfiles.Remove(SavedProfiles.Where(x => x.ID == int.Parse(selected.AutomationId)).FirstOrDefault());
            //References.WebhookProfileList = savedProfiles;
        }

        private void ProfileList_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }
        private void SavedProfiles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

        }
    }
}