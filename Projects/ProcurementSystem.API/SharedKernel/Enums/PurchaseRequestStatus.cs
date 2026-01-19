namespace ProcurementSystem.API.SharedKernel.Enums;

public enum PurchaseRequestStatus
{
    Draft,
    PendingPurchaseOfficerApproval,
    PendingFinanceManagerApproval,
    Approved,
    Rejected,
    Completed,
    Cancelled
}
