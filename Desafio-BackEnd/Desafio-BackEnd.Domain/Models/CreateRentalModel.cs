using System.Text.Json.Serialization;

namespace Desafio_BackEnd.Domain.Models
{ 
    /// <summary>
  /// Modelo de locação
  /// </summary>
    public class CreateRentalModel
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
                return CalculateDailyValue(Plan);
            }
        }

        /// <summary>
        /// Calcula o valor da diária com base no plano informado.
        /// (7, 15, 30, 45 ou 50)
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static decimal CalculateDailyValue(int plan)
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
}