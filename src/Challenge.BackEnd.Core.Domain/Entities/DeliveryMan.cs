namespace Challenge.BackEnd.Core.Domain.Entities
{
    public class DeliveryMan
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
        public string DriversLicense { get; set; }
        public string DriversLicenseCategory { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}