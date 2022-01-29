using System.Threading.Tasks;
using GetirCase.Core.Models;

namespace GetirCase.Core.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerWithAccountsById(int id);
        Task<Customer> CreateCustomer(Customer customer);
        Token CreateToken(Login login);
        Task<Customer> GetCustomerByLoginRequest(Login login);
        Task<Customer> GetCustomerById(int id);
        Task<Customer> GetCustomerByCustomerName(string customerName);
    }
}
