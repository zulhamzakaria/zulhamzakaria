using Newtonsoft.Json;
using Restaurant.Web.Models;
using Restaurant.Web.Models.DTOs;
using Restaurant.Web.Services.IServices;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Restaurant.Web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDTO responseDTO { get; set; }
        public IHttpClientFactory HttpClient { get; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseDTO = new ResponseDTO();
            HttpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("RestaurantAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.URL);
                client.DefaultRequestHeaders.Clear();
                if(apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), 
                        Encoding.UTF8, 
                        "application/json");
                }

                // to send out token for each request
                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                HttpResponseMessage apiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method =    HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method =    HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method =    HttpMethod.Delete;
                        break;
                    default:
                        message.Method =    HttpMethod.Get;
                        break;
                }
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDTO = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDTO;

            }
            catch(Exception ex)
            {
                var dto = new ResponseDTO
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false,
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDTO = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDTO;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }
}
