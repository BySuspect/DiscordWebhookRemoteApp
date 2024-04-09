using CommunityToolkit.Maui;
using DiscordWebhookRemoteApp.Handlers;
using Plugin.MauiMTAdmob;
using SkiaSharp.Views.Maui.Controls.Hosting;
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
            builder.UseMauiApp<App>().UseMauiMTAdmob().UseMauiCommunityToolkit().UseSkiaSharp();

            FormHandler.RemoveBorders();
            AppThemeService.SetTheme(AppThemeTypes.Discord);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
