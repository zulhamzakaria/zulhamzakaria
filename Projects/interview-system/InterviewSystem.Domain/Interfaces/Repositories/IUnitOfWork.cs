namespace InterviewSystem.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
