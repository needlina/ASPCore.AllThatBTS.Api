using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        private static IConfigurationSection appSetting
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
            return appSetting["ConnectionString"];
        }

        public static string GetJwtSecret()
        {
            return appSetting["JwtSecret"];
        }

    }
}
