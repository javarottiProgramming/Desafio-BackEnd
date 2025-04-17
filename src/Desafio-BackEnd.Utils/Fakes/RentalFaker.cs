using Bogus;
using Challenge.BackEnd.Core.Domain.Entities;
using System.Numerics;

namespace Challenge.BackEnd.Core.Utils.Fakes
{
    public class RentalFaker : Faker<Rental>
    {
        public RentalFaker()
            : base("pt_BR")
        {
            RuleFor(r => r.DeliveryManId, f => "entregador" + f.Random.Number(1, 9999) + "_");
            RuleFor(r => r.MotorcycleId, f => "moto" + f.Random.Number(1, 9999) + "_");
            RuleFor(r => r.StartDate, f => DateTime.UtcNow.AddDays(1));
            RuleFor(r => r.EndDate, f => DateTime.UtcNow.AddDays(7));
            RuleFor(r => r.ExpectedEndDate, DateTime.UtcNow.AddDays(7));
            RuleFor(r => r.Plan, f => 7);
            RuleFor(r => r.DailyValue, 30);
        }

        public RentalFaker(string entregadorId, string motorcycleId)
            : base("pt_BR")
        {
            RuleFor(r => r.DeliveryManId, entregadorId);
            RuleFor(r => r.MotorcycleId, motorcycleId);
            RuleFor(r => r.StartDate, f => DateTime.UtcNow.AddDays(1));
            RuleFor(r => r.EndDate, f => DateTime.UtcNow.AddDays(7));
            RuleFor(r => r.ExpectedEndDate, DateTime.UtcNow.AddDays(7));
            RuleFor(r => r.Plan, f => 7);
            RuleFor(r => r.DailyValue, 30);
        }

        public RentalFaker(int plan)
           : base("pt_BR")
        {

            plan = plan switch
            {
                7 => 7,
                15 => 15,
                30 => 30,
                45 => 45,
                50 => 50,
                _ => 7
            };

            var dailyValue = plan switch
            {
                7 => 30.0m,
                15 => 28.0m,
                30 => 22.0m,
                45 => 20.0m,
                50 => 18.0m,
                _ => 30.0m
            };

            RuleFor(r => r.DeliveryManId, f => "entregador" + f.Random.Number(1, 9999) + "_");
            RuleFor(r => r.MotorcycleId, f => "moto" + f.Random.Number(1, 9999) + "_");
            RuleFor(r => r.StartDate, f => DateTime.UtcNow.AddDays(1));
            RuleFor(r => r.EndDate, f => DateTime.UtcNow.AddDays(plan));
            RuleFor(r => r.ExpectedEndDate, DateTime.UtcNow.AddDays(plan));
            RuleFor(r => r.Plan, plan);
            RuleFor(r => r.DailyValue, dailyValue);
        }
    }
}