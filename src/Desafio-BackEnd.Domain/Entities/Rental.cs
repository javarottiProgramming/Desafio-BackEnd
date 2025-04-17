namespace Challenge.BackEnd.Core.Domain.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public string DeliveryManId { get; set; }
        public string MotorcycleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int Plan { get; set; }
        public decimal DailyValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}