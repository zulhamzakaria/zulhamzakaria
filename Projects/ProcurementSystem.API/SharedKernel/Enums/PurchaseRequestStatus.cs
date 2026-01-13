namespace ProcurementSystem.API.SharedKernel.Enums;

public enum PurchaseRequestStatus
{
    Draft,
    PendingPOApproval,
    PendingFMApproval,
    Approved,
    Rejected,
    Completed,
    Cancelled
}
