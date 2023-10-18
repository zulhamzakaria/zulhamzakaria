using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using WebAdvert.Search.Worker.Messages;

namespace WebAdvert.Search.Worker.Helpers
{
    // handles creation of elasticsearch instances
    // needed since DI is not usable
    public static class ElasticSearchHelper
    {
        public static IElasticClient client;
        public static IElasticClient GetInstance(IConfiguration configuration)
        {
            if(client == null)
            {
                var url = configuration.GetSection("ElasticSearch").GetValue<string>("url");
                var settings = new ConnectionSettings(new Uri(url)).DefaultIndex("adverts")
                    .DefaultMappingFor<AdvertType>(m => m.IdProperty(x => x.Id));
                client = new ElasticClient(settings);
            }
            return client;
        }
    }
}
