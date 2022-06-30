using ACTINEO.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTINEO.Service.Dtos {
    public class CarAdvertDto {
        public int Id { get; set; }
        public string Title { get; set; }
        public FuelType Fuel { get; set; }
        public int Price { get; set; }
        public bool IsNew { get; set; }
        public int Mileage { get; set; }
        public DateTime? FirstRegistrationDate { get; set; }
        public string FuelName => Fuel.ToString();
    }
}
