using ProcurementSystem.API.Modules.Procurement.Domain.Entities;
using ProcurementSystem.API.SharedKernel;
using ProcurementSystem.API.SharedKernel.Enums;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

namespace ProcurementSystem.API.Modules.Procurement.Domain.Aggregates;

public class PurchaseRequest : BaseEntity
{
    private const int MinPurposeLength = 5;
    private const int MaxPurposeLength = 250;
    public Guid TenantId { get; private set; }
    public string PurchaseRequestNumber { get; private set; }
    public Guid RequesterId { get; private set; }
    public string RequesterName { get; private set; }
    public Department Department { get; private set; }
    public DateTimeOffset RequestDate { get; private set; }
    public string Purpose { get; private set; }
    public DateTimeOffset RequiredDate { get; private set; }
    public PurchaseRequestStatus Status { get; private set; }

    private readonly List<PurchaseRequestItem> _items;
    public IReadOnlyCollection<PurchaseRequestItem> Items => _items;
    private PurchaseRequest()
    {
        //EF Core
    }

    public static Result<PurchaseRequest> Create(Guid tenantId, string purchaseRequestNumber,
        Guid requesterId, string requesterName, Department department, DateTimeOffset requestDate,
        string purpose, DateTimeOffset requiredDate, Guid createdBy)
    {
        List<Error> errors = new();

        if (tenantId == Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(tenantId)));
        if (string.IsNullOrWhiteSpace(purchaseRequestNumber))
            errors.Add(CommonErrors.Required(nameof(purchaseRequestNumber)));
        if (requesterId == Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(requesterId)));
        if (string.IsNullOrWhiteSpace(requesterName))
            errors.Add(CommonErrors.Required(nameof(requesterName)));
        if (Enum.IsDefined(department) is false)
            errors.Add(CommonErrors.InvalidInput(nameof(department)));
        if (requestDate == default)
            errors.Add(CommonErrors.Required(nameof(requestDate)));
        if (string.IsNullOrWhiteSpace(purpose))
            errors.Add(CommonErrors.Required(nameof(purpose)));
        else if (purpose.Length < MinPurposeLength || purpose.Length > MaxPurposeLength)
            errors.Add(CommonErrors.InvalidLength(nameof(purpose), MinPurposeLength, MaxPurposeLength));
        if (requiredDate == default)
            errors.Add(CommonErrors.Required(nameof(requiredDate)));
        if (createdBy == Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(createdBy)));

        if (errors.Any())
            return Result<PurchaseRequest>.Failure(errors);

        // Validation logic here...
        var pr = new PurchaseRequest
        {
            TenantId = tenantId,
            PurchaseRequestNumber = purchaseRequestNumber,
            RequesterId = requesterId,
            RequesterName = requesterName,
            Department = department,
            RequestDate = requestDate,
            Purpose = purpose,
            RequiredDate = requiredDate,
            Status = PurchaseRequestStatus.Draft,
            CreatedBy = createdBy
        };

        return Result<PurchaseRequest>.Success(pr);
    }

    public static Result<PurchaseRequestItem> AddItem(PurchaseRequestItem item)
    {
        var result = PurchaseRequestItem.Create(
            prItemId: item.PRItemId,
            description: item.Description,
            quantity: item.Quantity,
            uom: item.UOM,
            estimatedUnitPrice: item.EstimatedUnitPrice,
            createdBy: item.CreatedBy
            );

        if (result.IsFailure)
            return Result<PurchaseRequestItem>.Failure(result.Errors);

        return Result<PurchaseRequestItem>.Success(result.Value!);
    }

}
