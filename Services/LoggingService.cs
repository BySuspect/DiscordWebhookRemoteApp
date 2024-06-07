using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace DiscordWebhookRemoteApp.Services
{
    public static class LoggingService
    {
        public static List<LogMessageModel> LogManager
        {
            get
            {
                var logs = JsonConvert.DeserializeObject<List<LogMessageModel>>(
                    Preferences.Get("ApplicationLogs", null) ?? "[]"
                );

                return logs ?? new List<LogMessageModel>();
            }
            set { Preferences.Set("ApplicationLogs", JsonConvert.SerializeObject(value)); }
        }

        public static Task Log(string message, LogLevel level, string? errorCode = null)
        {
            var manager = LogManager;
            var log = new LogMessageModel
            {
                timestamp = DateTime.UtcNow,
                level = level,
                message = message,
                context = new LogContext { error_code = errorCode }
            };
            manager ??= new List<LogMessageModel>();
            manager.Add(log);
            LogManager = manager;

            return Task.CompletedTask;
        }

        public class LogMessageModel
        {
            public required DateTime timestamp { get; set; }
            public required LogLevel level { get; set; }
            public required string message { get; set; }
            public required LogContext context { get; set; }
        }

        public class DeviceDetails
        {
            public DeviceDetails()
            {
                DeviceGuid = Guid.Parse(Preferences.Get("DeviceGuid", new Guid().ToString()));
                if (DeviceGuid == Guid.Empty || DeviceGuid is null)
                {
                    DeviceGuid = Guid.NewGuid();
                    Preferences.Set("DeviceGuid", DeviceGuid.ToString());
                }
            }

            public Guid? DeviceGuid { get; set; }
            public string DeviceModel { get; set; } = DeviceInfo.Model;
            public string DeviceManufacturer { get; set; } = DeviceInfo.Manufacturer;
            public string DeviceName { get; set; } = DeviceInfo.Name;
            public string DeviceVersion { get; set; } = DeviceInfo.VersionString;
            public string DevicePlatform { get; set; } = DeviceInfo.Platform.ToString();
            public string DeviceType { get; set; } = DeviceInfo.Idiom.ToString();
        }

        public class LogContext
        {
            public string application { get; set; } = AppInfo.PackageName;
            public string version { get; set; } = AppInfo.VersionString;
            public string? error_code { get; set; }
        }

        public enum LogLevel
        {
            Info,
            Warning,
            Error
        }
    }
}
