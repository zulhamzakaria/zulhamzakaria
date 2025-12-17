using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace InterviewSystem.Infrastructure.Repositories;

public class InterviewProcessRepository : IInterviewProcessRepository
{
    private readonly AppDbContext _context;
    public InterviewProcessRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(InterviewProcess process)
    {
        await _context.InterviewProcesses.AddAsync(process);
    }

    public async Task<IReadOnlyCollection<InterviewProcess>> GetAllAsync()
    {
        return await _context.InterviewProcesses.ToListAsync(); 
    }

    public async Task<InterviewProcess?> GetByIdAsync(Guid id)
    {
        return await _context.InterviewProcesses.FindAsync(id);
    }
}
