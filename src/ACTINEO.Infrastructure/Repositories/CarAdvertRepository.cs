using ACTINEO.Core;
using ACTINEO.Core.Data;
using ACTINEO.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACTINEO.Infrastructure.Repositories {

    public class CarAdvertRepository : ICarAdvertRepository {

        private readonly CarManagementContext _context;
        private readonly DbSet<CarAdvert> DbSet;

        public CarAdvertRepository(CarManagementContext context) {
            _context = context;
            DbSet = _context.Set<CarAdvert>();
        }

        public async Task InsertAsync(CarAdvert entity) {
            await _context.CarAdverts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CarAdvert entity) {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CarAdvert entity) {
            _context.CarAdverts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarAdvert>> GetAllAsync(SortOptions sortOptions) {
            return await DbSet.AsQueryable().SortByFieldName(sortOptions.Column, sortOptions.Direction).ToListAsync();
        }

        public async Task<CarAdvert> GetByIdAsync(int id) {
            return await DbSet.AsQueryable().FirstOrDefaultAsync(c => c.Id == id);
        }


    }

}