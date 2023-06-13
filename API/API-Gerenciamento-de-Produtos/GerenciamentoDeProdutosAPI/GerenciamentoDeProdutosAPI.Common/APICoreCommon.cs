using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GerenciamentoDeProdutosAPI.Common
{
    public class APICoreCommon
    {
        public static string GetValueSetting(string settingName)
        {
            var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json");

            var cfg = builder.Build();

#if DEBUG
            if (settingName == "CONNECTION_STRING")
                return cfg["CONNECTION_STRING_DEBUG"];
#endif

            return cfg[settingName];
        }
    }
}
