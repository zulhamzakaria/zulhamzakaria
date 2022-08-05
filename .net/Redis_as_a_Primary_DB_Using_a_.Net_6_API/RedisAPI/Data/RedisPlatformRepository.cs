using System.Text.Json;
using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data
{
    public class RedisPlatformRepository : IPlatformRepository
    {
        private readonly IConnectionMultiplexer connectionMultiplexer;
        public RedisPlatformRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            this.connectionMultiplexer = connectionMultiplexer;
        }
        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
                throw new ArgumentOutOfRangeException(nameof(platform));

            var db = connectionMultiplexer.GetDatabase();
            var serialPlatform = JsonSerializer.Serialize(platform);
            // db.StringSet(platform.Id, serialPlatform);
            // db.SetAdd("PlatformsSet", serialPlatform);

            db.HashSet("hashplatform", new HashEntry[] { new HashEntry(platform.Id, serialPlatform) });

        }

        public IEnumerable<Platform?>? GetAllPlatforms()
        {
            var db = connectionMultiplexer.GetDatabase();
            //var result = db.SetMembers("PlatformsSet");
            var result = db.HashGetAll("hashplatform");

            if (result.Length > 0)
            {
                var obj = Array.ConvertAll(result, val => JsonSerializer.Deserialize<Platform>(val.Value)).ToList();
                return obj;
            }
            return null;
        }

        public Platform? GetPlatformById(string id)
        {
            var db = connectionMultiplexer.GetDatabase();
            //var platform = db.StringGet(id);
            var platform = db.HashGet("hashplatform", id);
            if (!string.IsNullOrEmpty(platform))
            {
                return JsonSerializer.Deserialize<Platform>(platform);
            }
            return null;
        }
    }
}