using ACTINEO.Infrastructure.Repositories;
using ACTINEO.Service;
using ACTINEO.Service.Dtos;
using ACTINEO.Web.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ACTINEO.Core;
using ACTINEO.Core.Entities;

namespace ACTINEO.Test.UnitTests {

    public class CarAdvertControllerTest {

        private ICarAdvertRepository _carAdvertRepo;
        private ICarAdvertService _carAdvertService;
        private CarAdvertsController _carAdvertsController;

        [SetUp]
        public void Setup() {
            _carAdvertRepo = new FakeCarAdvertRepository();
            _carAdvertService = new CarAdvertService(_carAdvertRepo);
            _carAdvertsController = new CarAdvertsController(_carAdvertService);
        }

        [Test]
        public async Task It_should_create_a_new_car_advert() {
            // arrange
            var carAdvert = new CarAdvertDto { Title = "BMW", Fuel = FuelType.Diesel, IsNew = true, Price = 800000 };

            // act
            var actualResult = await _carAdvertsController.PostCarAdvert(carAdvert);

            // assert
            actualResult.Should().BeOfType<CreatedAtActionResult>()
                .Which.Should().BeEquivalentTo(new CreatedAtActionResult(nameof(CarAdvertsController.GetCarAdvert), null, new { id = 1 }, 1));

            var entity = await _carAdvertRepo.GetByIdAsync(1);
            entity.Should().BeEquivalentTo(carAdvert, c => c.Excluding(e => e.Id).Excluding(e => e.FuelName));
        }

        [TestCase(0)]
        [TestCase(-5)]
        public async Task It_should_return_error_when_mileage_is_zero_or_less_for_an_used_car(int mileage) {
            // arrange
            var carAdvert = new CarAdvertDto { Title = "BMW", Fuel = FuelType.Diesel, IsNew = false, Mileage = mileage, FirstRegistrationDate = DateTime.Now, Price = 800000 };

            // act
            var actualResult = await _carAdvertsController.PostCarAdvert(carAdvert);

            // assert
            actualResult.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be("The mileage for used cars cannot be less or equal zero.");
        }

        [Test]
        public async Task It_should_not_store_the_time_of_first_registration_date() {
            // arrange
            var carAdvert = new CarAdvertDto { Title = "BMW", Fuel = FuelType.Diesel, IsNew = false, Mileage = 100, FirstRegistrationDate = new DateTime(2022, 6, 20, 14, 30, 0), Price = 800000 };

            // act
            var actualResult = await _carAdvertsController.PostCarAdvert(carAdvert);

            // assert
            var entity = await _carAdvertRepo.GetByIdAsync(1);
            entity.FirstRegistrationDate.Value.TimeOfDay.Should().Be(TimeSpan.Zero);
        }

        [Test]
        public async Task It_should_return_a_car_advert() {
            // arrange
            var carAdvert = new CarAdvert("BMW", FuelType.Diesel, 12200, false, 800000, DateTime.Now);
            await _carAdvertRepo.InsertAsync(carAdvert);

            // act
            var actualResult = await _carAdvertsController.GetCarAdvert(carAdvert.Id);

            // assert
            actualResult.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(carAdvert);
        }

        [Test]
        public async Task It_should_update_an_existing_car_advert() {
            // arrange
            var oldTitle = "BMW";
            var newTitle = "Quick";

            var carAdvert = new CarAdvert(oldTitle, FuelType.Diesel, 12200, false, 800000, DateTime.Now);
            await _carAdvertRepo.InsertAsync(carAdvert);

            var oldCarAdvert = await _carAdvertRepo.GetByIdAsync(1);
            var newCarAdvertDto = new CarAdvertDto() {
                Id = oldCarAdvert.Id,
                Title = newTitle,
                Fuel = oldCarAdvert.Fuel,
                FirstRegistrationDate = oldCarAdvert.FirstRegistrationDate,
                Price = oldCarAdvert.Price,
                IsNew = oldCarAdvert.IsNew,
                Mileage = oldCarAdvert.Mileage
            };

            // act
            var actualResult = await _carAdvertsController.PutCarAdvert(1, newCarAdvertDto);

            // assert
            actualResult.Should().BeOfType<NoContentResult>();

            var entity = await _carAdvertRepo.GetByIdAsync(carAdvert.Id);
            entity.Should().BeEquivalentTo(new CarAdvertDto() {
                Id = oldCarAdvert.Id,
                Title = newTitle,
                Fuel = oldCarAdvert.Fuel,
                FirstRegistrationDate = oldCarAdvert.FirstRegistrationDate,
                Price = oldCarAdvert.Price,
                IsNew = oldCarAdvert.IsNew,
                Mileage = oldCarAdvert.Mileage
            }, c => c.Excluding(e => e.FuelName));
        }

        [Test]
        public async Task It_should_delete_an_existing_car_advert() {
            // arrange
            var carAdvert = new CarAdvert("BMW", FuelType.Diesel, 12200, false, 800000, DateTime.Now);
            await _carAdvertRepo.InsertAsync(carAdvert);

            // act
            var actualResult = await _carAdvertsController.DeleteCarAdvert(1);

            // assert
            actualResult.Should().BeOfType<NoContentResult>();

            var entity = await _carAdvertRepo.GetByIdAsync(1);
            entity.Should().BeNull();
        }

        [Test]
        public async Task It_should_return_all_car_adverts() {
            // arrange
            var carAdvert = new CarAdvert("BMW", FuelType.Diesel, 12200, false, 800000, DateTime.Now);
            await _carAdvertRepo.InsertAsync(carAdvert);

            // act
            var actualResult = await _carAdvertsController.GetCarAdverts(null);

            // assert
            actualResult.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<List<CarAdvertDto>>()
                .Which.Should().HaveCount(1);
        }

    }

}
