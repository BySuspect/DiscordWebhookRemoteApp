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
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        private ObservableCollection<webhookProfileItems> _savedProfiles;

        public ObservableCollection<webhookProfileItems> SavedProfiles
        {
            get
            {
                return _savedProfiles;
            }
            set
            {
                _savedProfiles = value;
                OnPropertyChanged(nameof(SavedProfiles));
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
                    name="Test1",
                    image="https://cdn.discordapp.com/avatars/510531111647051796/1996bd9b3b369c8405378e405d049a88.png?size=4096",
                },
                new webhookProfileItems
                {
                    ID=1,
                    name="Test2",
                    image="https://cdn.discordapp.com/avatars/410054920600027147/a_50693c392bcc8694970249cecc61be01.png?size=4096",
                },
                new webhookProfileItems
                {
                    ID=2,
                    name="Test3",
                    image="https://cdn.discordapp.com/avatars/272665050672660501/f6b1c6bd7376412852e6b54a3f223987.png?size=4096",
                },
                new webhookProfileItems
                {
                    ID=3,
                    name="Test4",
                    image="https://cdn.discordapp.com/avatars/410054920600027147/a_50693c392bcc8694970249cecc61be01.png?size=4096",
                },
                new webhookProfileItems
                {
                    ID=4,
                    name="Test5",
                    image="https://cdn.discordapp.com/avatars/272665050672660501/f6b1c6bd7376412852e6b54a3f223987.png?size=4096",
                },
            };
            //References.WebhookProfileList = gecicilist;
#endif

            SavedProfiles = References.WebhookProfileList;
            if (!string.IsNullOrEmpty(url)
                && !string.IsNullOrEmpty(name)
                && SavedProfiles.Where(x => x.image == url && x.name == name).ToList().Count == 0
                && !url.Contains("dcdemoimage"))
            {
                entryWebhookName.Text = name;
                entryWebhookImage.Text = url;
                newWebhookProfileBack.IsVisible = true;
                newBtnImg.RotateTo(135);
            }
        }

        private void selectItem_Tapped(object sender, EventArgs e)
        {
            var selected = sender as Xamarin.Forms.Image;
            Dismiss(SavedProfiles.Where(x => x.ID == int.Parse(selected.AutomationId)).First());
        }
        private void deleteItem_Tapped(object sender, EventArgs e)
        {
            var selected = sender as Xamarin.Forms.Image;
            var gecicilist = SavedProfiles;
            gecicilist.Remove(gecicilist.Where(x => x.ID == int.Parse(selected.AutomationId)).FirstOrDefault());
            SavedProfiles = gecicilist;
            References.WebhookProfileList = SavedProfiles;
        }
        private void NewProfileSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                entryWebhookImage.Unfocus();
                entryWebhookName.Unfocus();
                if (!string.IsNullOrEmpty(entryWebhookName.Text) && !string.IsNullOrEmpty(entryWebhookImage.Text))
                {
                    string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    bool isImageUrl = imageExtensions.Any(entryWebhookImage.Text.Trim().Contains);

                    if (isImageUrl)
                    {
                        var gecicilist = SavedProfiles;
                        gecicilist.Add(new webhookProfileItems
                        {
                            ID = (SavedProfiles.Count > 0) ? (SavedProfiles.OrderBy(x => x.ID).Max(x => x.ID) + 1) : 1,
                            name = entryWebhookName.Text.Trim(),
                            image = entryWebhookImage.Text.Trim(),
                        });
                        newRoomViewClose_Tapped(null, null);
                        SavedProfiles = gecicilist;
                        References.WebhookProfileList = SavedProfiles;
                    }
                    else
                    {
                        entryWebhookImage.TextColor = Color.Red;
                        entryWebhookImage.TextChanged += (s, evnt) =>
                        {
                            entryWebhookImage.TextColor = ThemeColors.TextColor;
                        };
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void newWebhookProfile_Tapped(object sender, EventArgs e)
        {
            entryWebhookImage.Unfocus();
            entryWebhookName.Unfocus();
            newBtnImg.CancelAnimations();
            if (newWebhookProfileBack.IsVisible)
            {
                await newBtnImg.RotateTo(165, 250);
                await newBtnImg.RotateTo(-30, 250);
                newWebhookProfileBack.IsVisible = false;
                await newBtnImg.RotateTo(0, 250);
            }
            else
            {
                await newBtnImg.RotateTo(-30, 250);
                await newBtnImg.RotateTo(165, 250);
                newWebhookProfileBack.IsVisible = true;
                await newBtnImg.RotateTo(135, 250);
                entryWebhookName.Text = "";
                entryWebhookImage.Text = "";
                //imgWebhookImage.Source = "dcdemoimage.png";
            }
        }
        private async void newRoomViewClose_Tapped(object sender, EventArgs e)
        {
            entryWebhookImage.Unfocus();
            entryWebhookName.Unfocus();
            newBtnImg.CancelAnimations();
            await newBtnImg.RotateTo(165, 250);
            await newBtnImg.RotateTo(-30, 250);
            newWebhookProfileBack.IsVisible = false;
            await newBtnImg.RotateTo(0, 250);
            //imgWebhookImage.Source = "dcdemoimage.png";
        }
    }
}