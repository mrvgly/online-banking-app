using FluentValidation;
using GetirCase.Api.DTO;

namespace GetirCase.Api.Validators
{
    public class SaveAccountDTOValidator: AbstractValidator<SaveAccountDTO>
    {
        public SaveAccountDTOValidator()
        {
            RuleFor(m => m.CustomerId)
                .NotEmpty()
                .WithMessage("'Customer Id' must not be 0.");
        }
    }
}
