﻿using Dapper.FluentMap.Mapping;
using Challenge.BackEnd.Core.Domain.Entities;

namespace Challenge.BackEnd.Core.Domain.Mappings.Entities
{
    public class MotorcycleMap : EntityMap<Motorcycle>
    {
        public MotorcycleMap()
        {
            Map(r => r.Id).ToColumn("id");
            Map(r => r.FabricationYear).ToColumn("fabrication_year");
            Map(r => r.Model).ToColumn("model");
            Map(r => r.Plate).ToColumn("plate");
            Map(r => r.CreatedDate).ToColumn("created_date");
            Map(r => r.UpdatedDate).ToColumn("updated_date");
        }
    }
}