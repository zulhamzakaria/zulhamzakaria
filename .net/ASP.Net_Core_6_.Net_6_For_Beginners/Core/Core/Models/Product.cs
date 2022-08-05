using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class Product
    {
        public long Id { get; set; }
        // Data annotation inside [] is for validation
        [Required(ErrorMessage ="Please enter a Name")]
        // [BindNever] -> Restrict property from be bound i.e returns Null
        //[BindNever]
        public string Name { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Please eenter a value")]
        public decimal Price { get; set; }

        [Required, Range(1, long.MaxValue, ErrorMessage = "Missing Value")]
        public long CategoryId { get; set; }

        //// This is to modify the return result
        //// Commented so as to use the rule defined inside Program.cs
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Category Category { get; set; }
    }
}
