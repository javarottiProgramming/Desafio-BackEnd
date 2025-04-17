using Challenge.BackEnd.Core.Domain.Dtos;
using FluentValidation;

namespace Challenge.BackEnd.Core.Domain.Validators
{
    public class DeliveryManValidator : AbstractValidator<DeliveryManDto>
    {
        public DeliveryManValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("'Id' is required");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("'nome' is required");
            RuleFor(x => x.Document).NotNull().NotEmpty().WithMessage("'cnpj' is required");
            RuleFor(x => x.BirthDate).NotNull().WithMessage("'data_nascimento' is required")
                .Must(BeAValidDate).WithMessage("'data_nascimento' is invalid");

            RuleFor(x => x.DriversLicense).NotNull().NotEmpty().WithMessage("'numero_cnh' is required")
                .MaximumLength(11).WithMessage("'numero_cnh' must be 11 characters long");

            RuleFor(x => x.DriversLicenseCategory)
                .NotNull().NotEmpty().WithMessage("'tipo_cnh' is required")
                .MaximumLength(3)
                .Must(x => x == "A" || x == "B" || x == "A+B").WithMessage("'tipo_cnh' must be: A, B or A+B");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date > DateTime.MinValue && date < DateTime.MaxValue;
        }
    }
}
