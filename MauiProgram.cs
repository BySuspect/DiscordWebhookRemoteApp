using System.Reflection;
using CommunityToolkit.Maui;
using DiscordWebhookRemoteApp.Handlers;
using DiscordWebhookRemoteApp.Helpers;
using Microsoft.Extensions.Configuration;
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
            builder.UseMauiApp<App>().UseMauiCommunityToolkit().UseSkiaSharp();

            builder.AddEnvJson();

            StaticPropertiesService.LoggingApiUrl = EncryptionHelper.Encrypt(
                builder.Configuration.GetValue<string>("LOGGING_API_URL")
            );

            StaticPropertiesService.LoggingApiKey = EncryptionHelper.Encrypt(
                builder.Configuration.GetValue<string>("LOGGING_API_KEY")
            );

            FormHandler.RemoveBorders();
            AppThemeService.SetTheme(AppThemeTypes.Discord);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void AddEnvJson(this MauiAppBuilder builder)
        {
            using Stream? stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("DiscordWebhookRemoteApp.env.json");
            if (stream != null)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
                builder.Configuration.AddConfiguration(config);
            }
        }
    }
}
