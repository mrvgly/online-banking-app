using System.Collections.Generic;
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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpGet("{accountId}")]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<List<TransactionDTO>>> GetTransactionsByAccountId(int accountId)
        {
            var transactions = await _transactionService.GetAllTransactionsByAccountId(accountId);

            var transactionsDTOs = _mapper.Map<List<Transaction>, List<TransactionDTO>>(transactions);

            return Ok(transactionsDTOs);
        }

        [HttpGet("WithPeriods/{customerId}")]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<List<TransactionDTO>>> GetAllTransactionsByCustomerIdWithPeriods(int customerId,
                                                                                                        [FromQuery] string startDate,
                                                                                                        [FromQuery] string endDate)
        {
            var transactions = await _transactionService.GetAllTransactionsByCustomerIdWithPeriods(customerId, startDate, endDate);

            var transactionsDTOs = _mapper.Map<List<Transaction>, List<TransactionDTO>>(transactions);

            transactionsDTOs.ForEach(x => x.CustomerId = customerId);

            return Ok(transactionsDTOs);
        }

        [HttpPost("MakeDeposit")]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<SaveTransactionDTO>> MakeDeposit(SaveTransactionDTO saveTransactionDTO)
        {
            var validator = new SaveTransactionDTOValidator();
            var validationResult = await validator.ValidateAsync(saveTransactionDTO);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var transaction = _mapper.Map<SaveTransactionDTO, Transaction>(saveTransactionDTO);

            await _transactionService.MakeDeposit(transaction);

            return Ok(saveTransactionDTO);
        }

        [HttpPost("MakeWithdrawal")]
        [Produces("application/json")]
        [Authorize]
        public async Task<ActionResult<SaveTransactionDTO>> MakeWithdrawal(SaveTransactionDTO saveTransactionDTO)
        {
            var validator = new SaveTransactionDTOValidator();
            var validationResult = await validator.ValidateAsync(saveTransactionDTO);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var transaction = _mapper.Map<SaveTransactionDTO, Transaction>(saveTransactionDTO);

            await _transactionService.MakeWithdrawal(transaction);

            return Ok(saveTransactionDTO);
        }
    }
}
