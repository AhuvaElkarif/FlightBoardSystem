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
            _logger.LogInformation("Notifying clients about new flight: {FlightNumber}", flight.FlightNumber);

            await _hubContext.Clients.All.SendAsync("FlightAdded", new
            {
                flight.Id,
                flight.FlightNumber,
                flight.Destination,
                flight.DepartureTime,
                flight.Gate,
                flight.CreatedAt,
                flight.UpdatedAt
            });
        }

        public async Task NotifyFlightDeletedAsync(int flightId)
        {
            _logger.LogInformation("Notifying clients about deleted flight: {FlightId}", flightId);

            await _hubContext.Clients.All.SendAsync("FlightDeleted", flightId);
        }

        public async Task NotifyFlightUpdatedAsync(Flight flight)
        {
            _logger.LogInformation("Notifying clients about updated flight: {FlightNumber}", flight.FlightNumber);

            await _hubContext.Clients.All.SendAsync("FlightUpdated", new
            {
                flight.Id,
                flight.FlightNumber,
                flight.Destination,
                flight.DepartureTime,
                flight.Gate,
                flight.CreatedAt,
                flight.UpdatedAt
            });
        }
    }
}
