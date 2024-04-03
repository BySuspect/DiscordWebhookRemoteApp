using CommunityToolkit.Maui;
/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-maccatalyst)'
Before:
using DiscordWebhookRemoteApp.Handlers;
using Microsoft.Extensions.Logging;
using Plugin.MauiMTAdmob;
After:
using DiscordWebhookRemoteApp.Handlers;

using Microsoft.Extensions.Logging;

using Plugin.MauiMTAdmob;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-ios)'
Before:
using DiscordWebhookRemoteApp.Handlers;
using Microsoft.Extensions.Logging;
using Plugin.MauiMTAdmob;
After:
using DiscordWebhookRemoteApp.Handlers;

using Microsoft.Extensions.Logging;

using Plugin.MauiMTAdmob;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-windows10.0.19041.0)'
Before:
using DiscordWebhookRemoteApp.Handlers;
using Microsoft.Extensions.Logging;
using Plugin.MauiMTAdmob;
After:
using DiscordWebhookRemoteApp.Handlers;

using Microsoft.Extensions.Logging;

using Plugin.MauiMTAdmob;
*/
using DiscordWebhookRemoteApp.Handlers;
using Plugin.MauiMTAdmob;

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
