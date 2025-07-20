using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Interfaces;
using FlightBoard.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace FlightBoard.Infrastructure.Services
{
    public class FlightNotificationService : IFlightNotificationService
    {
        private readonly IHubContext<FlightBoardHub> _hubContext;
        private readonly ILogger<FlightNotificationService> _logger;

        public FlightNotificationService(
            IHubContext<FlightBoardHub> hubContext,
            ILogger<FlightNotificationService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task NotifyFlightAddedAsync(Flight flight)
        {
            _logger.LogInformation("Adding flight notification: {FlightNumber}", flight.FlightNumber);
            await _hubContext.Clients.All.SendAsync("FlightAdded", MapFlight(flight));
        }

        public async Task NotifyFlightDeletedAsync(int flightId)
        {
            _logger.LogInformation("Deleting flight notification: {FlightId}", flightId);
            await _hubContext.Clients.All.SendAsync("FlightDeleted", flightId);
        }

        public async Task NotifyFlightUpdatedAsync(Flight flight)
        {
            _logger.LogInformation("Updating flight notification: {FlightNumber}", flight.FlightNumber);
            await _hubContext.Clients.All.SendAsync("FlightUpdated", MapFlight(flight));
        }

        private static object MapFlight(Flight flight) => new
        {
            flight.Id,
            flight.FlightNumber,
            flight.Destination,
            flight.DepartureTime,
            flight.Gate,
            flight.CreatedAt,
            flight.UpdatedAt
        };
    }
}