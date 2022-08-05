using System;
using System.Collections.Generic;
using System.Linq;
using CommandService.Models;

namespace CommandService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private AppDbContext dbContext;

        public CommandRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void CreateCommand(int platformId, Command command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            command.PlatformId = platformId;
            dbContext.Commands.Add(command);
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null) throw new ArgumentNullException(nameof(platform));

            dbContext.Platforms.Add(platform);
        }

        public bool ExternalPlatformExists(int platformId)
        {
            return dbContext.Platforms.Any(p => p.Id == platformId);
        }

        public IEnumerable<Command> GetAllCommandsForPlatform(int platformId)
        {
            return dbContext.Commands.Where(c => c.PlatformId == platformId).OrderBy(c => c.Platform.Name);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return dbContext.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return dbContext.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        }

        public bool PlatformExists(int platformId)
        {
            return this.dbContext.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return (dbContext.SaveChanges() >= 0);
        }
    }
}