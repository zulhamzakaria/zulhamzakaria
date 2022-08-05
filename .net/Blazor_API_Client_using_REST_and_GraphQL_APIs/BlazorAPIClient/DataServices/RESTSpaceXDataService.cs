using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorAPIClient.DTO;

namespace BlazorAPIClient.DataServices
{
    public class RESTSpaceXDataService : ISpaceXDataService
    {
        private readonly HttpClient httpClient;
        public RESTSpaceXDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<LaunchDTO[]> GetAllLaunches()
        {
            return await httpClient.GetFromJsonAsync<LaunchDTO[]>("/rest/launches/");
        }
    }
}