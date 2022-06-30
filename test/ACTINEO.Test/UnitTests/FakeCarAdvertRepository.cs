using ACTINEO.Core;
using ACTINEO.Core.Data;
using ACTINEO.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTINEO.Test.UnitTests {
    public class FakeCarAdvertRepository : ICarAdvertRepository {
        private readonly Dictionary<int, CarAdvert> _db = new Dictionary<int, CarAdvert>();
        private int lastId = 0;

        public Task DeleteAsync(CarAdvert entity) {
            _db.Remove(entity.Id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<CarAdvert>> GetAllAsync(SortOptions sortOptions) {
            return Task.FromResult<IEnumerable<CarAdvert>>(_db.Values);
        }

        public Task<CarAdvert> GetByIdAsync(int id) {
            try {
                var entity = _db[id];
                return Task.FromResult(entity);
            }
            catch (Exception) {

               return Task.FromResult<CarAdvert>(null);
            }
           
        }

        public Task InsertAsync(CarAdvert entity) {
            entity.GetType().GetProperty("Id").SetValue(entity, ++lastId);
            _db.Add(entity.Id, entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(CarAdvert entity) {
            _db[entity.Id] = entity;
            return Task.CompletedTask;
        }
    }
}
