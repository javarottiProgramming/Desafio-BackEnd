namespace Desafio_BackEnd.Domain.Entities
{
    public class Motorcycle
    {
        public string Id { get; set; }
        public int FabricationYear { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}