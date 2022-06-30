using ACTINEO.Core.Data;
using ACTINEO.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACTINEO.Core {
    public interface ICarAdvertRepository {
        Task InsertAsync(CarAdvert entity);
        Task UpdateAsync(CarAdvert entity);
        Task DeleteAsync(CarAdvert entity);
        Task<IEnumerable<CarAdvert>> GetAllAsync(SortOptions sortOptions);
        Task<CarAdvert> GetByIdAsync(int id);
    }
}
