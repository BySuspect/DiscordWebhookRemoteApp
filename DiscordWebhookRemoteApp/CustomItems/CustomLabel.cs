using IntelliAbb.Xamarin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace DiscordWebhookRemoteApp.CustomItems
{
    public class CustomLabel : Label
    {
        private DateTime? _savedDate;

        public DateTime? SavedDateTimeData
        {
            get
            {
                return _savedDate;
            }
            set
            {
                if (value != _savedDate)
                {
                    _savedDate = value;
                    OnPropertyChanged(nameof(SavedDateTimeData));
                }
            }
        }

        public CustomLabel() : base()
        {
            PropertyChanged += OnPropertyChanged;
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SavedDateTimeData))
            {
                if (SavedDateTimeData != null)
                {
                    Text = SavedDateTimeData.ToString();
                    IsVisible = true;
                }
                else
                {
                    Text = "Selected Date and Time";
                    //IsVisible = false;
                }
            }
        }
    }
}
