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
            RuleFor(x => x.Year).NotNull();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Plate).NotEmpty();
        }
    }
}
