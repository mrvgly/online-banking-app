using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using GetirCase.Api.DTO;
using GetirCase.Api.Validators;
using GetirCase.Core.Models;
using GetirCase.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetirCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllWithCustomer();
            var accountDTOs = _mapper.Map<IEnumerable<Account>, IEnumerable<AccountDTO>>(accounts);

            return Ok(accountDTOs);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<AccountDTO>> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountById(id);
            var accountResource = _mapper.Map<Account, AccountDTO>(account);

            return Ok(accountResource);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<AccountDTO>> CreateAccount([Required][FromBody] SaveAccountDTO saveAccountDTO)
        {
            var validator = new SaveAccountDTOValidator();
            var validationResult = await validator.ValidateAsync(saveAccountDTO);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var accountToCreate = _mapper.Map<SaveAccountDTO, Account>(saveAccountDTO);

            var newAccount = await _accountService.CreateAccount(accountToCreate);

            var account = await _accountService.GetAccountById(newAccount.Id);

            var accountDTO = _mapper.Map<Account, AccountDTO>(account);

            return Ok(accountDTO);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<AccountDTO>> UpdateAccount(int id, [FromBody][Required] SaveAccountDTO saveAccountDTO)
        {
            var validator = new SaveAccountDTOValidator();
            var validationResult = await validator.ValidateAsync(saveAccountDTO);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var accountToBeUpdate = await _accountService.GetAccountById(id);

            if (accountToBeUpdate == null)
                return NotFound();

            var account = _mapper.Map<SaveAccountDTO, Account>(saveAccountDTO);

            await _accountService.UpdateAccount(accountToBeUpdate, account);

            var updatedAccount = await _accountService.GetAccountById(id);
            var updatedAccountDTO = _mapper.Map<Account, AccountDTO>(updatedAccount);

            return Ok(updatedAccountDTO);
        }
    }
}
