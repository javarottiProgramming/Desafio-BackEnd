using Dapper;
using Challenge.BackEnd.Core.Domain.Entities;
using Challenge.BackEnd.Core.Domain.Interfaces.Repositories;
using System.Data;

namespace Challenge.BackEnd.Infrastructure.Data.Repositories
{
    public class DeliveryManRepository : IDeliveryManRepository
    {
        private readonly DatabaseConnection _databaseConnection;
        private const string TABLE_NAME = "deliverymen";

        public DeliveryManRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<bool> CreateDeliveryManAsync(DeliveryMan deliveryMan)
        {
            var query = $"INSERT INTO {TABLE_NAME} " +
                $"(id, name, document, birth_date, drivers_license, drivers_license_category) " +
                $"VALUES (@Id, @Name, @Document, @BirthDate, @DriversLicense, @DriversLicenseCategory)";

            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, deliveryMan);

                return await Task.FromResult(result > 0);
            }
        }

        public async Task<DeliveryMan?> GetDeliveryManByIdAsync(string id)
        {
            var query = $"SELECT * FROM {TABLE_NAME} WHERE id = @id";
            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<DeliveryMan>(query, new { id });
                return await Task.FromResult(result);
            }
        }
    }
}
