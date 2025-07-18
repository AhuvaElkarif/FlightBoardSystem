using FlightBoard.Domain.Entities;

namespace FlightBoard.Domain.Interfaces
{
    public interface IFlightNotificationService
    {
        Task NotifyFlightAddedAsync(Flight flight);
        Task NotifyFlightDeletedAsync(int flightId);
        Task NotifyFlightUpdatedAsync(Flight flight);
    }
}
