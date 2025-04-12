using Desafio_BackEnd.Domain.Models;
using FluentValidation;

namespace Desafio_BackEnd.Domain.Validators
{
    public class DeliveryManValidator : AbstractValidator<DeliveryMan>
    {
        public DeliveryManValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.DriversLicenseCategory)
                .NotNull().MaximumLength(3)
                .Must(x => x == "A" || x == "B" || x == "A+B");
        }
    }
}
