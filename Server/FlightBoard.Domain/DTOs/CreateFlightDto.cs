namespace FlightBoard.Domain.DTOs
{
    public record CreateFlightDto(
     string FlightNumber,
     string Destination,
     DateTime DepartureTime,
     string Gate
 );
}
