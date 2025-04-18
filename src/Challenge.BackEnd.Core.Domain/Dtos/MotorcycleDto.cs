﻿using System.Text.Json.Serialization;

namespace Challenge.BackEnd.Core.Domain.Dtos
{
    public class MotorcycleDto
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        [JsonPropertyName("ano")]
        public int FabricationYear { get; set; }

        [JsonPropertyName("modelo")]
        public string Model { get; set; }

        [JsonPropertyName("placa")]
        public string Plate { get; set; }
    }
}