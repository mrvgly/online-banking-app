using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using GetirCase.Core;
using GetirCase.Core.Models;
using GetirCase.Core.Services;
using GetirCase.Core.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GetirCase.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public CustomerService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            customer.Password = Helper.PasswordHasher(customer.Password);

            await _unitOfWork.Customers
                             .AddAsync(customer);

            await _unitOfWork.CommitAsync();

            return customer;
        }

        public Token CreateToken(Login login)
        {
            var token = new Token
            {
                Expiration = DateTime.Now.AddMinutes(15)
            };

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
           
            JwtSecurityToken securityToken = new(
                issuer: _configuration["Tokens:Issuer"],
                audience: _configuration["Tokens:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new();

            token.AccessToken = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public async Task<Customer> GetCustomerWithAccountsById(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);

            var accounts = await _unitOfWork.Accounts.GetAllAccountsByCustomerIdAsync(customer.Id);

            customer.Accounts = accounts;

            return customer;
        }

        public async Task<Customer> GetCustomerByLoginRequest(Login login)
        {
            login.Password = Helper.PasswordHasher(login.Password);

            return await _unitOfWork.Customers.GetCustomerByLoginRequestAsync(login);
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _unitOfWork.Customers.GetByIdAsync(id);
        }

        public async Task<Customer> GetCustomerByCustomerName(string customerName)
        {
            return await _unitOfWork.Customers.GetCustomerByNameAsync(customerName);
        }
    }
}
