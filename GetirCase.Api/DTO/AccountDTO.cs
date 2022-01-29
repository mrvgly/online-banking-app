namespace GetirCase.Api.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public CustomerDTO Customer { get; set; }
    }
}
