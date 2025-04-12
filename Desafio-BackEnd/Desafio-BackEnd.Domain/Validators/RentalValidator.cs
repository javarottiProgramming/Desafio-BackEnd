using Desafio_BackEnd.Domain.Models;
using FluentValidation;

namespace Desafio_BackEnd.Domain.Validators
{
    public class RentalValidator : AbstractValidator<RentalDto>
    {
        public RentalValidator()
        {

            RuleFor(x => x.DeliveryManId).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.MotorcycleId).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.StartDate).NotNull().Must(BeAValidDate);
            RuleFor(x => x.EndDate).NotNull().Must(BeAValidDate);
            RuleFor(x => x.ExpectedEndDate).NotNull().Must(BeAValidDate);
            RuleFor(x => x.Plan).NotNull().
                Must(x => x == 7 || x == 15 || x == 30 || x == 45 || x == 50)
                .WithMessage("O plano deve ser 7, 15, 30, 45 ou 50 dias.");
        }

        //TODO Verificar
        private bool BeAValidDate(DateTime date)
        {
            //TODO O inicio da locação obrigatóriamente é o primeiro dia após a data de criação.
            return date > DateTime.MinValue && date < DateTime.MaxValue;
        }
    }

    public class RentalReturnDatelidator : AbstractValidator<RentalReturnDate>
    {
        public RentalReturnDatelidator()
        {
            RuleFor(x => x.ReturnDate).NotNull();

        }
    }

}
