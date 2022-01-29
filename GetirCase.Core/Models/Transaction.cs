using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetirCase.Core.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public decimal Amount { get; set; }

        [NotMapped] 
        public int AccountId { get; set; }

        public Account Account { get; set; }

        public string Note { get; set; }

        public DateTime Date { get; set; }

        public TransactionTypes Type { get; set; }
    }

    public enum TransactionTypes
    {
        Deposit,
        Withdrawing
    }
}
