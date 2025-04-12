using Dapper;
using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Interfaces.Repositories;
using System.Data;

namespace Desafio_BackEnd.Data.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly DatabaseConnection _databaseConnection;
        private const string TABLE_NAME = "rentals";

        public RentalRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;

        }

        public async Task<Rental> GetRentalByIdAsync(string id)
        {
            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                string query = $"SELECT * FROM {TABLE_NAME} WHERE id = @id";
                var rental = await connection.QueryFirstOrDefaultAsync<Rental>(query, new { id = Convert.ToInt32(id) });
                return rental;
            }
        }





        public async Task AddRentalAsync(Rental rental)
        {
            using (IDbConnection connection = _databaseConnection.CreateConnection())
            {
                const string query = @"
                    INSERT INTO Rentals (DeliveryManId, MotoId, StartDate, EndDate, ExpectedEndDate, Plan)
                    VALUES (@DeliveryManId, @MotoId, @StartDate, @EndDate, @ExpectedEndDate, @Plan)";
                await connection.ExecuteAsync(query, rental);
            }
        }
    }
}
