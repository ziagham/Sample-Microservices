using System;
using FluentValidation;

namespace OrderApi.Service.v1.Command.CreateOrder
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerFullName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(2).WithMessage("The {PropertyName} must be at least 2 character long");
        }
    }
}
