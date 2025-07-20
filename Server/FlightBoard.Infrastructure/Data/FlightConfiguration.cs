using FlightBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightBoard.Infrastructure.Data.Configurations
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FlightNumber)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.Destination)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Gate)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.DepartureTime).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();

            entity.HasIndex(e => e.FlightNumber).IsUnique();
            entity.HasIndex(e => e.DepartureTime);
        }
    }
}
