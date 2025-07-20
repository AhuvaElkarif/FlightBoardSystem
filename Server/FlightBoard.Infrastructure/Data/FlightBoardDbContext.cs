using FlightBoard.Domain.Entities;
using FlightBoard.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FlightBoard.Infrastructure.Data
{
    public class FlightBoardDbContext : DbContext
    {
        public FlightBoardDbContext(DbContextOptions<FlightBoardDbContext> options) : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FlightConfiguration());
            modelBuilder.Entity<Flight>().HasData(SeedFlights());
        }

        private static IEnumerable<Flight> SeedFlights()
        {
            var now = DateTime.Now;
            return new[]
            {
                new Flight
                {
                    Id = 1,
                    FlightNumber = "AA101",
                    Destination = "New York",
                    DepartureTime = now.AddHours(2),
                    Gate = "A1",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Flight
                {
                    Id = 2,
                    FlightNumber = "BA202",
                    Destination = "London",
                    DepartureTime = now.AddMinutes(15),
                    Gate = "B2",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Flight
                {
                    Id = 3,
                    FlightNumber = "DL303",
                    Destination = "Paris",
                    DepartureTime = now.AddMinutes(-30),
                    Gate = "C3",
                    CreatedAt = now,
                    UpdatedAt = now
                }
            };
        }
    }
}
