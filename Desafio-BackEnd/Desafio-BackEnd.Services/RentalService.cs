using AutoMapper;
using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;

namespace Desafio_BackEnd.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public RentalService(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateRentalAsync(CreateRentalModel rental)
        {
            //TODO Implementar busca em repositorio para verificar se o entregador possui carteira A
            //Somente entregadores habilitados na categoria A podem efetuar uma locação

            var dailyValue = rental.DailyValue;

            Console.WriteLine(dailyValue);



            return await Task.FromResult(true);

        }
        public async Task<RentalDto> GetRentalByIdAsync(string id)
        {

            var rental = await _rentalRepository.GetRentalByIdAsync(id);

            if (rental == null)
            {
                throw new Exception("Locação não encontrada");
            }

            var rentalDto = _mapper.Map<RentalDto>(rental);

            return rentalDto;

        }

        public async Task<bool> CreateRentalReturnByIdAsync(string id, RentalReturnDto rentalReturnDate)
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
