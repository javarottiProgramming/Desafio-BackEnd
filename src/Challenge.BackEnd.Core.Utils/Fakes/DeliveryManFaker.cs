using Bogus;
using Bogus.Extensions.Brazil;
using Challenge.BackEnd.Core.Domain.Entities;

namespace Challenge.BackEnd.Core.Utils.Fakes
{
    public class DeliveryManFaker : Faker<DeliveryMan>
    {
        private readonly string[] driversLicenseCategories = { "A", "B", "A+B" };

        public DeliveryManFaker()
            : base("pt_BR")
        {
            RuleFor(d => d.Id, f => "entregador" + f.Random.Number(1, 9999) + "_"); //Apenas para não correr o risco de ter o mesmo id mesmo usando transações
            RuleFor(d => d.Name, f => f.Name.FullName());
            RuleFor(d => d.Document, f => f.Company.Cnpj(false));
            RuleFor(d => d.BirthDate, f => f.Date.Past(30));
            RuleFor(d => d.DriversLicense, f => f.Random.String2(11, "0123456789"));
            RuleFor(d => d.DriversLicenseCategory, f => f.PickRandom(driversLicenseCategories));
        }
    }
}