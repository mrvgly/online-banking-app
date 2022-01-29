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
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet("Detail/{id}")]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<AccountDTO>> GetAccountDetailById(int id)
        {
            var account = await _accountService.GetAccountDetailById(id);
            var accountDTO = _mapper.Map<Account, AccountDTO>(account);

            return Ok(accountDTO);
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<AccountDTO>> CreateAccount([Required][FromBody] SaveAccountDTO saveAccountDTO)
        {
            var validator = new SaveAccountDTOValidator();
            var validationResult = await validator.ValidateAsync(saveAccountDTO);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var accountToCreate = _mapper.Map<SaveAccountDTO, Account>(saveAccountDTO);

            var newAccount = await _accountService.CreateAccount(accountToCreate);

            var account = await _accountService.GetAccountDetailById(newAccount.Id);

            var accountDTO = _mapper.Map<Account, AccountDTO>(account);

            return Ok(accountDTO);
        }
    }
}
