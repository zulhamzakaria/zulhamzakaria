using InterviewSystem.Domain.Common.ErrorHandling;
using InterviewSystem.Domain.Entity;
using InterviewSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace InterviewSystem.Application.Candidates.CreateCandidate;

public sealed class CreateCandidateHandler : IRequestHandler<CreateCandidateCommand, Result<Guid>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IUnitOfWorkRepository _uow;

    public CreateCandidateHandler(ICandidateRepository candidateRepository, IUnitOfWorkRepository uow)
    {
        _candidateRepository = candidateRepository;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        var result = Candidate.Create(request.Name, 
            request.Email,
            request.PhoneNumber,
            request.AppliedPosition);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Errors);
        var newCandidate = result.Value;

        await _candidateRepository.AddAsync(newCandidate!);
        await _uow.SaveChangesAsync();

        return Result<Guid>.Success(newCandidate!.Id);
    }
}
