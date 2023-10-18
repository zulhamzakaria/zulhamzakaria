using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.SearchAPI.Messages;

namespace WebAdvert.SearchAPI.Services
{
    public class SearchService : ISearchService
    {
        private readonly IElasticClient elasticClient;

        public SearchService(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }
        public async Task<List<AdvertType>> Search(string keyword)
        {

            // returns any title with the provided keyword
            var response = await elasticClient.SearchAsync<AdvertType>(search => search
                            .Query(q => q.Term(field => field.Title, keyword.ToLower()))
                            );
            return response.Hits.Select(h => h.Source).ToList();
        }
    }
}
