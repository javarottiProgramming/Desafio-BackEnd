using AutoMapper;
using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Desafio_BackEnd.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MotorcycleService> _logger;

        public MotorcycleService(IMotorcycleRepository motorcycleRepository, IMapper mapper, ILogger<MotorcycleService> logger)
        {
            _motorcycleRepository = motorcycleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateMotorcycleAsync(MotorcycleDto motorcycle)
        {
            try
            {
                var motorcycleEntity = _mapper.Map<Motorcycle>(motorcycle);

                var inserted = await _motorcycleRepository.CreateMotorcycleAsync(motorcycleEntity);

                return await Task.FromResult(inserted);
            }
            catch (Npgsql.PostgresException ex)
            {
                _logger.LogError($"Constraint {ex.ConstraintName} violation");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while creating motorcycle: {ex.Message}");
            }

            return false;

            //Quando a moto for cadastrada a aplicação deverá gerar um evento de moto cadastrada
            //Todo: Implementar o evento de moto cadastrada com trycatch.


        }

        public async Task<MotorcycleDto?> GetMotorcycleByPlateAsync(string plate)
        {
            try
            {
                var motorcycle = await _motorcycleRepository.GetMotorcycleByPlateAsync(plate);
                if (motorcycle == null)
                {
                    return null;
                }

                return _mapper.Map<MotorcycleDto>(motorcycle);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<MotorcycleDto?> GetMotorcycleByIdAsync(string id)
        {
            try
            {
                var motorcycle = await _motorcycleRepository.GetMotorcycleByIdAsync(id);
                if (motorcycle == null)
                {
                    return null;
                }

                return _mapper.Map<MotorcycleDto>(motorcycle);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateMotorcyclePlateByIdAsync(string id, string plate)
        {
            try
            {
                //TODO: Implementar a atualização da placa da moto no banco de dados.
                return await _motorcycleRepository.UpdateMotorcyclePlateByIdAsync(id, plate);
            }
            catch (Npgsql.PostgresException ex)
            {
                _logger.LogError($"Constraint {ex.ConstraintName} violation");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating motorcycle plate: {ex.Message}");
            }

            return false;
        }

        public async Task<bool> DeleteMotorcycleByIdAsync(string id)
        {
            try
            {
                return await _motorcycleRepository.DeleteMotorcycleByIdAsync(id);
            }
            catch (Npgsql.PostgresException ex)
            {
                _logger.LogError($"Constraint {ex.ConstraintName} violation");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when delete motorcycle: {ex.Message}");
            }

            return false;
        }
    }
}