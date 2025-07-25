using BookingApi.Data;
using BookingApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingDbContext _dbContext;

        public BookingController(BookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Booking>>> GetBookings()
        {
            return Ok(await _dbContext.Bookings.Include(b => b.Passengers).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(Guid id)
        {
            var booking = await _dbContext.Bookings.Include(b => b.Passengers).FirstOrDefaultAsync(b => b.Id == id);
            if (booking is null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // TODO: Is it desired to create the booking, then add passengers, or to add them when creating the booking itself?
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking()
        {
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Status = BookingStatus.Pending
            };

            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(Guid id, Booking updatedBooking)
        {
            if (updatedBooking == null)
                return BadRequest();

            var booking = await _dbContext.Bookings
                .Include(b => b.Passengers)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound();

            // Booking is not confirmed without at least one passenger
            // TODO: Is this desired behavior?
            if (updatedBooking.Status == BookingStatus.Confirmed &&
                (booking.Passengers == null || booking.Passengers.Count == 0))
            {
                return BadRequest();
            }

            booking.Status = updatedBooking.Status;
            booking.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var booking = await _dbContext.Bookings.Include(b => b.Passengers).FirstOrDefaultAsync(b => b.Id == id);
            if (booking is null)
            {
                return NotFound();
            }

            _dbContext.Bookings.Remove(booking);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/passport-details")]
        public async Task<ActionResult<List<PassengerPassportDto>>> GetPassportDetails(Guid id)
        {
            var booking = await _dbContext.Bookings
                .Include(b => b.Passengers)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking is null)
                return NotFound();

            var passportDetails = booking.Passengers.Select(p => new PassengerPassportDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                PassportNumber = p.PassportNumber,
                PassportExpiryDate = p.PassportExpiryDate
            }).ToList();

            return Ok(passportDetails);
        }
    }
}
