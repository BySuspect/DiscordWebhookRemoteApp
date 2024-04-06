using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using DiscordWebhookRemoteApp.Components.Popups.Common;

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

        #region ShowPopup Section
        public static async Task<object?> ShowPopupAsync(Popup popup)
        {
            return await PopupExtensions.ShowPopupAsync(Application.Current.MainPage, popup);
        }

        public static Task ShowPopup(Popup popup)
        {
            PopupExtensions.ShowPopupAsync(Application.Current.MainPage, popup);
            return Task.CompletedTask;
        }
        #endregion

        #region ShowToast Section
        public static async Task ShowShortToastAsync(string text)
        {
            await Toast.Make(text, ToastDuration.Short).Show();
        }

        public static void ShowShortToast(string text)
        {
            Task.Run(() => showShortToast(text));
        }

        private static Task showShortToast(string text)
        {
            return Toast.Make(text, ToastDuration.Short).Show();
        }

        public static async Task ShowLongToastAsync(string text)
        {
            await Toast.Make(text, ToastDuration.Long).Show();
        }

        public static void ShowLongToast(string text)
        {
            Task.Run(() => showLongToast(text));
        }

        public static Task showLongToast(string text)
        {
            return Toast.Make(text, ToastDuration.Long).Show();
        }
        #endregion

        #region Alerts Section
        public static async Task<bool> ShowCustomAlertAsync(
            string title,
            string content,
            string ok,
            string? cancel = null
        )
        {
            return (bool)await ShowPopupAsync(new CustomAlertPopup(title, content, ok, cancel));
        }

        public static void ShowCustomAlert(
            string title,
            string content,
            string ok,
            string? cancel = null
        )
        {
            ShowPopup(new CustomAlertPopup(title, content, ok, cancel));
        }
        #endregion
    }
}
