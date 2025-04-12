using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Dtos
{
    /// <summary>
    /// DTO devolução da locação
    /// </summary>
    public class RentalReturnDto
    {
        [JsonPropertyName("data_devolucao")]
        public required DateTime ReturnDate { get; set; }
    }
}