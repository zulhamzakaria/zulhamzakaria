using InterviewSystem.Domain.Common.Enums;

namespace InterviewSystem.Domain.Entity;

public class InterviewRound : EntityBase
{
    public Guid Id { get; private set; }
    public EmployeeDepartment Department { get; private set; }
    public int NumberOfRounds { get; private set; } //get number of InterviewRoundItems

    private readonly List<InterviewRoundItem> _items = new();
    public IReadOnlyList<InterviewRoundItem> Items => _items.AsReadOnly();

    private InterviewRound()
    {
        //EF Core needs this
    }

    private InterviewRound(Guid id, EmployeeDepartment department, List<InterviewRoundItem> items)
    {
        Id = id;
        Department = department;
        _items = items.OrderBy(i => i.Sequence).ToList();
        NumberOfRounds = items.Count;
    }

}
