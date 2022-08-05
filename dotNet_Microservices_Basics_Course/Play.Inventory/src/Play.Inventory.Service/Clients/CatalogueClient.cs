using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Play.Inventory.Service.DTOs;

namespace Play.Inventory.Service.Clients
{
    public class CatalogueClient
    {
        private readonly HttpClient httpClient;

        public CatalogueClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<CatalogueItemDTO>> GetCatalogueItemsAsync()
        {
            var item = await httpClient.GetFromJsonAsync<IReadOnlyCollection<CatalogueItemDTO>>("/items");
            return item;
        }
    }
}