using FluentValidation;
using System.ComponentModel;

namespace Desafio_BackEnd.Domain.Models
{
    public class DeliveryMan
    {
        [DisplayName("Identificador")]
        public string Id { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public string DocumentImgBase64 { get; set; }
    }

    /*
     Os tipos de cnh válidos são A, B ou ambas A+B.
	- O cnpj é único e não pode se repetir.
	- O número da CNH é único e não pode se repetir.
     */

    public class DeliveryManValidator : AbstractValidator<DeliveryMan>
    {
        public DeliveryManValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}