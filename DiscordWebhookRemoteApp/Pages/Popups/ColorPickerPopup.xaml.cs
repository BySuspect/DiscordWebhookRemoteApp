using DiscordWebhookRemoteApp.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
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
            entryHex.Text = "#" + e.NewColor.ToHex().ToString().Remove(0, 3);
            entryRed.Text = MathP.Clamp(Math.Round(e.NewColor.R * 255), 0, 255).ToString();
            entryGreen.Text = MathP.Clamp(Math.Round(e.NewColor.G * 255), 0, 255).ToString();
            entryBlue.Text = MathP.Clamp(Math.Round(e.NewColor.B * 255), 0, 255).ToString();
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

        private void entryHex_Completed(object sender, EventArgs e)
        {
            try
            {
                var color = Color.FromHex(entryHex.Text);
                colorPicker.SelectedColor = color;
            }
            catch
            {
                //entryHex.Text = "#" + colorPicker.SelectedColor.ToHex().ToString().Remove(0, 3);
            }
        }
        private void entryRed_Completed(object sender, EventArgs e)
        {
            try
            {
                var color = Color.FromRgb(double.Parse(entryRed.Text) / 255, double.Parse(entryGreen.Text) / 255, double.Parse(entryBlue.Text) / 255);
                colorPicker.SelectedColor = color;
            }
            catch
            {
                entryRed.Text = MathP.Clamp(Math.Round(colorPicker.SelectedColor.R * 255), 0, 255).ToString();
            }
        }
        private void entryGreen_Completed(object sender, EventArgs e)
        {
            try
            {
                var color = Color.FromRgb(double.Parse(entryRed.Text) / 255, double.Parse(entryGreen.Text) / 255, double.Parse(entryBlue.Text) / 255);
                colorPicker.SelectedColor = color;
            }
            catch
            {
                entryGreen.Text = MathP.Clamp(Math.Round(colorPicker.SelectedColor.G * 255), 0, 255).ToString();
            }
        }
        private void entryBlue_Completed(object sender, EventArgs e)
        {
            try
            {
                var color = Color.FromRgb(double.Parse(entryRed.Text) / 255, double.Parse(entryGreen.Text) / 255, double.Parse(entryBlue.Text) / 255);
                colorPicker.SelectedColor = color;
            }
            catch
            {
                entryBlue.Text = MathP.Clamp(Math.Round(colorPicker.SelectedColor.B * 255), 0, 255).ToString();
            }
        }
    }
}
