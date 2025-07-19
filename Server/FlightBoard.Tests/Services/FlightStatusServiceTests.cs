using FlightBoard.Application.Services;
using FlightBoard.Domain.Enums;

namespace FlightBoard.Tests.Services
{
    public class FlightStatusServiceTests
    {
        private readonly FlightStatusService _flightStatusService;

        public FlightStatusServiceTests()
        {
            _flightStatusService = new FlightStatusService();
        }

        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsMoreThan30MinutesInFuture_ReturnsScheduled()
        {
            // Arrange
            var currentTime = DateTime.UtcNow;
            var departureTime = currentTime.AddMinutes(45);

            // Act
            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);

            // Assert
            Assert.Equal(FlightStatus.Scheduled, result);
        }

        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsExactly30MinutesInFuture_ReturnsScheduled()
        {
            // Arrange
            var currentTime = DateTime.UtcNow;
            var departureTime = currentTime.AddMinutes(30);

            // Act
            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);

            // Assert
            Assert.Equal(FlightStatus.Scheduled, result);
        }

        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsWithin30Minutes_ReturnsBoarding()
        {
            // Arrange
            var currentTime = DateTime.UtcNow;
            var departureTime = currentTime.AddMinutes(15);

            // Act
            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);

            // Assert
            Assert.Equal(FlightStatus.Boarding, result);
        }

        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsNow_ReturnsBoarding()
        {
            // Arrange
            var currentTime = DateTime.UtcNow;
            var departureTime = currentTime;

            // Act
            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);

            // Assert
            Assert.Equal(FlightStatus.Boarding, result);
        }

        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsWithin60MinutesInPast_ReturnsDeparted()
        {
            // Arrange
            var currentTime = DateTime.UtcNow;
            var departureTime = currentTime.AddMinutes(-30);

            // Act
            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);

            // Assert
            Assert.Equal(FlightStatus.Departed, result);
        }

        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsExactly60MinutesInPast_ReturnsDeparted()
        {
            // Arrange
            var currentTime = DateTime.UtcNow;
            var departureTime = currentTime.AddMinutes(-60);

            // Act
            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);

            // Assert
            Assert.Equal(FlightStatus.Departed, result);
        }

        [Fact]
        public void CalculateStatus_WhenDepartureTimeIsMoreThan60MinutesInPast_ReturnsLanded()
        {
            // Arrange
            var currentTime = DateTime.UtcNow;
            var departureTime = currentTime.AddMinutes(-90);

            // Act
            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);

            // Assert
            Assert.Equal(FlightStatus.Landed, result);
        }

        [Theory]
        [InlineData(31, FlightStatus.Scheduled)]
        [InlineData(30, FlightStatus.Scheduled)]
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
            // Arrange
            var currentTime = DateTime.UtcNow;
            var departureTime = currentTime.AddMinutes(minutesOffset);

            // Act
            var result = _flightStatusService.CalculateStatus(departureTime, currentTime);

            // Assert
            Assert.Equal(expectedStatus, result);
        }
    }
}
