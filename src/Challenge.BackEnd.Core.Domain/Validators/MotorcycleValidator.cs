using Challenge.BackEnd.Core.Domain.Dtos;
using FluentValidation;

namespace Challenge.BackEnd.Core.Domain.Validators
{
    public class MotorcycleValidator : AbstractValidator<MotorcycleDto>
    {
        public MotorcycleValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("'identificador' is required");
            RuleFor(x => x.FabricationYear).NotNull().WithMessage("'ano' is required")
                .InclusiveBetween(1951, DateTime.UtcNow.Year).WithMessage("'ano' is invalid");
            RuleFor(x => x.Model).NotNull().NotEmpty().WithMessage("'modelo' is required");
            RuleFor(x => x.Plate).NotNull().NotEmpty().WithMessage("'placa' is required");
        }
    }

    public class MotorcycleUpdateValidator : AbstractValidator<MotorcyclePlateUpdateDto>
    {
        public MotorcycleUpdateValidator()
        {
            RuleFor(x => x.Plate).NotNull().NotEmpty().WithMessage("'placa' is required");
        }
    }
}
