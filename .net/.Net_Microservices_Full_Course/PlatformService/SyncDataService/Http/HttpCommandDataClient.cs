using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.DTOs;

namespace PlatformService.SyncDataService.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient client;
        private readonly IConfiguration config;

        public HttpCommandDataClient(HttpClient client, IConfiguration config)
        {
            this.client = client;
            this.config = config;
        }
        public async Task SendPlatformToCommand(PlatformReadDTO platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json"
            );
            var response = await client.PostAsync($"{config["CommandService"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync post to commandservice was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync post to commandservice was NOT OK!");
            }

        }
    }
}