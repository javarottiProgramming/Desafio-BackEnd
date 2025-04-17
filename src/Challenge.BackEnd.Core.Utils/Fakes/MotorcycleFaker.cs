using Bogus;
using Challenge.BackEnd.Core.Domain.Entities;

namespace Challenge.BackEnd.Core.Utils.Fakes
{
    public class MotorcycleFaker : Faker<Motorcycle>
    {
        public MotorcycleFaker()
            : base("pt_BR")
        {
            RuleFor(m => m.Id, f => "moto" + f.Random.Number(1, 9999) + "_");
            RuleFor(m => m.FabricationYear, f => f.Date.Past(1).Year);
            RuleFor(m => m.Model, f => f.Vehicle.Model());
            RuleFor(m => m.Plate, f => f.Vehicle.Vin().Substring(0, 8));
        }
    }
}