using InterviewSystem.Application.Candidates.CreateCandidate;

namespace InterviewSystem.Application.Services;

public class CandidateService
{
    private readonly CreateCandidateHandler _handler;
    public CandidateService(CreateCandidateHandler handler)
    {
        _handler = handler;
    }

    public void Test(CreateCandidateCommand cmd)
    {
        var result = _handler.HandleAsync(cmd);
    }
}
