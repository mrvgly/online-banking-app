using FluentValidation;
using GetirCase.Api.DTO;

namespace GetirCase.Api.Validators
{
    public class SaveCustomerDTOValidator : AbstractValidator<SaveCustomerDTO>
    {
        public SaveCustomerDTOValidator()
        {
            RuleFor(a => a.Name)
              .NotEmpty()
              .MaximumLength(50);
        }
    }
}
