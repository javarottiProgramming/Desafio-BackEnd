using Dapper;
using Challenge.BackEnd.Core.Domain.Entities;
using Challenge.BackEnd.Core.Domain.Interfaces.Repositories;
using System.Data;

namespace Challenge.BackEnd.Infrastructure.Data.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly DatabaseConnection _databaseConnection;
        private const string TABLE_NAME = "rentals";

        public RentalRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;

        }

        public async Task<Rental?> GetRentalByIdAsync(int id)
        {
            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                string query = $"SELECT * FROM {TABLE_NAME} WHERE id = @id";
                return await connection.QueryFirstOrDefaultAsync<Rental>(query, new { id });
            }
        }

        public async Task<bool> CreateRentalAsync(Rental rental)
        {
            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                const string query = @$"
                    INSERT INTO {TABLE_NAME} (delivery_man_id, motorcycle_id, start_date, 
	                        end_date, expected_end_date, plan, daily_value)
                    VALUES (@DeliveryManId, @MotorcycleId, @StartDate, 
                            @EndDate, @ExpectedEndDate, @Plan, @DailyValue)";

                var result = await connection.ExecuteAsync(query, rental);

                return await Task.FromResult(result > 0);
            }
        }

        public Task<bool> UpdateRentalReturnByIdAsync(Rental rental)
        {
            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                string query = @$"UPDATE {TABLE_NAME} SET return_date = @ReturnDate, 
                    daily_value = @DailyValue, updated_date = @UpdatedDate WHERE id = @Id";

                var result = connection.Execute(query, rental);

                return Task.FromResult(result > 0);
            }
        }
    }
}