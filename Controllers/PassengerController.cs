using BookingApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly BookingDbContext _dbContext;

        public PassengerController(BookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassengerById(Guid id)
        {
            var passenger = await _dbContext.Passengers.FindAsync(id);

            if (passenger == null)
                return NotFound();

            return Ok(passenger);
        }

        [HttpPost("booking/{bookingId}")]
        public async Task<ActionResult<Passenger>> AddPassengerToBooking(Guid bookingId, Passenger newPassenger)
        {
            var booking = await _dbContext.Bookings.FindAsync(bookingId);

            if (booking == null)
                return NotFound();

            newPassenger.Id = Guid.NewGuid();
            newPassenger.BookingId = bookingId;

            _dbContext.Passengers.Add(newPassenger);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPassengerById), new { id = newPassenger.Id }, newPassenger);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePassenger(Guid id, Passenger updatedPassenger)
        {
            var passenger = await _dbContext.Passengers.FindAsync(id);

            if (passenger == null)
                return NotFound();

            passenger.FirstName = updatedPassenger.FirstName;
            passenger.LastName = updatedPassenger.LastName;
            passenger.Email = updatedPassenger.Email;
            passenger.PhoneNumber = updatedPassenger.PhoneNumber;
            passenger.DateOfBirth = updatedPassenger.DateOfBirth;
            passenger.PassportNumber = updatedPassenger.PassportNumber;
            passenger.PassportExpiryDate = updatedPassenger.PassportExpiryDate;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(Guid id)
        {
            var passenger = await _dbContext.Passengers.FindAsync(id);

            if (passenger == null)
                return NotFound();

            _dbContext.Passengers.Remove(passenger);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
