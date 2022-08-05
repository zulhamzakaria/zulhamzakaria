using System;
using System.Collections.Generic;
using AutoMapper;
using CommandService.Data;
using CommandService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [ApiController]
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepository commandRepository;
        private readonly IMapper mapper;

        public PlatformsController(ICommandRepository commandRepository, IMapper mapper)
        {
            this.commandRepository = commandRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms...");
            var platforms = commandRepository.GetAllPlatforms();
            return Ok(mapper.Map<IEnumerable<PlatformReadDTO>>(platforms));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound post # command service");
            return Ok("Inbound test from Platforms Controller");
        }
    }
}