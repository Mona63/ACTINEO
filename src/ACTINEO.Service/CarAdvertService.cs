using ACTINEO.Core;
using ACTINEO.Core.Data;
using ACTINEO.Core.Entities;
using ACTINEO.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACTINEO.Service {
    public class CarAdvertService : ICarAdvertService {
        private readonly ICarAdvertRepository _carAdvertRepository;

        public CarAdvertService(ICarAdvertRepository carAdvertRepository) {
            _carAdvertRepository = carAdvertRepository;
        }

        public async Task<CarAdvertDto> AddCarAdvertAsync(CarAdvertDto carAdvertDto) {

            var carAdvert = new CarAdvert(carAdvertDto.Title, carAdvertDto.Fuel, carAdvertDto.Price, carAdvertDto.IsNew, carAdvertDto.Mileage, carAdvertDto.FirstRegistrationDate);
            await _carAdvertRepository.InsertAsync(carAdvert);

            return await GetCarAdvertAsync(carAdvert.Id);
        }

        public async Task<CarAdvertDto> GetCarAdvertAsync(int id) {
            var carAdvert = await _carAdvertRepository.GetByIdAsync(id);
            if (carAdvert is null) return null;

            return new CarAdvertDto { Id = carAdvert.Id, Title = carAdvert.Title, Fuel = carAdvert.Fuel, Price = carAdvert.Price, IsNew = carAdvert.IsNew, Mileage = carAdvert.Mileage, FirstRegistrationDate = carAdvert.FirstRegistrationDate };
        }

        public async Task<List<CarAdvertDto>> GetCarAdvertsAsync(SortOptions sortOptions) {
            var carAdverts = await _carAdvertRepository.GetAllAsync(sortOptions);
            var carAdvertDtos = carAdverts.Select(ca => new CarAdvertDto {
                Id = ca.Id,
                Title = ca.Title,
                Price = ca.Price,
                FirstRegistrationDate = ca.FirstRegistrationDate,
                IsNew = ca.IsNew,
                Mileage = ca.Mileage,
                Fuel = ca.Fuel
            });
            return carAdvertDtos.ToList();
        }

        public async Task DeleteCarAdvertAsync(int id) {
            var carAdvert = await _carAdvertRepository.GetByIdAsync(id);
            await _carAdvertRepository.DeleteAsync(carAdvert);
        }

        public async Task UpdateCarAdvertAsync(CarAdvertDto carAdvertDto) {
            var carAdvert = new CarAdvert(carAdvertDto.Title, carAdvertDto.Fuel, carAdvertDto.Price, carAdvertDto.IsNew, carAdvertDto.Mileage, carAdvertDto.FirstRegistrationDate, carAdvertDto.Id);
            await _carAdvertRepository.UpdateAsync(carAdvert);
        }

    }
}
