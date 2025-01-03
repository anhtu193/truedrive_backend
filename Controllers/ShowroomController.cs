using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using truedrive_backend.Data;
using truedrive_backend.Models;

namespace truedrive_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowroomController : ControllerBase
    {
        private readonly TrueDriveContext _context;

        public ShowroomController(TrueDriveContext context)
        {
            _context = context;
        }

        // GET: api/Showroom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Showroom>>> GetShowrooms()
        {
            return await _context.Showroom.ToListAsync();
        }

        // GET: api/Showroom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Showroom>> GetShowroom(int id)
        {
            var showroom = await _context.Showroom.FindAsync(id);

            if (showroom == null)
            {
                return NotFound();
            }

            return showroom;
        }

        // POST: api/Showroom
        [HttpPost]
        public async Task<ActionResult<Showroom>> PostShowroom(Showroom showroom)
        {
            _context.Showroom.Add(showroom);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShowroom), new { id = showroom.ShowroomId }, showroom);
        }

        // PUT: api/Showroom/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShowroom(int id, Showroom showroom)
        {
            if (id != showroom.ShowroomId)
            {
                return BadRequest();
            }

            _context.Entry(showroom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowroomExists(id))
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

        // DELETE: api/Showroom/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowroom(int id)
        {
            var showroom = await _context.Showroom.FindAsync(id);
            if (showroom == null)
            {
                return NotFound();
            }

            _context.Showroom.Remove(showroom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShowroomExists(int id)
        {
            return _context.Showroom.Any(e => e.ShowroomId == id);
        }
    }
}
