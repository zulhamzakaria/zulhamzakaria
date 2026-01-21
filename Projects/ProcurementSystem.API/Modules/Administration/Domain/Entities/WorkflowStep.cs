using ProcurementSystem.API.SharedKernel;
using ProcurementSystem.API.SharedKernel.Enums;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

namespace ProcurementSystem.API.Modules.Administration.Domain.Entities;

public sealed class WorkflowStep : BaseEntity
{
    public Guid WorkflowInstanceId { get; private set; }
    public UserRole Position { get; private set; }
    public int Sequence { get; private set; }
    public WorkflowStepStatus Status { get; set; }
    public bool IsMandatory { get; private set; }
    public bool CanCompleteProcess { get; private set; }

    public WorkflowStep()
    {
        // EF Core
    }

    public static Result<WorkflowStep> Create(UserRole position, int sequence,
        bool isMandatory, bool canCompleteProcess)
    {
        List<Error> errors = new();
        if (sequence <= 0)
            errors.Add(CommonErrors.InvalidInput(nameof(sequence)));
        if(Enum.IsDefined(position) is false)
            errors.Add(CommonErrors.InvalidInput(nameof(position)));

        if (errors.Any())
            return Result<WorkflowStep>.Failure(errors);

        var workflowStep = new WorkflowStep
        {
            Position = position,
            Sequence = sequence,
            IsMandatory = isMandatory,
            Status = sequence == 1 ? WorkflowStepStatus.Pending : WorkflowStepStatus.Locked,
            CanCompleteProcess = canCompleteProcess
        };
        return Result<WorkflowStep>.Success(workflowStep);
    }

    public void SetStatus(WorkflowStepStatus status)
        => Status = status;
}
