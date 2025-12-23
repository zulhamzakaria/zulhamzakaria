using InterviewSystem.Application.Candidates.CreateCandidate;

namespace InterviewSystem.Application.Services;

public class CandidateService
{
    private readonly Handler _handler;
    public CandidateService(Handler handler)
    {
        _handler = handler;
    }

    public void Test(Command cmd)
    {
        var result = _handler.HandleAsync(cmd);
    }
}
