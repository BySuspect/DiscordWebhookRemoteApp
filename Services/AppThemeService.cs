using CommunityToolkit.Maui.Core;

namespace DiscordWebhookRemoteApp.Services
{
    public static class AppThemeService
    {
        public static void SetTheme(AppThemeTypes theme)
        {
            switch (theme)
            {
                case AppThemeTypes.Discord:
                    DiscordTheme();
                    break;
                case AppThemeTypes.Light:
                    LightTheme();
                    break;
                case AppThemeTypes.Dark:
                    DarkTheme();
                    break;
                case AppThemeTypes.Test:
                    TestTheme();
                    break;
            }
        }

        #region Themes

        private static void LightTheme()
        {
            AppThemeColors.TextColor = Color.Parse("#000000");
            AppThemeColors.BorderColor = Color.Parse("#000000");
            AppThemeColors.BackgroundColor = Color.Parse("#FFFFFF");
            AppThemeColors.StatusBarStyle = StatusBarStyle.DarkContent;
            AppThemeColors.backgroundImg = null;
        }

        private static void DarkTheme()
        {
            AppThemeColors.TextColor = Color.Parse("#FFFFFF");
            AppThemeColors.BorderColor = Color.Parse("#E6B8B8B8");
            AppThemeColors.BackgroundColor = Color.Parse("#262323");
            AppThemeColors.StatusBarStyle = StatusBarStyle.LightContent;
            AppThemeColors.backgroundImg = null;
        }

        private static void DiscordTheme()
        {
            AppThemeColors.TextColor = Color.Parse("#ffffff");
            AppThemeColors.PlaceholderTextColor = Color.Parse("#98aab5");
            AppThemeColors.BorderColor = Color.Parse("#98aab5");
            AppThemeColors.BackgroundColor = Color.Parse("#09090E");
            AppThemeColors.StatusBarStyle = StatusBarStyle.LightContent;
            AppThemeColors.backgroundImg = null;
        }

        private static void TestTheme()
        {
            AppThemeColors.TextColor = Colors.Purple;
            AppThemeColors.PlaceholderTextColor = Colors.Blue;
            AppThemeColors.BorderColor = Colors.Green;
            AppThemeColors.BackgroundColor = Colors.DarkGray;
            AppThemeColors.StatusBarStyle = StatusBarStyle.LightContent;
            AppThemeColors.backgroundImg = null;
        }
        #endregion
    }

    public static class AppThemeColors
    {
        public static Color? TextColor { get; set; }
        public static Color? PlaceholderTextColor { get; set; }
        public static Color? BorderColor { get; set; }
        public static Color? BackgroundColor { get; set; }
        public static StatusBarStyle StatusBarStyle { get; set; }
        public static ImageSource? backgroundImg { get; set; }
    }

    public enum AppThemeTypes
    {
        Discord,
        Light,
        Dark,
        Test
    }
}
