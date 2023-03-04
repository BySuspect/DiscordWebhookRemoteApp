using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPickerPopup : Popup
    {
        Page _page;
        public ColorPickerPopup(Page page, Color selected)
        {
            InitializeComponent();
            _page = page;
            colorPicker.SelectedColor = selected;
        }

        private void ColorWheel_SelectedColorChanged(object sender, ColorPicker.BaseClasses.ColorPickerEventArgs.ColorChangedEventArgs e)
        {
            Debug.WriteLine(e.NewColor.ToHex());
            ((BoxView)_page.FindByName("embedBodyColorPicker")).SetValue(BoxView.ColorProperty, e.NewColor);
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            Dismiss(colorPicker.SelectedColor);
        }
        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Dismiss("cancel");
        }
    }
}