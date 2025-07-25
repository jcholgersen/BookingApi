using System.ComponentModel.DataAnnotations;

namespace BookingApi
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }

    public class Booking
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } 

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public List<Passenger> Passengers { get; set; } = new List<Passenger>();

    }
}
