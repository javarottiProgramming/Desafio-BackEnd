using System.Text.Json.Serialization;

namespace Challenge.BackEnd.Core.Domain.Dtos
{
    /// <summary>
    /// DTO de devolução da locação
    /// </summary>
    public class RentalDto
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        [JsonPropertyName("valor_diaria")]
        public decimal DailyValue { get; set; }

        [JsonPropertyName("entregador_id")]
        public string DeliveryManId { get; set; }

        [JsonPropertyName("moto_id")]
        public string MotorCycleId { get; set; }

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime ExpectedEndDate { get; set; }

        [JsonPropertyName("data_devolucao")]
        public DateTime? ReturnDate { get; set; }

    }

}