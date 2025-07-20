using FlightBoard.Domain.Enums;
using FlightBoard.Domain.Interfaces;

namespace FlightBoard.Application.Services
{
    public class FlightStatusService : IFlightStatusService
    {
        public FlightStatus CalculateStatus(DateTime departureTime) =>
            CalculateStatus(departureTime, DateTime.Now);

        public FlightStatus CalculateStatus(DateTime departureTime, DateTime currentTime)
        {
            var minutesDifference = (departureTime - currentTime).TotalMinutes;

            if (minutesDifference > 30)
                return FlightStatus.Scheduled;

            if (minutesDifference <= 30 && minutesDifference >= 0)
                return FlightStatus.Boarding;

            if (minutesDifference < 0 && minutesDifference >= -60)
                return FlightStatus.Departed;

            return FlightStatus.Landed;
        }
    }
}