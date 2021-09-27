using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FX.FP.NetCore.Common
{
    public class ConfigHelper
    {
        private static IConfiguration configuration;

        /// <summary>
        /// 创建静态构造函数
        /// </summary>
        static ConfigHelper()
        {
            string fileName = "appsettings.json";
            string directory = AppContext.BaseDirectory;
            directory = directory.Replace("\\", "/");

            string filePath = $"{directory}{fileName}";
            if (!File.Exists(filePath))
            {
                int length = directory.IndexOf("/bin");
                filePath = $"{directory.Substring(0, length)}{fileName}";
            }
            //AddJsonFile需要引用Microsoft.AspNetCore.Mvc.NewtonsoftJson
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile(filePath, false, true);

            configuration = builder.Build();
        }

        public static string GetSectionValue(string key)
        {
            return configuration.GetSection(key).Value;
        }

    }
}
