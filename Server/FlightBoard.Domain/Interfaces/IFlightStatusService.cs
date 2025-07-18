using FlightBoard.Domain.Enums;

namespace FlightBoard.Domain.Interfaces
{
    public interface IFlightStatusService
    {
        FlightStatus CalculateStatus(DateTime departureTime);
        FlightStatus CalculateStatus(DateTime departureTime, DateTime currentTime);
    }
}
