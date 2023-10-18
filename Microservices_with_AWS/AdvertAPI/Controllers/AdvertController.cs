using AdvertAPI.Models.Messages;
using AdvertAPI.Models.Models;
using AdvertAPI.Models.Response;
using AdvertAPI.Services;
using Amazon.SimpleNotificationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Controllers
{
    // added v1 for versioning
    // if theres another version, create a new controller but with v2
    // "api/v2/[controller]"
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertStorageService advertStorageService;
        private readonly IConfiguration configuration;

        public AdvertController(IAdvertStorageService advertStorageService, IConfiguration configuration)
        {
            this.advertStorageService = advertStorageService;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(CreateAdvertResponse),201)]
        public async Task<IActionResult> Create(Advert model)
        {
            try
            {
                var recordId = await advertStorageService.Add(model);
                return StatusCode(201, new CreateAdvertResponse { Id = recordId }); 
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvert model)
        {
            try
            {
                // only raise message for confirmed Ad to avoid orphan data inside elasticsearch
                await advertStorageService.Confirm(model);
                await RaisedAdvertConfirmedMessage(model);
                return new OkResult();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private async Task RaisedAdvertConfirmedMessage(ConfirmAdvert model)
        {
            var topicArn = configuration.GetSection("AWSServices").GetValue<string>("SNSTopicARN");
            //var dbModel = await advertStorageService.GetById(model.Id);
            using(var client = new AmazonSimpleNotificationServiceClient())
            {
                var message = new AdvertConfirmedMessage
                {
                    Id = model.Id,
                    Title = ""
                };
                var messageJson = JsonConvert.SerializeObject(message);
                await client.PublishAsync(topicArn, messageJson);
            }
        }
    }
}
