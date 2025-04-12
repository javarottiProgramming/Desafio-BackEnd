using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Models
{
    /// <summary>
    /// Modelo de locação
    /// </summary>
    public class Rental
    {
        [JsonPropertyName("entregador_id")]
        public string DeliveryManId { get; set; }

        [JsonPropertyName("moto_id")]
        public string MotoId { get; set; }

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime ExpectedEndDate { get; set; }

        [JsonPropertyName("plano")]
        public int Plan { get; set; }
    }

    /// <summary>
    /// Modelo de devolução da locação
    /// </summary>
    public class RentalReturn
    {

        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        [JsonPropertyName("valor_diaria")]
        public decimal DailyValue { get; set; }

        [JsonPropertyName("entregador_id")]
        public string DeliveryManId { get; set; }

        [JsonPropertyName("moto_id")]
        public string MotoId { get; set; }

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime ExpectedEndDate { get; set; }

        [JsonPropertyName("data_devolucao")]
        public DateTime ReturnDate { get; set; }

    }

    /// <summary>
    /// Modelo de data de devolução da locação
    /// </summary>
    public class RentalReturnDate
    {
        [JsonPropertyName("data_devolucao")]
        public required DateTime ReturnDate { get; set; }
    }
}
