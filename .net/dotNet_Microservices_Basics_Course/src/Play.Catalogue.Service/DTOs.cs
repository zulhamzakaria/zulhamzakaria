using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalogue.Service.DTOS
{
    public record ItemDTO(Guid id, string name, string description, decimal price, DateTimeOffset createdDate);

    public record CreateItemDTO([Required] string name, string description, [Range(1, 1000)] decimal price);

    public record UpdateItemDTO([Required] string name, string description, [Range(1,1000)] decimal price);
}