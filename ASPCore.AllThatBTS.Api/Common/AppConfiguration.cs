using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Time;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using TimeZoneConverter;

namespace ASPCore.AllThatBTS.Api.Common
{
    public static class AppConfiguration
    {
        public static string SqlDataConnection
        {
            get
            {
                return GetSqlConnection();
            }
        }
        public static string JwtSecret
        {
            get
            {
                return GetJwtSecret();
            }
        }

        public static string NLogPath
        {
            get
            {
                return GetNLogPath();
            }
        }

        private static string GetNLogPath()
        {
            GlobalDiagnosticsContext.Set("configDir", Path.Combine(Directory.GetCurrentDirectory(), "NLogFile"));
            string logConfigPath = string.Empty;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                logConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "nlog.Development.config");
            }
            else
            {
                logConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "nlog.config");
            }

            return logConfigPath;
        }

        private static IConfigurationSection AppSetting
        {
            get
            {
                var configurationBuilder = new ConfigurationBuilder();
                string path = string.Empty;
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json");
                }
                else
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                }

                configurationBuilder.AddJsonFile(path, false);

                var root = configurationBuilder.Build();
                return root.GetSection("ConfigurationManager");
            }
        }

        public static string GetSqlConnection()
        {
            return AppSetting["ConnectionString"];
        }

        public static string GetJwtSecret()
        {
            return AppSetting["JwtSecret"];
        }

    }
}


[TimeSource("CustomTimeZone")]
public class CustomTimeZoneTimeSource : TimeSource
{
    string ZoneName;
    TimeZoneInfo ZoneInfo;

    [Required]
    public string Zone
    {
        get { return ZoneName; }
        set
        {
            ZoneName = value;
            ZoneInfo
                = TZConvert.GetTimeZoneInfo(ZoneName);
        }
    }

    public override DateTime Time
    {
        get
        {
            return TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow, ZoneInfo);
        }
    }

    public override DateTime FromSystemTime(DateTime systemTime)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(systemTime, ZoneInfo);
    }
}