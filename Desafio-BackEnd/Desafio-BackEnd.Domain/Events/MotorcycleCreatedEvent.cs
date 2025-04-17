namespace Desafio_BackEnd.Domain.Events
{
    public class MotorcycleCreatedEvent
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public int FabricationYear { get; set; }
    }
}