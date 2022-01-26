using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
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

        [HttpGet]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            var customerDTOs = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customers);

            return Ok(customerDTOs);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<CustomerDTO>> GetACustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            var customerDTO = _mapper.Map<Customer, CustomerDTO>(customer);

            return Ok(customerDTO);
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize]
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

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<CustomerDTO>> UpdateCustomer(int id, [FromBody][Required] SaveCustomerDTO saveCustomerDTO)
        {
            var validator = new SaveCustomerDTOValidator();
            var validationResult = await validator.ValidateAsync(saveCustomerDTO);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var customerToBeUpdated = await _customerService.GetCustomerById(id);

            if (customerToBeUpdated == null)
                return NotFound();

            var customer = _mapper.Map<SaveCustomerDTO, Customer>(saveCustomerDTO);

            await _customerService.UpdateCustomer(customerToBeUpdated, customer);

            var updatedCustomer = await _customerService.GetCustomerById(id);

            var updatedCustomerDTO = _mapper.Map<Customer, CustomerDTO>(updatedCustomer);

            return Ok(updatedCustomerDTO);
        }

        [HttpPost("Login")]
        [Produces("application/json")]
        public async Task<Token> Login([FromForm] LoginDTO loginDTO)
        {
            var login = _mapper.Map<LoginDTO, Login>(loginDTO);

            var customer = await _customerService.GetCustomerByLoginRequestAsync(login);

            if (customer != null)
            {

                var token = _customerService.CreateToken(login);

                return token;
            }

            return null;
        }
    }
}
