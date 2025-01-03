using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using truedrive_backend.Data;
using truedrive_backend.Models;

namespace truedrive_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly TrueDriveContext _context;

        public CarController(TrueDriveContext context)
        {
            _context = context;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars([FromQuery] int? limit)
        {
            var query = _context.Car.AsQueryable();

            if (limit.HasValue)
            {
                query = query.Take(limit.Value);
            }

            return await query.ToListAsync();
        }

        // GET: api/Car/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCars([FromQuery] string query, [FromQuery] int? catalogId, [FromQuery] int? makeId)
        {
            if (string.IsNullOrEmpty(query) && !catalogId.HasValue && !makeId.HasValue)
            {
                return BadRequest("At least one search parameter must be provided.");
            }

            var cars = _context.Car.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                cars = cars.Where(c => c.Model.Contains(query));
            }

            if (catalogId.HasValue)
            {
                cars = cars.Where(c => c.CatalogId == catalogId.Value);
            }

            if (makeId.HasValue)
            {
                cars = cars.Where(c => c.MakeId == makeId.Value);
            }

            var result = await cars.ToListAsync();

            return Ok(result);
        }

        // GET: api/Car/catalog/{catalogId}
        [HttpGet("catalog/{catalogId}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCarsByCatalogId(int catalogId)
        {
            var cars = await _context.Car.Where(c => c.CatalogId == catalogId).ToListAsync();

            if (cars == null || cars.Count == 0)
            {
                return NotFound();
            }

            return Ok(cars);
        }

        // POST: api/Car
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCar), new { id = car.CarId }, car);
        }

        // PUT: api/Car/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Car/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeCarStatus(int id, [FromBody] ChangeStatusRequest request)
        {
            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            car.Status = request.Status;
            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: api/Car/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCarsByStatus(string status)
        {
            var cars = await _context.Car.Where(c => c.Status == status).ToListAsync();

            if (cars == null || cars.Count == 0)
            {
                return NotFound();
            }

            return Ok(cars);
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.CarId == id);
        }
    }

    public class ChangeStatusRequest
    {
        public string Status { get; set; }
    }
}
