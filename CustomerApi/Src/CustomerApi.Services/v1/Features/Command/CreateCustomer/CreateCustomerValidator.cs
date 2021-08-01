using System;
using FluentValidation;

namespace CustomerApi.Services.v1.Features.Command.CreateCustomer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(2).WithMessage("The first name must be at least 2 character long");
            
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(2).WithMessage("The last name must be at least 2 character long");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .InclusiveBetween(DateTime.Now.AddYears(-150).Date, DateTime.Now)
                .WithMessage("The birthday must not be longer ago than 150 years and can not be in the future");
        }
    }
}
