using ProcurementSystem.API.SharedKernel;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

namespace ProcurementSystem.API.Modules.Administration.Domain.Entities;

public sealed class WorkflowHistory : BaseEntity
{
    private const int CommentsMaxLength = 1000;
    public Guid WorkflowInstanceId { get; private set; }
    public Guid WorkflowStepId { get; private set; }
    public Guid ActionedBy { get; private set; }
    public DateTimeOffset ActionedAt { get; private set; }
    public string Comments { get; private set; } = string.Empty;
    public WorkflowHistory()
    {
        // EF Core
    }
    public static Result<WorkflowHistory> Create(Guid workflowInstanceId, Guid workflowStepId,
        Guid actionedBy, string comments)
    {
        List<Error> errors = new();
        if (comments.Length > CommentsMaxLength)
            errors.Add(CommonErrors.InvalidLength(nameof(comments), 0, CommentsMaxLength));
        if (workflowInstanceId != Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(workflowInstanceId)));
        if (workflowStepId != Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(workflowStepId)));
        if (actionedBy != Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(actionedBy)));

        if (errors.Any())
            return Result<WorkflowHistory>.Failure(errors);

        var wfHistory = new WorkflowHistory
        {
            Id = Guid.NewGuid(),
            WorkflowInstanceId = workflowInstanceId,
            WorkflowStepId = workflowStepId,
            ActionedBy = actionedBy,
            ActionedAt = DateTimeOffset.UtcNow,
            Comments = comments,
            CreatedBy = actionedBy,
            CreatedAt = DateTimeOffset.UtcNow
        };

        return Result<WorkflowHistory>.Success(wfHistory);
    }
}
