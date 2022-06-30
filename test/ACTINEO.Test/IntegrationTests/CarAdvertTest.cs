using ACTINEO.Service.Dtos;
using ACTINEO.Test.Base;
using ACTINEO.Web;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;

namespace ACTINEO.Test.IntegrationTests {

    public class CarAdvertTest {

        private HttpClient _client;

        [OneTimeSetUp]
        public void OnTimeSetup() {
            var factory = new AppFactory<Startup>();
            _client = factory.CreateClient();
        }

        [Test]
        public async Task It_should_create_and_get_a_car_advert() {
            var carAdvert = new CarAdvertDto {
                Title = "Audi A4 Avant",
                Fuel = Core.Entities.FuelType.Diesel,
                Price = 12233,
                IsNew = false,
                Mileage = 120000,
                FirstRegistrationDate = DateTime.Now
            };

            var response = await _client.PostAsync("/api/caradverts", carAdvert);
            response.EnsureSuccessStatusCode();

            var dto = await _client.GetAsync<CarAdvertDto>(response.Headers.Location.ToString());

            dto.Should().BeEquivalentTo(carAdvert, c => c.Excluding(e => e.Id).Excluding(e => e.FirstRegistrationDate));
        }

    }
}
