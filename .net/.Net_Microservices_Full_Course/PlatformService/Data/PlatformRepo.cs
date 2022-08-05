using System;
using System.Collections.Generic;
using System.Linq;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext context;

        public PlatformRepo(AppDbContext context)
        {
            this.context = context;
        }
        public void CreatePlatform(Platform platform)
        {
            if (platform == null) throw new ArgumentNullException(nameof(platform));
            context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return context.Platforms.FirstOrDefault(platform => platform.Id == id);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }
    }
}