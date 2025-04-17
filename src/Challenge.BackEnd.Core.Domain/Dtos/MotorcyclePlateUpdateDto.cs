using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Challenge.BackEnd.Core.Domain.Dtos
{
    public class MotorcyclePlateUpdateDto
    {
        [JsonPropertyName("placa")]
        [Required]
        public string Plate { get; set; }
    }
}