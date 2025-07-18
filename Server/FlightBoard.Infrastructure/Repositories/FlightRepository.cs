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
            _logger.LogDebug("Retrieving all flights from database");
            return await _context.Flights
                .OrderBy(f => f.DepartureTime)
                .ToListAsync();
        }

        public async Task<Flight?> GetByIdAsync(int id)
        {
            _logger.LogDebug("Retrieving flight with ID: {FlightId}", id);
            return await _context.Flights.FindAsync(id);
        }

        public async Task<Flight?> GetByFlightNumberAsync(string flightNumber)
        {
            _logger.LogDebug("Retrieving flight with number: {FlightNumber}", flightNumber);
            return await _context.Flights
                .FirstOrDefaultAsync(f => f.FlightNumber == flightNumber);
        }

        public async Task<IEnumerable<Flight>> SearchAsync(string? status = null, string? destination = null)
        {
            _logger.LogDebug("Searching flights with status: {Status}, destination: {Destination}", status, destination);

            var query = _context.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(destination))
            {
                query = query.Where(f => f.Destination.ToLower().Contains(destination.ToLower().Trim()));
            }

            var flights = await query.OrderBy(f => f.DepartureTime).ToListAsync();

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<FlightStatus>(status, true, out var statusEnum))
            {
                var currentTime = DateTime.Now;
                flights = flights.Where(f => CalculateFlightStatus(f.DepartureTime, currentTime) == statusEnum).ToList();
            }

            return flights;
        }

        public async Task<Flight> AddAsync(Flight flight)
        {
            _logger.LogDebug("Adding new flight: {FlightNumber}", flight.FlightNumber);

            flight.CreatedAt = DateTime.Now;
            flight.UpdatedAt = DateTime.Now;

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            return flight;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogDebug("Deleting flight with ID: {FlightId}", id);

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
            _logger.LogDebug("Checking if flight number exists: {FlightNumber}", flightNumber);

            var query = _context.Flights.Where(f => f.FlightNumber == flightNumber);

            if (excludeId.HasValue)
            {
                query = query.Where(f => f.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        private static FlightStatus CalculateFlightStatus(DateTime departureTime, DateTime currentTime)
        {
            var timeDifference = departureTime - currentTime;

            if (timeDifference.TotalMinutes > 30)
            {
                return FlightStatus.Scheduled;
            }

            if (timeDifference.TotalMinutes > 0)
            {
                return FlightStatus.Boarding;
            }

            if (Math.Abs(timeDifference.TotalMinutes) <= 60)
            {
                return FlightStatus.Departed;
            }

            return FlightStatus.Landed;
        }
    }
}
