namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IUnitOfWorkRepository
{
    Task SaveChangesAsync();
}
