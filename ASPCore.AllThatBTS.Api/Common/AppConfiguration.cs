using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCore.AllThatBTS.Api.Common
{
    public class AppConfiguration
    {
        public string SqlDataConnection { get; }
        public string JwtSecret { get; }

        public AppConfiguration()
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
            var appSetting = root.GetSection("ConfigurationManager");
            SqlDataConnection = appSetting["ConnectionString"];
            JwtSecret = appSetting["JwtSecret"];
        }
    }
}
