using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebAdvert.Search.Worker.Helpers
{
    // handles creation of elasticsearch instances
    // needed since DI is not usable

    public static class ConfigurationHelper
    {
        private static IConfiguration configuration = null;
        public static IConfiguration Instance { get 
            {
                if(configuration == null) 
                {
                    configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
                }
                return configuration;
            }
        }
    }
}
