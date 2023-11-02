using Domain.Models;

namespace Application.Abstractions.Interfaces;

public interface IResponsibilityRepository
{
    Task<ICollection<Responsibility>> GetResponsibilities();
    Task<ICollection<Responsibility>> GetResponsibilityByExperienceId(int experienceId);
    Task<ICollection<Responsibility>> InsertResponsibility(List<Responsibility> responsibilities);
}
