using FlightBoard.Application.Services;
using FlightBoard.Domain.DTOs;
using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Enums;
using FlightBoard.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlightBoard.Tests.Services
{
    public class FlightServiceTests
    {
        private readonly Mock<IFlightRepository> _mockFlightRepository;
        private readonly Mock<IFlightStatusService> _mockFlightStatusService;
        private readonly Mock<IFlightNotificationService> _mockNotificationService;
        private readonly Mock<ILogger<FlightService>> _mockLogger;
        private readonly FlightService _flightService;

        public FlightServiceTests()
        {
            _mockFlightRepository = new Mock<IFlightRepository>();
            _mockFlightStatusService = new Mock<IFlightStatusService>();
            _mockNotificationService = new Mock<IFlightNotificationService>();
            _mockLogger = new Mock<ILogger<FlightService>>();

            _flightService = new FlightService(
                _mockFlightRepository.Object,
                _mockFlightStatusService.Object,
                _mockNotificationService.Object,
                _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllFlightsAsync_ReturnsAllFlights()
        {
            // Arrange
            var flights = new List<Flight>
        {
            new Flight { Id = 1, FlightNumber = "AA101", Destination = "New York", DepartureTime = DateTime.UtcNow.AddHours(2), Gate = "A1" },
            new Flight { Id = 2, FlightNumber = "BA202", Destination = "London", DepartureTime = DateTime.UtcNow.AddHours(3), Gate = "B2" }
        };

            _mockFlightRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(flights);
            _mockFlightStatusService.Setup(s => s.CalculateStatus(It.IsAny<DateTime>())).Returns(FlightStatus.Scheduled);

            // Act
            var result = await _flightService.GetAllFlightsAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("AA101", result.First().FlightNumber);
            _mockFlightRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetFlightByIdAsync_ExistingFlight_ReturnsFlight()
        {
            // Arrange
            var flight = new Flight { Id = 1, FlightNumber = "AA101", Destination = "New York", DepartureTime = DateTime.UtcNow.AddHours(2), Gate = "A1" };
            _mockFlightRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(flight);
            _mockFlightStatusService.Setup(s => s.CalculateStatus(It.IsAny<DateTime>())).Returns(FlightStatus.Scheduled);

            // Act
            var result = await _flightService.GetFlightByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AA101", result.FlightNumber);
            _mockFlightRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetFlightByIdAsync_NonExistingFlight_ReturnsNull()
        {
            // Arrange
            _mockFlightRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Flight?)null);

            // Act
            var result = await _flightService.GetFlightByIdAsync(999);

            // Assert
            Assert.Null(result);
            _mockFlightRepository.Verify(r => r.GetByIdAsync(999), Times.Once);
        }

        [Fact]
        public async Task AddFlightAsync_ValidFlight_ReturnsFlightDto()
        {
            // Arrange
            var createFlightDto = new CreateFlightDto("AA101", "New York", DateTime.UtcNow.AddHours(2), "A1");
            var savedFlight = new Flight
            {
                Id = 1,
                FlightNumber = "AA101",
                Destination = "New York",
                DepartureTime = DateTime.UtcNow.AddHours(2),
                Gate = "A1",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _mockFlightRepository.Setup(r => r.AddAsync(It.IsAny<Flight>())).ReturnsAsync(savedFlight);
            _mockFlightStatusService.Setup(s => s.CalculateStatus(It.IsAny<DateTime>())).Returns(FlightStatus.Scheduled);

            // Act
            var result = await _flightService.AddFlightAsync(createFlightDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AA101", result.FlightNumber);
            Assert.Equal("New York", result.Destination);
            Assert.Equal(FlightStatus.Scheduled, result.Status);
            _mockFlightRepository.Verify(r => r.AddAsync(It.IsAny<Flight>()), Times.Once);
            _mockNotificationService.Verify(n => n.NotifyFlightAddedAsync(It.IsAny<Flight>()), Times.Once);
        }

        [Fact]
        public async Task DeleteFlightAsync_ExistingFlight_ReturnsTrue()
        {
            // Arrange
            _mockFlightRepository.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _flightService.DeleteFlightAsync(1);

            // Assert
            Assert.True(result);
            _mockFlightRepository.Verify(r => r.DeleteAsync(1), Times.Once);
            _mockNotificationService.Verify(n => n.NotifyFlightDeletedAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteFlightAsync_NonExistingFlight_ReturnsFalse()
        {
            // Arrange
            _mockFlightRepository.Setup(r => r.DeleteAsync(999)).ReturnsAsync(false);

            // Act
            var result = await _flightService.DeleteFlightAsync(999);

            // Assert
            Assert.False(result);
            _mockFlightRepository.Verify(r => r.DeleteAsync(999), Times.Once);
            _mockNotificationService.Verify(n => n.NotifyFlightDeletedAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task SearchFlightsAsync_WithFilters_ReturnsFilteredFlights()
        {
            // Arrange
            var searchDto = new FlightSearchDto("Scheduled", "New York");
            var flights = new List<Flight>
        {
            new Flight { Id = 1, FlightNumber = "AA101", Destination = "New York", DepartureTime = DateTime.UtcNow.AddHours(2), Gate = "A1" }
        };

            _mockFlightRepository.Setup(r => r.SearchAsync("Scheduled", "New York")).ReturnsAsync(flights);
            _mockFlightStatusService.Setup(s => s.CalculateStatus(It.IsAny<DateTime>())).Returns(FlightStatus.Scheduled);

            // Act
            var result = await _flightService.SearchFlightsAsync(searchDto);

            // Assert
            Assert.Single(result);
            Assert.Equal("AA101", result.First().FlightNumber);
            _mockFlightRepository.Verify(r => r.SearchAsync("Scheduled", "New York"), Times.Once);
        }

        [Fact]
        public async Task FlightNumberExistsAsync_ExistingFlightNumber_ReturnsTrue()
        {
            // Arrange
            _mockFlightRepository.Setup(r => r.FlightNumberExistsAsync("AA101", null)).ReturnsAsync(true);

            // Act
            var result = await _flightService.FlightNumberExistsAsync("AA101");

            // Assert
            Assert.True(result);
            _mockFlightRepository.Verify(r => r.FlightNumberExistsAsync("AA101", null), Times.Once);
        }
    }
}
