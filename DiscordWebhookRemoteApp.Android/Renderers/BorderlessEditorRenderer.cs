using System;
using DiscordWebhookRemoteApp.Components.Partials.CustomItems;
using DiscordWebhookRemoteApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]


#pragma warning restore CS0612 // Type or member is obsolete
namespace DiscordWebhookRemoteApp.Droid.Renderers
{
    [Obsolete]
    public class BorderlessEditorRenderer : EditorRenderer
    {
        public static void Init() { }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                SetPadding(0, 0, 0, 0);
            }
        }
    }
}
