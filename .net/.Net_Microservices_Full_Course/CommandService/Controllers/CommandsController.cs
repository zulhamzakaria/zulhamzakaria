using System;
using System.Collections.Generic;
using AutoMapper;
using CommandService.Data;
using CommandService.DTOs;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [ApiController]
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository repository;
        private readonly IMapper mapper;

        public CommandsController(ICommandRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");
            if (!repository.PlatformExists(platformId))
            {
                return NotFound();
            }
            var commands = repository.GetAllCommandsForPlatform(platformId);

            return Ok(mapper.Map<IEnumerable<CommandReadDTO>>(commands));

        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDTO> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} / {commandId}");
            if (!repository.PlatformExists(platformId)) return NotFound();

            var command = repository.GetCommand(platformId, commandId);

            if (command == null) return NotFound();

            return Ok(mapper.Map<CommandReadDTO>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommandForPlatform(int platformId, CommandCreateDTO commandCreateDTO)
        {
            if (!repository.PlatformExists(platformId)) return NotFound();
            var command = mapper.Map<Command>(commandCreateDTO);
            repository.CreateCommand(platformId, command);
            repository.SaveChanges();

            var commandReadDTO = mapper.Map<CommandReadDTO>(command);

            return CreatedAtRoute(nameof(GetCommandForPlatform),
            new {platformId = platformId, commandId = commandReadDTO.Id, commandReadDTO});
        }
    }
}