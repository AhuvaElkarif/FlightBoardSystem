using FlightBoard.Domain.Entities;
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
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FlightNumber).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Destination).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Gate).IsRequired().HasMaxLength(10);
                entity.Property(e => e.DepartureTime).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();

                entity.HasIndex(e => e.FlightNumber).IsUnique();
                entity.HasIndex(e => e.DepartureTime);
            });

            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    Id = 1,
                    FlightNumber = "AA101",
                    Destination = "New York",
                    DepartureTime = DateTime.Now.AddHours(2),
                    Gate = "A1",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Flight
                {
                    Id = 2,
                    FlightNumber = "BA202",
                    Destination = "London",
                    DepartureTime = DateTime.Now.AddMinutes(15),
                    Gate = "B2",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Flight
                {
                    Id = 3,
                    FlightNumber = "DL303",
                    Destination = "Paris",
                    DepartureTime = DateTime.Now.AddMinutes(-30),
                    Gate = "C3",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );
        }
    }
}
