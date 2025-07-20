using FlightBoard.Application.Services;
using FlightBoard.Domain.Enums;

namespace FlightBoard.Tests.Services
{
    public class FlightStatusServiceTests
    {
        #region Fields and Constructor
        private readonly FlightStatusService _flightStatusService;
        private static readonly DateTime BaseTestTime = new DateTime(2024, 12, 25, 14, 30, 0);

        public FlightStatusServiceTests()
        {
            _flightStatusService = new FlightStatusService();
        }
        #endregion

        #region Scheduled Status Tests
        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsMoreThan30MinutesInFuture_ReturnsScheduled()
        {
            var currentTime = BaseTestTime;
            var departureTime = currentTime.AddMinutes(45);

            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);
            Assert.Equal(FlightStatus.Scheduled, result);
        }

        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsExactly30MinutesInFuture_ReturnsScheduled()
        {
            var currentTime = BaseTestTime;
            var departureTime = currentTime.AddMinutes(30);

            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);
            Assert.Equal(FlightStatus.Boarding, result);
        }
        #endregion

        #region Boarding Status Tests
        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsWithin30Minutes_ReturnsBoarding()
        {
            var currentTime = BaseTestTime;
            var departureTime = currentTime.AddMinutes(15);

            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);
            Assert.Equal(FlightStatus.Boarding, result);
        }

        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsNow_ReturnsBoarding()
        {
            var currentTime = BaseTestTime;
            var departureTime = currentTime;

            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);
            Assert.Equal(FlightStatus.Boarding, result);
        }
        #endregion

        #region Departed Status Tests
        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsWithin60MinutesInPast_ReturnsDeparted()
        {
            var currentTime = BaseTestTime;
            var departureTime = currentTime.AddMinutes(-30);

            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);
            Assert.Equal(FlightStatus.Departed, result);
        }

        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsExactly60MinutesInPast_ReturnsDeparted()
        {
            var currentTime = BaseTestTime;
            var departureTime = currentTime.AddMinutes(-60);

            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);
            Assert.Equal(FlightStatus.Departed, result);
        }
        #endregion

        #region Landed Status Tests
        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsMoreThan60MinutesInPast_ReturnsLanded()
        {
            var currentTime = BaseTestTime;
            var departureTime = currentTime.AddMinutes(-90);

            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);
            Assert.Equal(FlightStatus.Landed, result);
        }
        #endregion

        #region Mixed Status Logic Tests
        [Theory]
        [InlineData(31, FlightStatus.Scheduled)]
        [InlineData(30, FlightStatus.Boarding)]
        [InlineData(29, FlightStatus.Boarding)]
        [InlineData(1, FlightStatus.Boarding)]
        [InlineData(0, FlightStatus.Boarding)]
        [InlineData(-1, FlightStatus.Departed)]
        [InlineData(-30, FlightStatus.Departed)]
        [InlineData(-60, FlightStatus.Departed)]
        [InlineData(-61, FlightStatus.Landed)]
        [InlineData(-120, FlightStatus.Landed)]
        public void CalculateStatus_VariousTimeOffsets_ReturnsCorrectStatus(int minutesOffset, FlightStatus expectedStatus)
        {
            var currentTime = BaseTestTime;
            var departureTime = currentTime.AddMinutes(minutesOffset);

            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);
            Assert.Equal(expectedStatus, result);
        }
        #endregion
    }
}
