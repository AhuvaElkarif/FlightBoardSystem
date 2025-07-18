using FlightBoard.Domain.DTOs;

namespace FlightBoard.Application.Interfaces
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDto>> GetAllFlightsAsync();
        Task<FlightDto?> GetFlightByIdAsync(int id);
        Task<IEnumerable<FlightDto>> SearchFlightsAsync(FlightSearchDto searchDto);
        Task<FlightDto> AddFlightAsync(CreateFlightDto createFlightDto);
        Task<bool> DeleteFlightAsync(int id);
        Task<bool> FlightNumberExistsAsync(string flightNumber, int? excludeId = null);
    }
}
