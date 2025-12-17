using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewSystem.Infrastructure.Repositories;

public class InterviewRoundRepository : IInterviewRoundRepository
{
    private readonly AppDbContext _context;
    public InterviewRoundRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(InterviewRound round)
    {
        await _context.AddAsync(round);
    }

    public async Task<IReadOnlyCollection<InterviewRound>> GetAllAsync()
    {
        return await _context.InterviewRounds
            .Include(ir => ir.Items)
            .ToListAsync();
    }

    public async Task<InterviewRound?> GetByIdAsync(Guid id)
    {
        return await _context.InterviewRounds
            .Include(ir => ir.Items)
            .FirstOrDefaultAsync(ir => ir.Id == id);
    }
}
