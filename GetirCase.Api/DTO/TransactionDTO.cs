using System;

namespace GetirCase.Api.DTO
{
    public class TransactionDTO : SaveTransactionDTO
    {
        public string Type { get; set; }

        public DateTime Date { get; set; }
    }
}
