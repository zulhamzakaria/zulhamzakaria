using ProcurementSystem.API.SharedKernel.Enums;

namespace ProcurementSystem.API.Modules.Procurement.Domain.Policies;

public sealed record ApprovalStepPolicy(
    EmployeePosition Position,
    int Sequence ,
    bool IsMandatory,
    bool CanCompleteProcess);
