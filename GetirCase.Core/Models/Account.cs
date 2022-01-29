using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetirCase.Core.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public Customer Customer { get; set; }

        [NotMapped]
        public int CustomerId { get; set; }
    }
}
