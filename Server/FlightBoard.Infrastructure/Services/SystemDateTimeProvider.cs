using FlightBoard.Domain.Interfaces;

namespace FlightBoard.Infrastructure.Services
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}


