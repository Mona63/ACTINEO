using ACTINEO.Core;
using ACTINEO.Core.Data;
using ACTINEO.Core.Entities;
using ACTINEO.Infrastructure;
using ACTINEO.Service;
using ACTINEO.Service.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACTINEO.Web.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CarAdvertsController : ControllerBase {
        private readonly ICarAdvertService _carAdvertService;

        public CarAdvertsController(ICarAdvertService carService) {
            _carAdvertService = carService;
        }

        // GET: api/CarAdverts
        [HttpGet]
        public async Task<IActionResult> GetCarAdverts([FromQuery] SortOptions sortOptions) {
            return Ok(await _carAdvertService.GetCarAdvertsAsync(sortOptions));
        }

        // GET: api/CarAdverts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarAdvert(int id) {
            var carAdvert = await _carAdvertService.GetCarAdvertAsync(id);

            if (carAdvert is null) {
                return NotFound();
            }

            return Ok(carAdvert);
        }

        // PUT: api/CarAdverts/5 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarAdvert(int id, CarAdvertDto carAdvert) {
            if (id != carAdvert.Id) {
                return BadRequest();
            }

            await _carAdvertService.UpdateCarAdvertAsync(carAdvert);
            return NoContent();
        }

        // POST: api/CarAdverts
        [HttpPost]
        public async Task<IActionResult> PostCarAdvert(CarAdvertDto carAdvert) {
            try {
                var dto = await _carAdvertService.AddCarAdvertAsync(carAdvert);

                return CreatedAtAction(nameof(GetCarAdvert), new { id = dto.Id }, dto.Id);
            }
            catch (ValidationException ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/CarAdverts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarAdvert(int id) {
            await _carAdvertService.DeleteCarAdvertAsync(id);

            return NoContent();
        }


    }
}
