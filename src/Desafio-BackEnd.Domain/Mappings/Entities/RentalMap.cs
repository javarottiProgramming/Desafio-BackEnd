using Dapper.FluentMap.Mapping;
using Desafio_BackEnd.Domain.Entities;

namespace Desafio_BackEnd.Domain.Mappings.Entities
{
    public class RentalMap : EntityMap<Rental>
    {
        public RentalMap()
        {
            Map(r => r.Id).ToColumn("id");
            Map(r => r.DeliveryManId).ToColumn("delivery_man_id");
            Map(r => r.MotorcycleId).ToColumn("motorcycle_id");
            Map(r => r.StartDate).ToColumn("start_date");
            Map(r => r.EndDate).ToColumn("end_date");
            Map(r => r.ExpectedEndDate).ToColumn("expected_end_date");
            Map(r => r.ReturnDate).ToColumn("return_date");
            Map(r => r.Plan).ToColumn("plan");
            Map(r => r.DailyValue).ToColumn("daily_value");
            Map(r => r.CreatedDate).ToColumn("created_date");
            Map(r => r.UpdatedDate).ToColumn("updated_date");
        }
    }
}