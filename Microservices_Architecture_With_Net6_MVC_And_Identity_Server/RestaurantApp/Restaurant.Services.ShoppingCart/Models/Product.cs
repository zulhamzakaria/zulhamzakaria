// Shopping cart keeeps a copy of Product since accessing
// the Product API directly is time-consuming

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Services.ShoppingCart.Models
{
    public class Product
    {
        // field is still a primary key but the key is user-defined
        // this is to allow the column to store the original ProductId instead of database generated Id
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 1000)]
        public double Price { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }

    }
}
