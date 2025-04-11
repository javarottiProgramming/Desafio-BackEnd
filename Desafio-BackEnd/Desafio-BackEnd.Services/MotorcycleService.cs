using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        public Task<bool> CreateMotorcycleAsync(Motorcycle motorcycle)
        {
            Console.WriteLine($"Criando moto: {motorcycle.Id}");

            //A placa é um dado único e não pode se repetir.
            //TODO: Verificar se a placa já existe no banco de dados.

            //Quando a moto for cadastrada a aplicação deverá gerar um evento de moto cadastrada
            //Todo: Implementar o evento de moto cadastrada.


            return Task.FromResult(true);
        }

        public Task<bool> DeleteMotorcycleAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Motorcycle>> GetAllMotorcyclesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Motorcycle> GetMotorcycleByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMotorcycleAsync(string id, Motorcycle motorcycle)
        {
            throw new NotImplementedException();
        }
    }
}
