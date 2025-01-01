using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using truedrive_backend.Data;
using truedrive_backend.Models;

namespace truedrive_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeController : ControllerBase
    {
        private readonly TrueDriveContext _context;

        public MakeController(TrueDriveContext context)
        {
            _context = context;
        }

        // GET: api/Make
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        {
            return await _context.Make.ToListAsync();
        }

        // GET: api/Make/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Make>> GetMake(int id)
        {
            var make = await _context.Make.FindAsync(id);

            if (make == null)
            {
                return NotFound();
            }

            return make;
        }

        // POST: api/Make
        [HttpPost]
        public async Task<ActionResult<Make>> PostMake(Make make)
        {
            _context.Make.Add(make);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMake), new { id = make.MakeId }, make);
        }

        // PUT: api/Make/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMake(int id, Make make)
        {
            if (id != make.MakeId)
            {
                return BadRequest();
            }

            _context.Entry(make).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakeExists(id))
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

        // DELETE: api/Make/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            var make = await _context.Make.FindAsync(id);
            if (make == null)
            {
                return NotFound();
            }

            _context.Make.Remove(make);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MakeExists(int id)
        {
            return _context.Make.Any(e => e.MakeId == id);
        }
    }
}
