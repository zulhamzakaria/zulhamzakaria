using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;

namespace InterviewSystem.Application.Candidates.CreateCandidate;

public sealed class Handler
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IUnitOfWorkRepository _uow;

    public Handler(ICandidateRepository candidateRepository, IUnitOfWorkRepository uow)
    {
        _candidateRepository = candidateRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> HandleAsync(Command command)
    {
        var result = Candidate.Create(command.Name, command.Email, command.PhoneNumber, command.AppliedPosition);
        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);
        var newCandidate = result.Value;

        await _candidateRepository.AddAsync(newCandidate!);
        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newCandidate!.Id);
    }
}
