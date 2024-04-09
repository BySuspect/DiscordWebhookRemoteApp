using System.Diagnostics;
using CommunityToolkit.Maui.Views;

namespace DiscordWebhookRemoteApp.Components.Popups.Embed;

public partial class ColorPickPopup : Popup
{
    public ColorPickPopup(Color? color = null)
    {
        InitializeComponent();
        colorPicker.PickedColor = color ?? Colors.Black;
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(colorPicker.PickedColor);
    }

    private void colorPicker_PickedColorChanged(
        object sender,
        Partials.Views.CustomItemViews.PickedColorChangedEventArgs e
    )
    {
        try
        {
            Debug.WriteLine(e.NewColor.ToHex());
            entryHex.Text = e.NewColor.ToHex();
            entryRed.Text = Math.Clamp(Math.Round(e.NewColor.Red * 255), 0, 255).ToString();
            entryGreen.Text = Math.Clamp(Math.Round(e.NewColor.Green * 255), 0, 255).ToString();
            entryBlue.Text = Math.Clamp(Math.Round(e.NewColor.Blue * 255), 0, 255).ToString();
            lblColorText.TextColor = ApplicationService.InvertColor(e.NewColor);
        }
        catch { }
    }

    private void entryHex_Completed(object sender, EventArgs e)
    {
        try
        {
            colorPicker.PickedColor = Color.Parse(entryHex.Text);
        }
        catch
        {
            entryHex.Text = colorPicker.PickedColor.ToHex();
        }
    }

    private void entryRed_Completed(object sender, EventArgs e)
    {
        try
        {
            var color = Color.FromRgb(
                double.Parse(entryRed.Text) / 255,
                double.Parse(entryGreen.Text) / 255,
                double.Parse(entryBlue.Text) / 255
            );
            colorPicker.PickedColor = color;
        }
        catch
        {
            entryRed.Text = Math.Clamp(Math.Round(colorPicker.PickedColor.Red * 255), 0, 255)
                .ToString();
        }
    }

    private void entryGreen_Completed(object sender, EventArgs e)
    {
        try
        {
            var color = Color.FromRgb(
                double.Parse(entryRed.Text) / 255,
                double.Parse(entryGreen.Text) / 255,
                double.Parse(entryBlue.Text) / 255
            );
            colorPicker.PickedColor = color;
        }
        catch
        {
            entryGreen.Text = Math.Clamp(Math.Round(colorPicker.PickedColor.Green * 255), 0, 255)
                .ToString();
        }
    }

    private void entryBlue_Completed(object sender, EventArgs e)
    {
        try
        {
            var color = Color.FromRgb(
                double.Parse(entryRed.Text) / 255,
                double.Parse(entryGreen.Text) / 255,
                double.Parse(entryBlue.Text) / 255
            );
            colorPicker.PickedColor = color;
        }
        catch
        {
            entryBlue.Text = Math.Clamp(Math.Round(colorPicker.PickedColor.Blue * 255), 0, 255)
                .ToString();
        }
    }
}
