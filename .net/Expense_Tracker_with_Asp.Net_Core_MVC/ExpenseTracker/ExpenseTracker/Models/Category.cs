using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(50)")]
        public string Icon { get; set; } = "no icon";
        [Column(TypeName = "nvarchar(50)")]
        public string CategoryType { get; set; } = "Expense";
    }
}
