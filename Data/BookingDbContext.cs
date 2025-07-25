using Microsoft.EntityFrameworkCore;

namespace BookingApi.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Passenger> Passengers => Set<Passenger>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .Property(b => b.Status)
                .HasConversion<string>();

            // Seeding example data for testing purposes
            var bookingId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var bookingId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var passengerId1 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var passengerId2 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var passengerId3 = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

            var createdAt1 = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var createdAt2 = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = bookingId1,
                    CreatedAt = createdAt1,
                },
                new Booking
                {
                    Id = bookingId2,
                    CreatedAt = createdAt2,
                }
            );

            modelBuilder.Entity<Passenger>().HasData(
                new Passenger
                {
                    Id = passengerId1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "123-456-7890",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PassportNumber = "X12345678",
                    PassportExpiryDate = new DateTime(2030, 1, 1),
                    BookingId = bookingId1
                },
                new Passenger
                {
                    Id = passengerId2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@example.com",
                    PhoneNumber = "098-765-4321",
                    DateOfBirth = new DateTime(1991, 1, 1),
                    PassportNumber = "Y98765432",
                    PassportExpiryDate = new DateTime(2031, 1, 1),
                    BookingId = bookingId1
                },
                new Passenger
                {
                    Id = passengerId3,
                    FirstName = "Joe",
                    LastName = "Schmoe",
                    Email = "joe.schmoe@example.com",
                    PhoneNumber = "321-654-0987",
                    DateOfBirth = new DateTime(1992, 1, 1),
                    PassportNumber = "Z12345678",
                    PassportExpiryDate = new DateTime(2032, 1, 1),
                    BookingId = bookingId2
                }
            );
        }
    }
}
