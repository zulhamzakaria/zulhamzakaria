using Application.Abstractions.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ResponsibilityRepository : IResponsibilityRepository
{
    private readonly OnlineResumeDbContext _context;

    public ResponsibilityRepository(OnlineResumeDbContext context)
    {
        _context = context;
    }
    public async Task<ICollection<Responsibility>> GetResponsibilities()
        => await _context!.Responsibilities!.ToListAsync();


    public async Task<ICollection<Responsibility>> GetResponsibilityByExperienceId(int experienceId)
        => await _context!.Responsibilities!.Where(r => r.ExperienceId == experienceId).ToListAsync();


    public async Task<ICollection<Responsibility>> InsertResponsibility(List<Responsibility> responsibilities)
    {
        await _context!.BulkInsertAsync<Responsibility>(responsibilities);
        return responsibilities;
    }
}
