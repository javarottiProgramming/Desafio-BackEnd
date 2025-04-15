using AutoMapper;
using Desafio_BackEnd.Data.Repositories;
using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Entities;
using Desafio_BackEnd.Domain.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Desafio_BackEnd.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly ILogger<RentalService> _logger;
        private readonly IMapper _mapper;

        public RentalService(IRentalRepository rentalRepository, IDeliveryManRepository deliveryManRepository,
            IMapper mapper, ILogger<RentalService> logger)
        {
            _rentalRepository = rentalRepository;
            _deliveryManRepository = deliveryManRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateRentalAsync(CreateRentalModel createRentalModel)
        {
            try
            {
                if (await VerifyDeliveryManDriversLicense(createRentalModel.DeliveryManId))
                {
                    var rental = _mapper.Map<Rental>(createRentalModel);

                    return await _rentalRepository.CreateRentalAsync(rental);
                }
            }
            catch (Npgsql.PostgresException ex)
            {
                _logger.LogError($"Constraint {ex.ConstraintName} violation");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while creating a new rental: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Valida se o entregador cadastrado possui a categoria A na CNH para locação.
        /// </summary>
        /// <param name="deliveryManId"></param>
        /// <returns></returns>
        private async Task<bool> VerifyDeliveryManDriversLicense(string deliveryManId)
        {
            var deliveryMan = await _deliveryManRepository.GetDeliveryManByIdAsync(deliveryManId);
            if (deliveryMan == null)
            {
                _logger.LogWarning($"DeliveryMan not found. DeliveryManId: {deliveryManId}.");
                return false;
            }

            if (!deliveryMan.DriversLicenseCategory.ToUpper().Contains("A"))
            {
                string msg = $"Driver's category is not A. DeliveryManId: {deliveryManId}";
                _logger.LogWarning(msg);

                return false;
            }

            return true;
        }

        public async Task<RentalDto?> GetRentalByIdAsync(string id)
        {
            int rentalId = int.TryParse(id.Replace("locacao", ""), out int result) ? result : 0;

            if (rentalId == 0)
            {
                _logger.LogWarning($"Invalid rental ID: {id}");
                return null;
            }

            var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);

            if (rental == null)
            {
                return null;
            }

            var rentalDto = _mapper.Map<RentalDto>(rental);

            return rentalDto;
        }

        public async Task<bool> UpdateRentalReturnByIdAsync(string id, DateTime returnDate)
        {
            try
            {
                //TODO colocar no controller
                int rentalId = int.TryParse(id.Replace("locacao", ""), out int result) ? result : 0;

                if (rentalId == 0)
                {
                    _logger.LogWarning($"Invalid rental ID: {id}");
                    return false;
                }

                #region desmembrar
                var rental = await _rentalRepository.GetRentalByIdAsync(rentalId);

                if (rental == null)
                {
                    _logger.LogWarning($"Rental not found. id: {id}.");
                    return false;
                }


                decimal fine = 0;
                int diffDays, totalDays = 0;


                if (returnDate < rental.ExpectedEndDate)
                {
                    diffDays = (int)(rental.ExpectedEndDate - returnDate).TotalDays;
                    var dailyPlanValue = GetDailyPlanValue(rental.Plan);
                    var valueOfRemainingDaysPlan = dailyPlanValue * diffDays;

                    if (rental.Plan == 7)
                    {
                        fine = valueOfRemainingDaysPlan * (decimal)0.2;
                    }
                    else if (rental.Plan == 15)
                    {
                        fine = valueOfRemainingDaysPlan * (decimal)0.4;
                    }

                    totalDays = rental.Plan - diffDays;


                }
                else
                {
                    diffDays = (int)(returnDate - rental.ExpectedEndDate).TotalDays;
                    fine = (decimal)diffDays * 50;

                    totalDays = rental.Plan + diffDays;
                }

                #endregion

                rental.ReturnDate = returnDate;
                rental.DailyValue = CalculateDailyValueWithFine(rental.Plan, fine, totalDays);
                rental.UpdatedDate = DateTime.UtcNow;

                return await _rentalRepository.UpdateRentalReturnByIdAsync(rental);

            }
            catch (Npgsql.PostgresException ex)
            {
                _logger.LogError($"Constraint {ex.ConstraintName} violation");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating rental return date: {ex.Message}");
            }

            return false;
        }

        private decimal GetDailyPlanValue(int plan)
        {
            return plan switch
            {
                7 => 30.0m,
                15 => 28.0m,
                30 => 22.0m,
                45 => 20.0m,
                50 => 18.0m,
                _ => throw new ArgumentException("Plano inválido.")
            };
        }

        private decimal CalculateDailyValueWithFine(int plan, decimal fine, int totalDays)
        {
            var dailyPlanValue = GetDailyPlanValue(plan);

            var rentalTotalValue = (dailyPlanValue * plan) + fine;

            return rentalTotalValue / totalDays;
        }
    }
}