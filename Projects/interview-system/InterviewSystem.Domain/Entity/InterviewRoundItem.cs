using InterviewSystem.Domain.Common.Enum;

namespace InterviewSystem.Domain.Entity;

public class InterviewRoundItem
{
    public int Sequence { get; private set; }
    public EmployeePosition Position { get; private set; }

    private InterviewRoundItem()
    {
        //EF Core needs this
    }

    private InterviewRoundItem(int sequence, EmployeePosition employee)
    {
        Sequence = sequence;
        Position = employee;
    }
}
