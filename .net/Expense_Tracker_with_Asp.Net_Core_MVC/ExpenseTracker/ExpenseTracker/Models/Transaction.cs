using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransationId { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.Now;


        // By default, EF applies cascading delete to the data
        public int CategoryId { get; set; }
        public Category? Category { get; set; }


    }
}
