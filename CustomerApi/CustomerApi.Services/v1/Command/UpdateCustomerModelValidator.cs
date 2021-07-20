using System;
using FluentValidation;

namespace CustomerApi.Service.v1.Command
{
    public class UpdateCustomerModelValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.FirstName)
                .NotNull()
                .MinimumLength(2)
                .WithMessage("The first name must be at least 2 character long");
            
            RuleFor(x => x.LastName)
                .NotNull()
                .MinimumLength(2)
                .WithMessage("The last name must be at least 2 character long");

            RuleFor(x => x.BirthDate)
                .InclusiveBetween(DateTime.Now.AddYears(-150).Date, DateTime.Now)
                .WithMessage("The birthday must not be longer ago than 150 years and can not be in the future");
        }
    }
}
