﻿namespace Challenge.BackEnd.Core.Domain.Events
{
    public class MotorcycleCreatedEvent
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public int FabricationYear { get; set; }
    }

    public class MotorcycleNotification
    {
        public string Id { get; set; }
        public int FabricationYear { get; set; }
    }
}