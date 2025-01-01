using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using truedrive_backend.Data;
using truedrive_backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace truedrive_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly TrueDriveContext _context;

        public WishlistController(TrueDriveContext context)
        {
            _context = context;
        }

        // GET: api/Wishlist/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetWishlistByUserId(int userId)
        {
            var wishlist = await _context.Wishlist
                .Where(w => w.UserId == userId)
                .Include(w => w.Car)
                .ToListAsync();

            if (wishlist == null || wishlist.Count == 0)
            {
                return NotFound();
            }

            var cars = wishlist.Select(w => w.Car).ToList();
            return Ok(cars);
        }

        // POST: api/Wishlist
        [HttpPost]
        public async Task<ActionResult<Wishlist>> PostWishlist(Wishlist wishlist)
        {
            _context.Wishlist.Add(wishlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWishlistByUserId), new { userId = wishlist.UserId }, wishlist);
        }

        // POST: api/Wishlist/add
        [HttpPost("add")]
        public async Task<ActionResult> AddToWishlist(int userId, int carId)
        {
            var wishlistItem = new Wishlist
            {
                UserId = userId,
                CarId = carId
            };

            _context.Wishlist.Add(wishlistItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Wishlist/remove
        [HttpDelete("remove")]
        public async Task<ActionResult> RemoveFromWishlist(int userId, int carId)
        {
            var wishlistItem = await _context.Wishlist
                .FirstOrDefaultAsync(w => w.UserId == userId && w.CarId == carId);

            if (wishlistItem == null)
            {
                return NotFound();
            }

            _context.Wishlist.Remove(wishlistItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Wishlist/exists
        [HttpGet("exists")]
        public async Task<ActionResult<bool>> IsCarInWishlist(int userId, int carId)
        {
            var exists = await _context.Wishlist
                .AnyAsync(w => w.UserId == userId && w.CarId == carId);

            return Ok(exists);
        }

        // DELETE: api/Wishlist/removeCar/1
        [HttpDelete("removeCar/{carId}")]
        public async Task<ActionResult> RemoveCarFromAllWishlists(int carId)
        {
            var wishlistItems = await _context.Wishlist
                .Where(w => w.CarId == carId)
                .ToListAsync();

            if (wishlistItems == null || wishlistItems.Count == 0)
            {
                return NotFound();
            }

            _context.Wishlist.RemoveRange(wishlistItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
