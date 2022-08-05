using System;
using System.Collections.Generic;
using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms() => throw new NotImplementedException();
        Platform GetPlatformById(int id) =>
            throw new NotImplementedException();
        void CreatePlatform(Platform platform) =>
            throw new NotImplementedException();

    }
}