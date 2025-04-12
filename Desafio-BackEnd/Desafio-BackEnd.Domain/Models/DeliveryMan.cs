using FluentValidation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Models
{
    public class DeliveryMan
    {
        [DisplayName("Identificador")]
        public required string Id { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public string DocumentImgBase64 { get; set; }
    }

    public class DeliveryManFileUpload
    {
        [JsonPropertyName("imagem_cnh")]
        
        public required string DocumentImgBase64 { get; set; }




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