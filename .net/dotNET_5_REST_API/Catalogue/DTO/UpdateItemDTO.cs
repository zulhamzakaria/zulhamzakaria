using System.ComponentModel.DataAnnotations;

namespace Catalogue.DTO
{
    public record UpdateItemDTO
    {
        [Required]
        public string Name { get; init; }

        [Required]
        [Range(0, 1000)]
        public decimal Price { get; set; }
    }
}