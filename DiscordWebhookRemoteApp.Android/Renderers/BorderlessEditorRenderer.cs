using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Views.ViewGroup;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using DiscordWebhookRemoteApp.CustomItems;
using DiscordWebhookRemoteApp.Droid.Renderers;

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