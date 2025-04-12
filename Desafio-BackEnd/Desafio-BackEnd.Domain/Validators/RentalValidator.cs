using Desafio_BackEnd.Domain.Models;
using FluentValidation;

namespace Desafio_BackEnd.Domain.Validators
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {

            RuleFor(x => x.DeliveryManId).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.MotoId).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.StartDate).NotNull();
            RuleFor(x => x.EndDate).NotNull();
            RuleFor(x => x.ExpectedEndDate).NotNull();
            //RuleFor(x => x.Plan).NotNull().InclusiveBetween(1, 3);
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
