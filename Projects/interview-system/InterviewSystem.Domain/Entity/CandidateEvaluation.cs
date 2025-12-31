namespace InterviewSystem.Domain.Entity;

public class CandidateEvaluation
{
    public bool Passed { get; private set; }
    public string? Note { get; set; }
    public CandidateEvaluation(bool passed, string? note)
    {
        Passed = passed;
        Note = note;
    }

    public static CandidateEvaluation Pass(string? note) =>new (true, note);
    
    public static CandidateEvaluation Fail(string note)
    {
        if (string.IsNullOrWhiteSpace(note))
        {
            //TODO: use Result<T>
            throw new ArgumentNullException(nameof(note));
        }
        return new(false, note);
    }
}
