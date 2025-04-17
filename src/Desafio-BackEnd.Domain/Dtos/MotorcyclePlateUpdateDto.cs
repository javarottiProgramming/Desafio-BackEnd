using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Dtos
{
    public class MotorcyclePlateUpdateDto
    {
        [JsonPropertyName("placa")]
        [Required]
        public string Plate { get; set; }
    }
}