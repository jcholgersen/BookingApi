namespace BookingApi.Dtos
{
    public class PassengerPassportDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;
        public DateTime PassportExpiryDate { get; set; }
    }
}
