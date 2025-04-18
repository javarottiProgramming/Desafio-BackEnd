using Dapper;
using Challenge.BackEnd.Core.Domain.Entities;
using Challenge.BackEnd.Core.Domain.Interfaces.Repositories;
using System.Data;

namespace Challenge.BackEnd.Infrastructure.Data.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly DatabaseConnection _databaseConnection;
        private const string TABLE_NAME = "motorcycles";

        public MotorcycleRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<bool> CreateMotorcycleAsync(Motorcycle motorcycle)
        {
            var query = $"INSERT INTO {TABLE_NAME} " +
                $"(id, fabrication_year, model, plate) " +
                $"VALUES (@Id, @FabricationYear, @Model, @Plate)";

            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, motorcycle);

                return await Task.FromResult(result > 0);
            }
        }

        public async Task<Motorcycle?> GetMotorcycleByPlateAsync(string plate)
        {
            var query = $"SELECT * FROM {TABLE_NAME} WHERE plate = @plate";
            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<Motorcycle>(query, new { plate });
                return await Task.FromResult(result);
            }
        }

        public async Task<Motorcycle?> GetMotorcycleByIdAsync(string id)
        {
            var query = $"SELECT * FROM {TABLE_NAME} WHERE id = @id";
            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<Motorcycle>(query, new { id });
                return await Task.FromResult(result);
            }
        }

        public async Task<bool> UpdateMotorcyclePlateByIdAsync(string id, string plate)
        {
            var now = DateTime.UtcNow;
            var query = $"UPDATE {TABLE_NAME} SET plate = @plate, updated_date = @now WHERE id = @id";

            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { id, plate, now });
                return await Task.FromResult(result == 1);
            }
        }

        public async Task<bool> DeleteMotorcycleByIdAsync(string id)
        {
            var query = $"DELETE FROM {TABLE_NAME} WHERE id = @id";

            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { id });
                return await Task.FromResult(result == 1);
            }
        }
    }
}