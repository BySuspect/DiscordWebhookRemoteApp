using CommunityToolkit.Maui;
using DiscordWebhookRemoteApp.Handlers;
using Maui.TouchEffect.Hosting;
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
                .UseMauiTouchEffect();

            FormHandler.RemoveBorders();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
