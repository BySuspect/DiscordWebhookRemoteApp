using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using DiscordWebhookRemoteApp.Handlers;
using Microsoft.Extensions.Logging;
using Plugin.MauiMTAdmob;

namespace DiscordWebhookRemoteApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMTAdmob()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup();

            FormHandler.RemoveBorders();
            AppThemeService.SetTheme(AppThemeTypes.Discord);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
