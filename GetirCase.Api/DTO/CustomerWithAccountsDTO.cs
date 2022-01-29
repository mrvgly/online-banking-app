using System.Collections.Generic;

namespace GetirCase.Api.DTO
{
    public class CustomerWithAccountsDTO : CustomerDTO
    {
        public List<AccountResponse> Accounts { get; set; }
    }

    public class AccountResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }
    }
}
