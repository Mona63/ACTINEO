using ACTINEO.Core.Data;
using System;

namespace ACTINEO.Core.Entities {
    public class CarAdvert : IEntity {
        private CarAdvert() { }

        public CarAdvert(string title, FuelType fuel, int price, bool isNew, int mileage = 0, DateTime? firstRegistrationDate = null, int id = 0) {
            if (isNew) {
                if (mileage > 0) {
                    throw new ValidationException("The mileage for new cars cannot be more than zero.");
                }

                if (firstRegistrationDate != null) {
                    throw new ValidationException("New cars should not have firstRegistrationDate parameter.");
                }
            }
            else {
                if (mileage <= 0) {
                    throw new ValidationException("The mileage for used cars cannot be less or equal zero.");
                }

                if (firstRegistrationDate is null) {
                    throw new ValidationException("Used cars should have firstRegistrationDate parameter.");
                }

                Mileage = mileage;
                FirstRegistrationDate = firstRegistrationDate.Value.Date;
            }
            Id = id;
            Title = title;
            Fuel = fuel;
            Price = price;
            IsNew = isNew;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public FuelType Fuel { get; private set; }
        public int Price { get; private set; }
        public bool IsNew { get; private set; }
        public int Mileage { get; private set; }
        public DateTime? FirstRegistrationDate { get; private set; }

    }
}
