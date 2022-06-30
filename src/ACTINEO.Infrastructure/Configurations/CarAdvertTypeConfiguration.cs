using ACTINEO.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACTINEO.Infrastructure.Configurations {
    public class CarAdvertTypeConfiguration : IEntityTypeConfiguration<CarAdvert> {
        public void Configure(EntityTypeBuilder<CarAdvert> builder) {
            
            builder
                .Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            builder
                .Property(e => e.Title)
                .HasColumnName("title")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(e => e.Fuel)
                .HasColumnName("fuel")
                .IsRequired();

            builder
                .Property(e => e.IsNew)
                .HasColumnName("new")
                .IsRequired();

            builder
               .Property(e => e.Price)
               .HasColumnName("price")
               .IsRequired();

            builder
               .Property(e => e.Mileage)
               .HasColumnName("mileage");

            builder
             .Property(e => e.FirstRegistrationDate)
             .HasColumnName("first_registration_date");


        }
    }
}
