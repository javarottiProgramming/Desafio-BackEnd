using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Entities
{
    public class Rental
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } // Chave primária

        [JsonPropertyName("delivery_man_id")]
        public required string DeliveryManId { get; set; }

        [JsonPropertyName("motorcycle_id")]
        public required string MotorcycleId { get; set; }

        [JsonPropertyName("start_date")]
        public required DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public required DateTime EndDate { get; set; }

        [JsonPropertyName("expected_end_date")]
        public required DateTime ExpectedEndDate { get; set; }

        [JsonPropertyName("plan")]
        public int Plan { get; set; }

        [JsonPropertyName("daily_value")]
        public decimal DailyValue { get; set; }

        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("updated_date")]
        public DateTime UpdatedDate { get; set; }
    }
}
