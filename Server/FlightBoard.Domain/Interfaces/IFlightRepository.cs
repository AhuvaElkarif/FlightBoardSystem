using FlightBoard.Domain.Entities;

namespace FlightBoard.Domain.Interfaces
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<Flight?> GetByIdAsync(int id);
        Task<Flight?> GetByFlightNumberAsync(string flightNumber);
        Task<IEnumerable<Flight>> SearchAsync(string? status = null, string? destination = null);
        Task<Flight> AddAsync(Flight flight);
        Task<bool> DeleteAsync(int id);
        Task<bool> FlightNumberExistsAsync(string flightNumber, int? excludeId = null);
    }
}
