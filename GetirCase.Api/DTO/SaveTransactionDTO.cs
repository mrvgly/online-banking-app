namespace GetirCase.Api.DTO
{
    public class SaveTransactionDTO
    {
        public decimal Amount { get; set; }

        public int AccountId { get; set; }

        public int CustomerId { get; set; }

        public string Note { get; set; }
    }
}
