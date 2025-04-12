using Desafio_BackEnd.Domain.Models;
using FluentValidation;

namespace Desafio_BackEnd.Domain.Validators
{
    public class MotorcycleValidator : AbstractValidator<MotorcycleRequest>
    {
        public MotorcycleValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Year).NotNull().InclusiveBetween(1951, DateTime.UtcNow.Year);
            RuleFor(x => x.Model).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Plate).NotNull().NotEmpty().MaximumLength(8); //TODO Melhorar validacoes?
        }
    }

    public class MotorcycleUpdateValidator : AbstractValidator<MotorcycleUpdate>
    {
        public MotorcycleUpdateValidator()
        {
            RuleFor(x => x.Plate).NotNull().NotEmpty().MaximumLength(8); //TODO Melhorar validacoes?
        }
    }
}
