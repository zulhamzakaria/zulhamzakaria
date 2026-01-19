using ProcurementSystem.API.SharedKernel.Enums;

namespace ProcurementSystem.API.Modules.Procurement.Domain.Policies;

internal sealed record ApprovalStepPolicy(
    EmployeePosition Position,
    int Sequence,
    bool IsMandatory,
    bool CanCompleteProcess);

internal sealed record ApprovalProcessPolicy(
    string ProcessName,
    IReadOnlyList<ApprovalStepPolicy> Steps);

internal static class ProcurementApprovalStepPolicy
{
    public static readonly ApprovalProcessPolicy StandardProcurementApprovalProcess = new(
        ProcessName: "Standard Procurement Approval Process",
        Steps: new List<ApprovalStepPolicy>
        {
            new(
                Position: EmployeePosition.PurchasingOfficer,
                Sequence: 1,
                IsMandatory: true,
                CanCompleteProcess: false),
            new(
                Position: EmployeePosition.FinanceManager,
                Sequence: 2,
                IsMandatory: true,
                CanCompleteProcess: true),
        });
};
