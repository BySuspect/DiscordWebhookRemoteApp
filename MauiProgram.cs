using CommunityToolkit.Maui;
using DiscordWebhookRemoteApp.Handlers;
using Plugin.MauiMTAdmob;
#if DEBUG
using Microsoft.Extensions.Logging;
#endif

namespace DiscordWebhookRemoteApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().UseMauiMTAdmob().UseMauiCommunityToolkit();

            FormHandler.RemoveBorders();
            AppThemeService.SetTheme(AppThemeTypes.Discord);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
