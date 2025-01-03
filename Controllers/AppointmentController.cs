using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using truedrive_backend.Data;
using truedrive_backend.Models;

namespace truedrive_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly TrueDriveContext _context;

        public AppointmentController(TrueDriveContext context)
        {
            _context = context;
        }

        // GET: api/Appointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointment.ToListAsync();
        }

        // GET: api/Appointment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByUserId(int userId)
        {
            var appointments = await _context.Appointment
                .Where(a => a.CustomerId == userId)
                .ToListAsync();

            if (appointments == null || appointments.Count == 0)
            {
                return NotFound();
            }

            return Ok(appointments);
        }

        // POST: api/Appointment
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.AppointmentId }, appointment);
        }

        // PUT: api/Appointment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            var existingAppointment = await _context.Appointment.FindAsync(id);
            if (existingAppointment == null)
            {
                return NotFound();
            }

            // Update only the provided properties
            if (appointment.CustomerId != 0)
            {
                existingAppointment.CustomerId = appointment.CustomerId;
            }
            if (appointment.CarId != 0)
            {
                existingAppointment.CarId = appointment.CarId;
            }
            if (!string.IsNullOrEmpty(appointment.Date))
            {
                existingAppointment.Date = appointment.Date;
            }
            if (!string.IsNullOrEmpty(appointment.Time))
            {
                existingAppointment.Time = appointment.Time;
            }
            if (!string.IsNullOrEmpty(appointment.Showroom))
            {
                existingAppointment.Showroom = appointment.Showroom;
            }
            if (!string.IsNullOrEmpty(appointment.Status))
            {
                existingAppointment.Status = appointment.Status;
            }
            if (!string.IsNullOrEmpty(appointment.Purpose))
            {
                existingAppointment.Purpose = appointment.Purpose;
            }
            if (!string.IsNullOrEmpty(appointment.Note))
            {
                existingAppointment.Note = appointment.Note;
            }

            _context.Entry(existingAppointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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


        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.AppointmentId == id);
        }
    }
}
