using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using System;
using PlatformService.Models;
using PlatformService.SyncDataService.Http;
using System.Threading.Tasks;
using PlatformService.AsyncDataServices;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo repo;
        private readonly IMapper mapper;
        private readonly ICommandDataClient commandDataClient;
        private readonly IMessageBusClient messageBusClient;

        public PlatformsController(IPlatformRepo repo,
                                    IMapper mapper,
                                    ICommandDataClient commandDataClient,
                                    IMessageBusClient messageBusClient)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.commandDataClient = commandDataClient;
            this.messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetAllPlatforms()
        {
            Console.WriteLine("Getting platforms ...");

            var platformItem = repo.GetAllPlatforms();

            return Ok(mapper.Map<IEnumerable<PlatformReadDTO>>(platformItem));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDTO> GetPlatformById(int id)
        {
            Console.WriteLine("Getting platform...");
            var platform = repo.GetPlatformById(id);
            if (platform != null)
                return Ok(mapper.Map<PlatformReadDTO>(platform));
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDTO>> CreatePlatform(PlatformCreateDTO platformCreateDTO)
        {
            var platformModel = mapper.Map<Platform>(platformCreateDTO);
            repo.CreatePlatform(platformModel);
            repo.SaveChanges();

            var platformReadDTO = mapper.Map<PlatformReadDTO>(platformModel);
            //Send sync message
            try
            {
                await commandDataClient.SendPlatformToCommand(platformReadDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> could not send synchronously: {ex.Message}");
            }

            //send async message
            try
            {
                var platformPublishedDTO = mapper.Map<PlatformPublishedDTO>(platformReadDTO);
                platformPublishedDTO.Event = "Platform_Published";
                messageBusClient.PublishNewPlatform(platformPublishedDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatformById), new { id = platformReadDTO.Id, platformReadDTO });
            //return Ok();
        }
    }
}