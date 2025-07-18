using FlightBoard.Domain.Enums;

namespace FlightBoard.Domain.DTOs
{
    public record FlightDto(
     int Id,
     string FlightNumber,
     string Destination,
     DateTime DepartureTime,
     string Gate,
     FlightStatus Status,
     DateTime CreatedAt,
     DateTime UpdatedAt
 );
}
