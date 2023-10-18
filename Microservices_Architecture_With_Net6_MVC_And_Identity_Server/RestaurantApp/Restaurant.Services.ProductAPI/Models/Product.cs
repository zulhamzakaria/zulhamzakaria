﻿using System.ComponentModel.DataAnnotations;

namespace Restaurant.Services.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,1000)]
        public double Price { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }

    }
}
