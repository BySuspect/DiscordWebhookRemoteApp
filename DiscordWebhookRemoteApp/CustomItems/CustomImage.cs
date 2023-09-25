using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DiscordWebhookRemoteApp.CustomItems
{
    public class CustomImage : Image
    {
        private int maximumHeightRequest;

        public int MaximumHeighRequest
        {
            get
            {
                return maximumHeightRequest;
            }
            set
            {
                if (maximumHeightRequest != value)
                {
                    maximumHeightRequest = value;
                    OnPropertyChanged(nameof(MaximumHeighRequest));
                }
            }
        }


        public CustomImage()
        {
            this.PropertyChanged += OnPropertyChanged;

            // Resmin yüklendikten sonra boyutlarını alın
            this.SizeChanged += (sender, e) =>
            {
                try
                {
                    double width = this.Width * DeviceDisplay.MainDisplayInfo.Density;
                    double height = this.Height * DeviceDisplay.MainDisplayInfo.Density;

                    Debug.WriteLine("");
                    Debug.WriteLine("");
                    Debug.WriteLine("");
                    Debug.WriteLine($"Image Width: {width}, Height: {height}");
                    Debug.WriteLine($"Device Width Size: {DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density}");
                    Debug.WriteLine($"Max Image Width Size: {300 * DeviceDisplay.MainDisplayInfo.Density}");
                    Debug.WriteLine("");
                    Debug.WriteLine("");
                    Debug.WriteLine("");

                    //if (width >= 300 * DeviceDisplay.MainDisplayInfo.Density)
                    //    this.WidthRequest = 300;
                    //else
                    //    this.WidthRequest = -1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            };
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }
    }
}
