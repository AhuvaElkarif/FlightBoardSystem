using FlightBoard.Domain.Enums;
using FlightBoard.Domain.Interfaces;

namespace FlightBoard.Application.Services
{
    public class FlightStatusService : IFlightStatusService
    {
        public FlightStatus CalculateStatus(DateTime departureTime)
        {
            return CalculateStatus(departureTime, DateTime.Now);
        }

        public FlightStatus CalculateStatus(DateTime departureTime, DateTime currentTime)
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
