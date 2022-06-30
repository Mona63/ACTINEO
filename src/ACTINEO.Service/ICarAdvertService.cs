using ACTINEO.Core.Data;
using ACTINEO.Core.Entities;
using ACTINEO.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTINEO.Service {
    public interface ICarAdvertService {
        Task<List<CarAdvertDto>> GetCarAdvertsAsync(SortOptions sortOptions);
        Task<CarAdvertDto> GetCarAdvertAsync(int id);
        Task UpdateCarAdvertAsync(CarAdvertDto carAdvertDto);
        Task<CarAdvertDto> AddCarAdvertAsync(CarAdvertDto carAdvertDto);
        Task DeleteCarAdvertAsync(int id);
    }
}
