using System;

namespace Play.Inventory.Service.DTOs
{
    public record GrantItemsDTO(Guid userId, Guid catalogueItemId, int quantity);
    public record InventoryItemDTO(Guid catalogueItemId, string name, string description, int quantity, DateTimeOffset acquiredDate);
    public record CatalogueItemDTO(Guid id, string name, string description);

}