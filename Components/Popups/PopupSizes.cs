namespace DiscordWebhookRemoteApp.Components.Popups
{
    public static class PopupSize
    {
        public static Size Tiny => new Size(100, 100);

        public static Size Small => new Size(300, 250);
        public static Size WebhookAddOrEditPopup => new Size(300, 265);
        public static Size Medium =>
            new Size(
                0.7 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density),
                0.6 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density)
            );
        public static Size Medium2 =>
            new Size(
                0.9 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density),
                1.08 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density)
            );
        public static Size Tall =>
            new Size(
                0.8 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density),
                0.7 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density)
            );

        public static Size Large =>
            new Size(
                0.9 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density),
                0.8 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density)
            );
        public static Size Square =>
            new Size(
                0.9 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density),
                0.9 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density)
            );
    }
}
