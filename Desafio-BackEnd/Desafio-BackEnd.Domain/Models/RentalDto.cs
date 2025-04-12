using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Models
{
    /// <summary>
    /// Modelo de locação
    /// </summary>
    public class RentalDto
    {
        [JsonPropertyName("entregador_id")]
        public string DeliveryManId { get; set; }

        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; }

        [JsonPropertyName("data_inicio")]
        public required DateTime StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public required DateTime EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public required DateTime ExpectedEndDate { get; set; }

        [JsonPropertyName("plano")]
        public int Plan { get; set; }

        /// <summary>
        /// Valor da diária
        /// </summary>
        /// 
        [JsonIgnore]
        public decimal DailyValue
        {
            get
            {
                return CalculateDailyValue(this.Plan);
            }
        }

        private decimal CalculateDailyValue(int plan)
        {
            return plan switch
            {
                7 => 30.0m,
                15 => 28.0m,
                30 => 22.0m,
                45 => 20.0m,
                50 => 18.0m,
                _ => throw new ArgumentException("Plano inválido.")
            };
        }
    }

    /// <summary>
    /// Modelo de devolução da locação
    /// </summary>
    public class RentalReturnDto
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
