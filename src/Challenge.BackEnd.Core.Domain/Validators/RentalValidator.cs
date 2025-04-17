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
            RuleFor(x => x.StartDate).NotNull()
                .Must(BeAValidDate)
                .Must(BeAValidStartDate)
                .WithMessage("'data_inicio' invalid.");;
            RuleFor(x => x.EndDate).NotNull()
                .Must(BeAValidDate)
                .Must(BeAValidExpectedEndDate)
                .WithMessage("'data_termino' invalid.");
            RuleFor(x => x.ExpectedEndDate).NotNull()
                .Must(BeAValidDate)
                .Must(BeAValidExpectedEndDate)
                .WithMessage("'data_previsao_termino' invalid.");
            RuleFor(x => x.Plan).NotNull()
                .Must(x => x == 7 || x == 15 || x == 30 || x == 45 || x == 50)
                .WithMessage("'plano' value must be 7, 15, 30, 45 or 50.");
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
