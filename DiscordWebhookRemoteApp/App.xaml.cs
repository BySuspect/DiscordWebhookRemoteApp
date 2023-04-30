using DiscordWebhookRemoteApp.Helpers;
using DiscordWebhookRemoteApp.Pages;
using DiscordWebhookRemoteApp.Pages.Popups;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiscordWebhookRemoteApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //AppActions.OnAppAction += AppActions_OnAppAction;
            switch (AppInfo.RequestedTheme)
            {
                case AppTheme.Unspecified:
                    ChangeAppTheme.DarkTheme();
                    break;
                case AppTheme.Light:
                    ChangeAppTheme.LightTheme();
                    break;
                case AppTheme.Dark:
                    ChangeAppTheme.DarkTheme();
                    break;
                default:
                    break;
            }
            if (Preferences.Get("{zenandshriokossecret}", false))
                ChangeAppTheme.ForDenizTheme();

#if DEBUG
            Preferences.Set("privacy_policy_accepted", true);
#endif

            if (Preferences.Get("privacy_policy_accepted", false))
                MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = ThemeColors.StatusBarColor,
                    BarTextColor = ThemeColors.TextColor,
                };
            else
                MainPage = new PrivacyPolicyPage();

        }

        void AppActions_OnAppAction(object sender, AppActionEventArgs e)
        {
            // Don't handle events fired for old application instances
            // and cleanup the old instance's event handler
            if (Application.Current != this && Application.Current is App app)
            {
                AppActions.OnAppAction -= app.AppActions_OnAppAction;
                return;
            }
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Shell.Current.GoToAsync($"//{e.AppAction.Id}");
            });
        }
        protected override void OnStart()
        {
            //sade temizliyor
            //_ = AppActions.SetAsync();

            //try
            //{
            //    await AppActions.SetAsync(new AppAction("app_info", "App Info"));
            //}
            //catch (FeatureNotSupportedException ex)
            //{
            //    Debug.WriteLine("App Actions not supported");
            //}
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
