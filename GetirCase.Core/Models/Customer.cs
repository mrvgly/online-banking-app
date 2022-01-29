using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetirCase.Core.Models
{
    public class Customer
    {
        public Customer()
        {
            Accounts = new List<Account>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        [NotMapped]
        public List<Account> Accounts { get; set; }
    }
}
