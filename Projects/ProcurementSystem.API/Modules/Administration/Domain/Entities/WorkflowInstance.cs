using ProcurementSystem.API.SharedKernel;
using ProcurementSystem.API.SharedKernel.Enums;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

namespace ProcurementSystem.API.Modules.Administration.Domain.Entities;

public sealed class WorkflowInstance : BaseEntity
{
    public Guid ProcurementId { get; private set; }
    public ProcurementType ProcurementType { get; private set; }
    public Department Department { get; private set; }
    public WorkflowInstanceStatus Status { get; private set; }

    private readonly List<WorkflowStep> _steps;
    public IReadOnlyCollection<WorkflowStep> Steps => _steps;

    public WorkflowInstance()
    {
        //EF Core
    }

    public Result<WorkflowInstance> Create(Guid procurementId, ProcurementType procurementType,
        Department department, WorkflowInstanceStatus status, Guid createdBy,
        IReadOnlyList<WorkflowStep> steps)
    {
        List<Error> errors = new();
        if (procurementId == Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(procurementId)));
        if (Enum.IsDefined(procurementType) is false)
            errors.Add(CommonErrors.InvalidInput(nameof(procurementType)));
        if (Enum.IsDefined(department) is false)
            errors.Add(CommonErrors.InvalidInput(nameof(department)));
        if (Enum.IsDefined(status) is false)
            errors.Add(CommonErrors.InvalidInput(nameof(status)));
        if (createdBy == Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(createdBy)));

        var workflowInstance = new WorkflowInstance
        {
            ProcurementId = procurementId,
            ProcurementType = procurementType,
            Department = department,
            Status = status,
            CreatedBy = createdBy,
            CreatedAt = DateTimeOffset.UtcNow
        };

        foreach (var step in steps)
        {
            var resultStep = WorkflowStep.Create(
                step.Position,
                step.Sequence,
                step.IsMandatory,
                step.CanCompleteProcess);

            if (resultStep.IsFailure)
            {
                errors.AddRange(resultStep.Errors);
                continue;
            }
            workflowInstance._steps.Add(resultStep.Value!);
        }

        if (errors.Any())
            return Result<WorkflowInstance>.Failure(errors);

        return Result<WorkflowInstance>.Success(workflowInstance);
    }
}
