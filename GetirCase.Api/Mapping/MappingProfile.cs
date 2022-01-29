using AutoMapper;
using GetirCase.Api.DTO;
using GetirCase.Core.Models;

namespace GetirCase.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();

            CreateMap<Customer, CustomerWithAccountsDTO>();

            CreateMap<Account, AccountResponse>();


            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();

            CreateMap<SaveCustomerDTO, Customer>();
            CreateMap<SaveAccountDTO, Account>();

            CreateMap<LoginDTO, Login>();

            CreateMap<Transaction, TransactionDTO>();
            CreateMap<TransactionDTO, Transaction>();

            CreateMap<SaveTransactionDTO, Transaction>();
            CreateMap<Transaction, SaveTransactionDTO>();
        }
    }
}
