using FluentValidation;

namespace Desafio_BackEnd.Domain.Models
{
    public class Motorcycle
    {

        /// <summary>
        /// Identificador da moto
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Ano da moto
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Modelo da moto
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Placa da moto
        /// </summary>
        public string Plate { get; set; }
    }

    public class MotorcycleValidator : AbstractValidator<Motorcycle>
    {
        public MotorcycleValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Year).NotNull().InclusiveBetween(1951, DateTime.UtcNow.Year);
            RuleFor(x => x.Model).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Plate).NotNull().NotEmpty().MaximumLength(8); //TODO Melhorar validacoes?
        }
    }
}
