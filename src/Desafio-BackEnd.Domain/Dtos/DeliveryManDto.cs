using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Dtos
{
    public class DeliveryManDto
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("cnpj")]
        public string Document { get; set; }

        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; set; }
        
        [JsonPropertyName("numero_cnh")]
        public string DriversLicense { get; set; }

        [JsonPropertyName("tipo_cnh")]
        public string DriversLicenseCategory { get; set; }

        [JsonPropertyName("imagem_cnh")]
        public string? DriversLicenseBase64 { get; set; }
    }

    public class DeliveryManDtoFileUpload
    {
        [JsonPropertyName("imagem_cnh")]
        public required string DocumentImgBase64 { get; set; }
    }
}