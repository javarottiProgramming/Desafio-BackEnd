using FluentValidation;
using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Models
{
    public class DeliveryMan
    {
        [JsonPropertyName("identificador")]
        public required string Id { get; set; }

        [JsonPropertyName("nome")]
        public required string Name { get; set; }

        [JsonPropertyName("cnpj")]
        public required string DocumentNumber { get; set; }

        [JsonPropertyName("data_nascimento")]
        public required DateTime BirthDate { get; set; }
        
        [JsonPropertyName("numero_cnh")]
        public required string DriversLicenseNumber { get; set; }

        [JsonPropertyName("tipo_cnh")]
        public required string DriversLicenseCategory { get; set; }

        [JsonPropertyName("imagem_cnh")]
        public string? DriversLicenseBase64 { get; set; }
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

     Eu como usuário entregador quero me cadastrar na plataforma para alugar motos.
	- Os dados do entregador são( identificador, nome, cnpj, data de nascimento, número da CNHh, tipo da CNH, imagemCNH)
	- Os tipos de cnh válidos são A, B ou ambas A+B.
	- O cnpj é único e não pode se repetir.
	- O número da CNH é único e não pode se repetir.
     */

}