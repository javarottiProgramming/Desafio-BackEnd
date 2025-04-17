using Dapper.FluentMap.Mapping;
using Challenge.BackEnd.Core.Domain.Entities;

namespace Challenge.BackEnd.Core.Domain.Mappings.Entities
{
    public class DeliveryManMap : EntityMap<DeliveryMan>
    {
        public DeliveryManMap()
        {
            Map(r => r.Id).ToColumn("id");
            Map(r => r.Name).ToColumn("name");
            Map(r => r.Document).ToColumn("document");
            Map(r => r.BirthDate).ToColumn("birth_date");
            Map(r => r.DriversLicense).ToColumn("drivers_license");
            Map(r => r.DriversLicenseCategory).ToColumn("drivers_license_category");
            Map(r => r.CreatedDate).ToColumn("created_date");
            Map(r => r.UpdatedDate).ToColumn("updated_date");
        }
    }
}
