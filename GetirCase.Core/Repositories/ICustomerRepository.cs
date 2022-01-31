using System.Threading.Tasks;
using GetirCase.Core.Models;

namespace GetirCase.Core.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByLoginRequestAsync(Login login);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<Customer> GetCustomerByNameAsync(string customerName);
    }
}
