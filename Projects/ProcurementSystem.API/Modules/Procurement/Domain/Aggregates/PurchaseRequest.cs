using ProcurementSystem.API.Modules.Procurement.Domain.Entities;
using ProcurementSystem.API.SharedKernel;
using ProcurementSystem.API.SharedKernel.Enums;

namespace ProcurementSystem.API.Modules.Procurement.Domain.Aggregates;

public class PurchaseRequest : BaseEntity
{
    public Guid TenantId { get; private set; }
    public string PRNumber { get; private set; }
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
}
