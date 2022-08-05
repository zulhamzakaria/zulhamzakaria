using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;
using RedisAPI.Models;

namespace RedisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository platformRepository;

        public PlatformsController(IPlatformRepository platformRepository)
        {
            this.platformRepository = platformRepository;
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<Platform> GetPlatformById(string id)
        {
            var platform = platformRepository.GetPlatformById(id);

            if (platform != null)
                return Ok(platform);

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Platform> CreatePlatform(Platform platform)
        {
            platformRepository.CreatePlatform(platform);
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id }, platform);
        }

        public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
        {
            return Ok(platformRepository.GetAllPlatforms());
        }
    }
}