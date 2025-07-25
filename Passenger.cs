using System.ComponentModel.DataAnnotations;

namespace BookingApi
{
    public class Passenger
    {
        public Guid Id { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public required string PassportNumber { get; set; }

        public DateTime PassportExpiryDate { get; set; }

        [Required]
        public Guid BookingId { get; set; }

        public Booking? Booking { get; set; }
    }
}