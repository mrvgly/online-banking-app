using FluentValidation;
using GetirCase.Api.DTO;

namespace GetirCase.Api.Validators
{
    public class SaveTransactionDTOValidator : AbstractValidator<SaveTransactionDTO>
    {
        public SaveTransactionDTOValidator()
        {
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0).WithMessage("Amount must be grater than zero.");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
            RuleFor(x => x.AccountId).NotEmpty().WithMessage("AccountId is required.");
        }
    }
}
