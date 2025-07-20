using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Enums;
using FlightBoard.Domain.Interfaces;
using FlightBoard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlightBoard.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightBoardDbContext _context;
        private readonly ILogger<FlightRepository> _logger;

        public FlightRepository(FlightBoardDbContext context, ILogger<FlightRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all flights from database");
            return await _context.Flights
                .AsNoTracking()
                .OrderBy(f => f.DepartureTime)
                .ToListAsync();
        }

        public async Task<Flight?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving flight with ID: {FlightId}", id);
            return await _context.Flights.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Flight?> GetByFlightNumberAsync(string flightNumber)
        {
            _logger.LogInformation("Retrieving flight with number: {FlightNumber}", flightNumber);
            return await _context.Flights
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.FlightNumber == flightNumber);
        }

        public async Task<IEnumerable<Flight>> SearchAsync(string? destination = null)
        {
            var query = _context.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(destination))
            {
                query = query.Where(f => f.Destination.ToLower().Contains(destination.ToLower().Trim()));
            }

            return await query.OrderBy(f => f.DepartureTime).ToListAsync();
        }

        public async Task<Flight> AddAsync(Flight flight)
        {
            _logger.LogInformation("Adding new flight: {FlightNumber}", flight.FlightNumber);

            flight.CreatedAt = DateTime.Now;
            flight.UpdatedAt = DateTime.Now;

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            return flight;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting flight with ID: {FlightId}", id);

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return false;
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> FlightNumberExistsAsync(string flightNumber, int? excludeId = null)
        {
            _logger.LogInformation("Checking if flight number exists: {FlightNumber}", flightNumber);

            var query = _context.Flights.Where(f => f.FlightNumber == flightNumber);

            if (excludeId.HasValue)
            {
                query = query.Where(f => f.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
