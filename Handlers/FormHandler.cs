/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-windows10.0.19041.0)'
Before:
using System.Drawing;
using Microsoft.Maui;
After:
using Microsoft.Maui;
using System.Drawing;
*/

/* Unmerged change from project 'DiscordWebhookRemoteApp (net8.0-maccatalyst)'
Before:
using System.Drawing;
using Microsoft.Maui;
After:
using Microsoft.Maui;
using System.Drawing;
*/
#if IOS
using Foundation;
using UIKit;
#endif

#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace DiscordWebhookRemoteApp.Handlers
{
    public static class FormHandler
    {
        public static void RemoveBorders()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(
                "MyCustomization",
                (handler, view) =>
                {
#if ANDROID
                    handler.PlatformView.BackgroundTintList =
                        Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#endif
                }
            );

            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(
                "MyCustomization",
                (handler, view) =>
                {
#if ANDROID
                    handler.PlatformView.BackgroundTintList =
                        Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#endif
                }
            );

            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(
                "MyCustomization",
                (handler, view) =>
                {
#if ANDROID
                    handler.PlatformView.BackgroundTintList =
                        Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#endif
                }
            );
        }
    }
}
