using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using GetirCase.Api.DTO;
using GetirCase.Api.Validators;
using GetirCase.Core.Models;
using GetirCase.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetirCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;

        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody][Required] SaveCustomerDTO saveCustomerDTO)
        {
            var validator = new SaveCustomerDTOValidator();
            var validationResult = await validator.ValidateAsync(saveCustomerDTO);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var customerToCreate = _mapper.Map<SaveCustomerDTO, Customer>(saveCustomerDTO);

            var newCustomer = await _customerService.CreateCustomer(customerToCreate);

            var customer = await _customerService.GetCustomerById(newCustomer.Id);

            var customerDTO = _mapper.Map<Customer, CustomerDTO>(customer);

            return Ok(customerDTO);
        }

        [HttpGet("Detail/{id}")]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<CustomerWithAccountsDTO>> GetCustomerWithAccountsById(int id)
        {
            var customer = await _customerService.GetCustomerWithAccountsById(id);
            var customerWithAccountsDTO = _mapper.Map<Customer, CustomerWithAccountsDTO>(customer);

            return Ok(customerWithAccountsDTO);
        }

        [HttpPost("Login")]
        [Produces("application/json")]
        public async Task<ActionResult<Token>> Login([FromForm] LoginDTO loginDTO)
        {
            var login = _mapper.Map<LoginDTO, Login>(loginDTO);

            var customer = await _customerService.GetCustomerByLoginRequest(login);

            if (customer != null)
            {
                var token = _customerService.CreateToken(login);

                return Ok(token);
            }

            return BadRequest();
        }
    }
}
