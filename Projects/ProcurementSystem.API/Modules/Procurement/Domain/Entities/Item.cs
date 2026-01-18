using ProcurementSystem.API.SharedKernel;
using ProcurementSystem.API.SharedKernel.ErrorHandling;
using ProcurementSystem.API.SharedKernel.ErrorHandling.Errors;

namespace ProcurementSystem.API.Modules.Procurement.Domain.Entities;

public sealed class Item : BaseEntity
{
    private const int NameMinLength = 3;
    private const int NameMaxLength = 100;
    private const int SKUMinLength = 5;
    private const int SKUMaxLength = 100;
    public Guid SupplierId { get; private set; }
    public string Name { get; private set; }
    public string SKU { get; private set; }
    public Money UnitPrice { get; private set; }

    private Item()
    {
        //EF Core
    }

    public static Result<Item> Create(Guid supplierId, string name, string sku,
        Money unitPrice, Guid createdBy)
    {
        List<Error> errors = new();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(CommonErrors.Required(nameof(name)));
        else if (name.Length < NameMinLength || name.Length > NameMaxLength)
            errors.Add(CommonErrors.InvalidLength(nameof(name), NameMinLength, NameMaxLength));
        if (string.IsNullOrWhiteSpace(sku))
            errors.Add(CommonErrors.Required(nameof(sku))); 
        else if (sku.Length < SKUMinLength || sku.Length > SKUMaxLength)
            errors.Add(CommonErrors.InvalidLength(nameof(sku), SKUMinLength, SKUMaxLength));
        if (createdBy == Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(createdBy)));
        if(supplierId == Guid.Empty)
            errors.Add(CommonErrors.Required(nameof(supplierId)));
        if(unitPrice == null)
            errors.Add(CommonErrors.Required(nameof(unitPrice)));
        else if (unitPrice.Amount < 0)
            errors.Add(CommonErrors.NegativeAmount());

        if (errors.Count > 0)
            return Result<Item>.Failure(errors);

        var item = new Item()
        {
            SupplierId = supplierId,
            Name = name,
            SKU = sku,
            UnitPrice = unitPrice!,
            CreatedBy = createdBy
        };
        return Result<Item>.Success(item);
    }

}
