using DiscordWebhookRemoteApp.Helpers;
using DiscordWebhookRemoteApp.Pages;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

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
            //Preferences.Set("privacy_policy_accepted", false);
            //Preferences.Set("privacy_policy_accepted17May2023", false);
            //Preferences.Set("SupportPopupDate", null);
            MainPage = new NavigationPage(new TestPage())
            {
                BarBackgroundColor = ThemeColors.StatusBarColor,
                BarTextColor = ThemeColors.TextColor,
            };
#endif
#if !DEBUG

            if (Preferences.Get("privacy_policy_accepted17May2023", false))
                MainPage = new NavigationPage(new MainPage())
                {
                    BarBackgroundColor = ThemeColors.StatusBarColor,
                    BarTextColor = ThemeColors.TextColor,
                };
            else
                MainPage = new PrivacyPolicyPage();
#endif

            //MainPage = new TestPage();



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

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            base.OnAppLinkRequestReceived(uri);

            Debug.WriteLine($"\n\n\n{uri.ToString()}\n\n\n");

            Application.Current.MainPage = new TestPage(uri.ToString());
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
