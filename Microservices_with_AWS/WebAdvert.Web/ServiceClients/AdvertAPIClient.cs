using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAdvert.Web.Models;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.Models.Requests;
using WebAdvert.Web.Models.Response;
using WebAdvert.Web.ServiceClients.Interface;

namespace WebAdvert.Web.ServiceClients
{
    public class AdvertAPIClient : IAdvertAPIClient
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;
        private readonly IMapper mapper;

        public AdvertAPIClient(IConfiguration configuration, HttpClient httpClient, IMapper mapper)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
            this.mapper = mapper;

            // configure HttpClient
            var createUrl = configuration.GetSection("AdvertAPI").GetValue<string>("BaseURL");
            httpClient.BaseAddress = new Uri($"{createUrl}/create/");
            httpClient.DefaultRequestHeaders.Add("Content-type", "application/json");

        }

        public async Task<bool> Confirm(ConfirmAdvert model)
        {
            var advertModel = mapper.Map<ConfirmAdvertRequest>(model);
            var jsonModel = JsonConvert.SerializeObject(advertModel);
            var response = await httpClient.PutAsync(new Uri($"{httpClient.BaseAddress}/confirm"), new StringContent(jsonModel))
                .ConfigureAwait(false);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<AdvertResponse> Create(AdvertAPI model)
        {
            var advertAPIModel = mapper.Map<CreateAdvert>(model);
            var result = JsonConvert.SerializeObject(advertAPIModel);
            var response = await httpClient.PostAsync(httpClient.BaseAddress, new StringContent(result))
                .ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var createAdvertReponse = JsonConvert.DeserializeObject<CreateAdvertResponse>(responseJson);

            var advertResponse = mapper.Map<AdvertResponse>(createAdvertReponse);

            return advertResponse;
        }

        public async Task<List<Advertisement>> GetAllAsync()
        {
            var apiCallResponse = await httpClient.GetAsync(new Uri($"{httpClient.BaseAddress}/all")).ConfigureAwait(false);
            var allAdvertModels = await apiCallResponse.Content.ReadAsAsync<List<AdvertAPI>>().ConfigureAwait(false);

            return allAdvertModels.Select(x => mapper.Map<Advertisement>(x)).ToList();
        }
    }
}
