using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourseSite.Entities
{
    public class CategoryItem
    {
        private DateTimeOffset _releaseDate = DateTimeOffset.MinValue;
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTimeOffset DateTimeReleased {
            get 
            {
                return (_releaseDate == DateTimeOffset.MinValue) ? DateTimeOffset.UtcNow : _releaseDate;
            }
            set
            {
                _releaseDate = value;
            }
        }
        public int MediaTypeId { get; set; }
        [NotMapped]
        public virtual ICollection<SelectListItem> MediaTypes { get; set; }
        public int CategoryId { get; set; }
    }
}
