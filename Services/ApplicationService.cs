using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Popups;

namespace DiscordWebhookRemoteApp.Services
{
    public static class ApplicationService
    {
        #region Loading View Section
        private static LoadingViewPopup loadingPopup = new LoadingViewPopup();
        private static bool IsOpen = false;

        public static void ShowLoadingView()
        {
            if (IsOpen)
                return;
            loadingPopup = new LoadingViewPopup();
            loadingPopup.Opened += (s, e) => IsOpen = true;
            loadingPopup.CanBeDismissedByTappingOutsideOfPopup = false;
            PopupExtensions.ShowPopup(Application.Current.MainPage, loadingPopup);
        }

        public static void HideLoadingView()
        {
            if (!IsOpen)
                return;

            loadingPopup.Close();
            IsOpen = false;
        }
        #endregion
    }
}
