using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAdvert.Web.Messages;
using WebAdvert.Web.ServiceClients.Interface;

namespace WebAdvert.Web.ServiceClients
{
    public class SearchAPIClient : ISearchAPIClient
    {
        private readonly HttpClient httpClient;
        private readonly string BaseAddress = string.Empty;

        public SearchAPIClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            BaseAddress = configuration.GetSection("SearchAPI").GetValue<string>("URL");
        }
        public async Task<List<AdvertType>> Search(string keyword)
        {
            var result = new List<AdvertType>();
            var callUrl = $"{BaseAddress}/search/v1/{keyword.ToLower()}";
            var httpResponse = await httpClient.GetAsync(new Uri(callUrl)).ConfigureAwait(false);

            if(httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var allAdverts = await httpResponse.Content.ReadAsAsync<List<AdvertType>>().ConfigureAwait(false);
                result.AddRange(allAdverts);
            }

            return result;
        }
    }
}
