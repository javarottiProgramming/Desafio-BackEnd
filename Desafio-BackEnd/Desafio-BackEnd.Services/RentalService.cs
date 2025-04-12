using Desafio_BackEnd.Domain.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<bool> CreateRentalAsync(RentalDto rental)
        {
            //TODO Implementar busca em repositorio para verificar se o entregador possui carteira A
            //Somente entregadores habilitados na categoria A podem efetuar uma locação

            var dailyValue = rental.DailyValue;

            Console.WriteLine(dailyValue);



            return await Task.FromResult(true);

        }
        public async Task<RentalReturnDto> GetRentalByIdAsync(string id)
        {

            var all = await _rentalRepository.GetAllRentalsAsync();

            return await Task.FromResult(new RentalReturnDto());

        }

        public async Task<bool> SendRentalReturnByIdAsync(string id, RentalReturnDate rentalReturnDate)
        {
            /*
             
Eu como entregador quero informar a data que irei devolver a moto e consultar o valor total da locação.

Quando a data informada for inferior a data prevista do término, será cobrado o valor das diárias e uma multa adicional
    Para plano de 7 dias o valor da multa é de 20% sobre o valor das diárias não efetivadas.
    Para plano de 15 dias o valor da multa é de 40% sobre o valor das diárias não efetivadas.
Quando a data informada for superior a data prevista do término, será cobrado um valor adicional de R$50,00 por diária adicional.
             */

            return await Task.FromResult(true);
        }
    }

}
