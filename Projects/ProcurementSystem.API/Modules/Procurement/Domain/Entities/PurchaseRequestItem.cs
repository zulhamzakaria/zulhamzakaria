using ProcurementSystem.API.SharedKernel;
using ProcurementSystem.API.SharedKernel.Enums;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

namespace ProcurementSystem.API.Modules.Procurement.Domain.Entities;

public class PurchaseRequestItem : BaseEntity
{
    private static readonly int DescriptionMinLength = 5;
    private static readonly int DescriptionMaxLength = 500;

    public Guid PRItemId { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }
    public UOM UOM { get; private set; }
    public Money EstimatedUnitPrice { get; private set; }
    public Money EstimatedTotal => EstimatedUnitPrice * Quantity;

    public PurchaseRequestItem()
    {
        //EF Core
    }

    public Result<PurchaseRequestItem> Create(Guid prItemId, string description,
        int quantity, UOM uom, Money estimatedUnitPrice, Guid createdBy)
    {
        List<Error> errors = new();

        if (prItemId == Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(prItemId)));
        if (string.IsNullOrWhiteSpace(description))
            errors.Add(CommonErrors.Required(nameof(description)));
        else if (description.Length < DescriptionMinLength || description.Length > DescriptionMaxLength)
            errors.Add(CommonErrors.InvalidLength(nameof(description), minLength: DescriptionMinLength, maxLength: DescriptionMaxLength));
        if (quantity <= 0)
            errors.Add(CommonErrors.InvalidInput(nameof(quantity)));
        if (estimatedUnitPrice == null)
            errors.Add(CommonErrors.Required(nameof(estimatedUnitPrice)));
        if (createdBy == Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(createdBy)));

        if (errors.Count > 0)
            return Result<PurchaseRequestItem>.Failure(errors);

        var prItem = new PurchaseRequestItem()
        {
            PRItemId = prItemId,
            Description = description,
            Quantity = quantity,
            UOM = uom,
            EstimatedUnitPrice = estimatedUnitPrice!,
            CreatedBy = createdBy
        };
        return Result<PurchaseRequestItem>.Success(prItem);
    }

}
