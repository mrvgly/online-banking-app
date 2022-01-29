using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GetirCase.Core;
using GetirCase.Core.Models;
using GetirCase.Core.Services;
using GetirCase.Core.Utils;

namespace GetirCase.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public TransactionService(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<List<Transaction>> GetAllTransactionsByAccountId(int accountId)
        {
            var transactions = await _unitOfWork.Transaction.GetAllTransactionsByAccountIdAsync(accountId);

            return transactions;
        }

        public async Task<List<Transaction>> GetAllTransactionsByCustomerIdWithPeriods(int customerId, string startDate, string endDate)
        {
            var sDate = Helper.ToDateTime(startDate);
            var eDate = Helper.ToDateTime(endDate);

            if (!Helper.DateTimeChecker(sDate, eDate))
                throw new Exception("Start date must be less than end date.");

            var transactions = await _unitOfWork.Transaction.GetAllTransactionsByCustomerIdWithPeriodsAsync(customerId, sDate, eDate);

            return transactions;
        }

        public async Task<Transaction> MakeDeposit(Transaction transaction)
        {
            var account = await _unitOfWork.Accounts.GetAccountWithCustomerInfoByIdAsync(transaction.AccountId);

            await _accountService.UpdateAccountBalance(account, transaction.Amount);

            transaction.Account = account;

            transaction.Date = DateTime.Now;

            transaction.Type = TransactionTypes.Deposit;

            await _unitOfWork.Transaction.AddAsync(transaction);

            await _unitOfWork.CommitAsync();

            return transaction;
        }

        public async Task<Transaction> MakeWithdrawal(Transaction transaction)
        {
            var account = await _unitOfWork.Accounts.GetAccountWithCustomerInfoByIdAsync(transaction.AccountId);

            if(account.Balance < transaction.Amount)
                throw new Exception("Not sufficient balance for this withdrawal");

            await _accountService.UpdateAccountBalance(account, -transaction.Amount);

            transaction.Account = account;

            transaction.Date = DateTime.Now;

            transaction.Type = TransactionTypes.Withdrawing;

            await _unitOfWork.Transaction.AddAsync(transaction);

            await _unitOfWork.CommitAsync();

            return transaction;
        }
    }
}
