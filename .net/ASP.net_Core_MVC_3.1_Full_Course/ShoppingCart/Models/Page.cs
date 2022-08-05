using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Page
    {
        public int Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        [StringLength(250)]
        public string Title { get; set; }
        [StringLength(250)]
        public string  Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        [StringLength(250)]
        public string Content { get; set; }
        public int Sorting { get; set; }
    }
}
