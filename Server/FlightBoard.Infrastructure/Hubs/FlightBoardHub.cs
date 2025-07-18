using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace FlightBoard.Infrastructure.Hubs
{
    public class FlightBoardHub : Hub
    {
        private readonly ILogger<FlightBoardHub> _logger;

        public FlightBoardHub(ILogger<FlightBoardHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
