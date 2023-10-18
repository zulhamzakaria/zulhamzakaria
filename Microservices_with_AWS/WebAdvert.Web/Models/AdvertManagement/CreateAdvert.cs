﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.Models.AdvertManagement
{
    public class CreateAdvert
    {
        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; }
        public string  Description { get; set; }

        [Required(ErrorMessage = "Price Required")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}
