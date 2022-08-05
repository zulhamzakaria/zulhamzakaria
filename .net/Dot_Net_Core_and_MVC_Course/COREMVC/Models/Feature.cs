using System.ComponentModel.DataAnnotations;

namespace COREMVC.Models
{
    public class Feature
    {
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
    }
}