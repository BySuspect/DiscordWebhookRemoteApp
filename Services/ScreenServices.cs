using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordWebhookRemoteApp.Services
{
    public static class ScreenServices
    {
        #region 10Percent
        public static double _10PercentWidth =>
            0.1 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

        public static double _10PercentHeight =>
            0.1 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
        #endregion

        #region 20Percent
        public static double _20PercentWidth =>
            0.2 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

        public static double _20PercentHeight =>
            0.2 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
        #endregion

        #region 30Percent
        public static double _30PercentWidth =>
            0.3 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

        public static double _30PercentHeight =>
            0.3 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
        #endregion

        #region 40Percent
        public static double _40PercentWidth =>
            0.4 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

        public static double _40PercentHeight =>
            0.4 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
        #endregion

        #region 50Percent
        public static double _50PercentWidth =>
            0.5 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

        public static double _50PercentHeight =>
            0.5 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
        #endregion

        #region 60Percent
        public static double _60PercentWidth =>
            0.6 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

        public static double _60PercentHeight =>
            0.6 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
        #endregion

        #region 70Percent
        public static double _70PercentWidth =>
            0.7 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

        public static double _70PercentHeight =>
            0.7 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
        #endregion

        #region 80Percent
        public static double _80PercentWidth =>
            0.8 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

        public static double _80PercentHeight =>
            0.8 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
        #endregion

        #region 90Percent
        public static double _90PercentWidth =>
            0.9 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

        public static double _90PercentHeight =>
            0.9 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
        #endregion

        #region 100Percent
        public static double _100PercentWidth =>
            1 * (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);

        public static double _100PercentHeight =>
            1 * (DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density);
        #endregion
    }
}
