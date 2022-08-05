using System.ComponentModel.DataAnnotations;

namespace Catalogue.DTO
{
    public class CreateItemDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(0, 1000)]
        public decimal Price { get; init; }
    }
}