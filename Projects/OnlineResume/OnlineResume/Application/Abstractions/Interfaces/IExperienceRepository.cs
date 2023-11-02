using Domain.Models;

namespace Application.Abstractions.Interfaces;

public interface IExperienceRepository
{
    Task<ICollection<Experience>> GetExperiences();
    Task<ICollection<Experience>> InsertExperiences(List<Experience> experiences);  
    Task<ICollection<Experience>> UpdateExperiences(List<Experience> experiences);
}
