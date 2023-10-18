using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.SearchAPI.Messages;

namespace WebAdvert.SearchAPI.Extensions
{
    public static class AddNestConfigurationExtensions
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticSearchURL = configuration.GetSection("ElasticSearch").GetValue<string>("URL");
            var connectionSettings = new ConnectionSettings(new Uri(elasticSearchURL))
                .DefaultIndex("adverts")
                .DefaultMappingFor<AdvertType>(advert => advert.IdProperty(p => p.Id));
            var client = new ElasticClient(connectionSettings);
            services.AddSingleton<IElasticClient>(client);
        }
    }
}
