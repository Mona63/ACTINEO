using ACTINEO.Core.Entities;
using ACTINEO.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ACTINEO.Infrastructure {
    public class CarManagementContext : DbContext {
        public CarManagementContext() {
        }
        public CarManagementContext(DbContextOptions<CarManagementContext> options)
            : base(options) {
            Database.EnsureCreated();
        }
        public DbSet<CarAdvert> CarAdverts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            new CarAdvertTypeConfiguration().Configure(modelBuilder.Entity<CarAdvert>());
        }
        }
}
