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
            //TODO: Implementar a exclusão da moto no banco de dados.
            return Task.FromResult(true);
        }

        public Task<Motorcycle> GetMotorcycleByPlateAsync(string plate)
        {
            //TODO: Implementar a busca da moto pela placa no banco de dados.
            return Task.FromResult(new Motorcycle { Plate = plate });
        }

        public Task<Motorcycle> GetMotorcycleByIdAsync(string id)
        {
            //TODO: Implementar a busca da moto pelo ID no banco de dados.
            return Task.FromResult(new Motorcycle { Id = id });
        }

        public Task<bool> UpdateMotorcycleAsync(string id, MotorcycleUpdate motorcycle)
        {
            Console.WriteLine($"Atualizando placa da moto: {id}");

            //TODO: Implementar a atualização da placa da moto no banco de dados.
            return Task.FromResult(true);

        }
    }
}
