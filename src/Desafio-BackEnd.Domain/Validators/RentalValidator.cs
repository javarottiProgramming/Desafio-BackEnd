using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Models;
using FluentValidation;

namespace Challenge.BackEnd.Core.Domain.Validators
{
    public class RentalValidator : AbstractValidator<CreateRentalModel>
    {
        public RentalValidator()
        {

            RuleFor(x => x.DeliveryManId).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.MotorcycleId).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.StartDate).NotNull().Must(BeAValidDate).Must(BeAValidStartDate);
            RuleFor(x => x.EndDate).NotNull().Must(BeAValidDate);
            RuleFor(x => x.ExpectedEndDate).NotNull().Must(BeAValidDate).Must(BeAValidExpectedEndDate);
            RuleFor(x => x.Plan).NotNull().
                Must(x => x == 7 || x == 15 || x == 30 || x == 45 || x == 50)
                .WithMessage("O plano deve ser 7, 15, 30, 45 ou 50 dias.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date > DateTime.MinValue && date < DateTime.MaxValue;
        }

        private bool BeAValidStartDate(DateTime startDate)
        {
            var now = DateTime.Now;

            return startDate > now && startDate.Day == now.AddDays(1).Day;
        }

        //TODO: Melhorar a validação?
        //pois esta se baseando no campo PLAN adotando que o campo é a qtd de dias
        private bool BeAValidExpectedEndDate(CreateRentalModel obj, DateTime expectedEndDate)
        {
            return expectedEndDate > obj.StartDate && 
                expectedEndDate.Day == obj.StartDate.AddDays(obj.Plan).Day;
        }
    }

    public class RentalReturnDatelidator : AbstractValidator<RentalReturnDto>
    {
        public RentalReturnDatelidator()
        {
            RuleFor(x => x.ReturnDate).NotNull();

        }
    }

}
