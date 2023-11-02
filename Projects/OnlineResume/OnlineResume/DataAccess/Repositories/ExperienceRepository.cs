using Application.Abstractions.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ExperienceRepository : IExperienceRepository
{
    private readonly OnlineResumeDbContext _dbContext;

    public ExperienceRepository(OnlineResumeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Experience>> GetExperiences()
    {
        return await _dbContext.Experiences!.ToListAsync();
    }

    public async Task<ICollection<Experience>> InsertExperiences(List<Experience> experiences)
    {
        await _dbContext.BulkInsertAsync<Experience>(experiences);
        return experiences;
    }
    
    public async Task<ICollection<Experience>> UpdateExperiences(List<Experience> experiences)
    {
        await _dbContext.BulkUpdateAsync<Experience>(experiences);
        return experiences;
    }
}
