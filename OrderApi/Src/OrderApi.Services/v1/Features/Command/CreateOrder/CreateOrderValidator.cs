using System;
using FluentValidation;

namespace OrderApi.Services.v1.Features.Command.CreateOrder
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
