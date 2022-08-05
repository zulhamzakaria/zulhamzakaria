using System;

namespace Play.Catalogue.Contracts
{
    public record CatalogueItemCreated(Guid itemId, string name, string desrcription);
    public record CatalogueItemUpdated(Guid itemId, string name, string description);
    public record CatalogueItemDeleted(Guid itemId);

}