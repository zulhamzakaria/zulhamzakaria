namespace ProcurementSystem.API.SharedKernel.Enums;

public enum WorkflowInstanceStatus
{
    Running,
    Completed,
    Cancelled
}

public enum WorkflowStepStatus
{
    Locked,
    Pending,
    Approved,
    Rejected
}
