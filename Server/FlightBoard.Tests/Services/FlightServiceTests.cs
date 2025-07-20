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
        #region Fields and Constructor
        private readonly Mock<IFlightRepository> _mockFlightRepository;
        private readonly Mock<IFlightStatusService> _mockFlightStatusService;
        private readonly Mock<IFlightNotificationService> _mockNotificationService;
        private readonly Mock<ILogger<FlightService>> _mockLogger;
        private readonly FlightService _flightService;

        private static readonly DateTime TestDepartureTime = new DateTime(2024, 12, 25, 14, 30, 0);
        private static readonly DateTime TestCreatedTime = new DateTime(2024, 12, 20, 10, 0, 0);

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
        #endregion

        #region GetAllFlightsAsync Tests

        [Fact]
        public async Task GetAllFlightsAsync_WhenFlightsExist_ReturnsAllFlights()
        {
            var expectedFlights = CreateTestFlights();
            SetupMockRepository(expectedFlights);
            SetupMockStatusService(FlightStatus.Scheduled);

            var result = await _flightService.GetAllFlightsAsync();
            var flightList = result.ToList();

            Assert.Equal(2, flightList.Count);
            Assert.Equal("AA101", flightList.First().FlightNumber);
            Assert.Equal("BA202", flightList.Last().FlightNumber);

            VerifyRepositoryGetAllCalled();
        }

        [Fact]
        public async Task GetAllFlightsAsync_WhenNoFlightsExist_ReturnsEmptyCollection()
        {
            SetupMockRepository(new List<Flight>());

            var result = await _flightService.GetAllFlightsAsync();

            Assert.Empty(result);
            VerifyRepositoryGetAllCalled();
        }
        #endregion

        #region GetFlightByIdAsync Tests

        [Fact]
        public async Task GetFlightByIdAsync_WhenFlightExists_ReturnsFlight()
        {
            const int flightId = 1;
            var expectedFlight = CreateTestFlight(flightId, "AA101", "New York");

            _mockFlightRepository.Setup(r => r.GetByIdAsync(flightId))
                .ReturnsAsync(expectedFlight);
            SetupMockStatusService(FlightStatus.Scheduled);

            var result = await _flightService.GetFlightByIdAsync(flightId);

            Assert.NotNull(result);
            Assert.Equal("AA101", result.FlightNumber);
            Assert.Equal("New York", result.Destination);

            VerifyRepositoryGetByIdCalled(flightId);
        }

        [Fact]
        public async Task GetFlightByIdAsync_WhenFlightDoesNotExist_ReturnsNull()
        {
            const int nonExistentFlightId = 999;
            _mockFlightRepository.Setup(r => r.GetByIdAsync(nonExistentFlightId))
                .ReturnsAsync((Flight?)null);

            var result = await _flightService.GetFlightByIdAsync(nonExistentFlightId);

            Assert.Null(result);
            VerifyRepositoryGetByIdCalled(nonExistentFlightId);
        }

        #endregion

        #region AddFlightAsync Tests

        [Fact]
        public async Task AddFlightAsync_WithValidFlight_ReturnsFlightDto()
        {
            var createFlightDto = CreateTestCreateFlightDto();
            var savedFlight = CreateTestSavedFlight();

            _mockFlightRepository.Setup(r => r.AddAsync(It.IsAny<Flight>()))
                .ReturnsAsync(savedFlight);
            SetupMockStatusService(FlightStatus.Scheduled);

            var result = await _flightService.AddFlightAsync(createFlightDto);

            Assert.NotNull(result);
            Assert.Equal("AA101", result.FlightNumber);
            Assert.Equal("New York", result.Destination);
            Assert.Equal(FlightStatus.Scheduled, result.Status);

            VerifyRepositoryAddCalled();
            VerifyNotificationServiceFlightAddedCalled();
        }

        #endregion

        #region DeleteFlightAsync Tests

        [Fact]
        public async Task DeleteFlightAsync_WhenFlightExists_ReturnsTrue()
        {
            const int flightId = 1;
            _mockFlightRepository.Setup(r => r.DeleteAsync(flightId))
                .ReturnsAsync(true);

            var result = await _flightService.DeleteFlightAsync(flightId);

            Assert.True(result);
            VerifyRepositoryDeleteCalled(flightId);
            VerifyNotificationServiceFlightDeletedCalled(flightId);
        }

        [Fact]
        public async Task DeleteFlightAsync_WhenFlightDoesNotExist_ReturnsFalse()
        {
            const int nonExistentFlightId = 999;
            _mockFlightRepository.Setup(r => r.DeleteAsync(nonExistentFlightId))
                .ReturnsAsync(false);

            var result = await _flightService.DeleteFlightAsync(nonExistentFlightId);

            Assert.False(result);
            VerifyRepositoryDeleteCalled(nonExistentFlightId);
            VerifyNotificationServiceFlightDeletedNotCalled();
        }

        #endregion

        #region FlightNumberExistsAsync Tests

        [Fact]
        public async Task FlightNumberExistsAsync_WhenFlightNumberExists_ReturnsTrue()
        {
            const string flightNumber = "AA101";
            _mockFlightRepository.Setup(r => r.FlightNumberExistsAsync(flightNumber, null))
                .ReturnsAsync(true);

            var result = await _flightService.FlightNumberExistsAsync(flightNumber);

            Assert.True(result);
            VerifyRepositoryFlightNumberExistsCalled(flightNumber);
        }

        [Fact]
        public async Task FlightNumberExistsAsync_WhenFlightNumberDoesNotExist_ReturnsFalse()
        {
            const string nonExistentFlightNumber = "XX999";
            _mockFlightRepository.Setup(r => r.FlightNumberExistsAsync(nonExistentFlightNumber, null))
                .ReturnsAsync(false);

            var result = await _flightService.FlightNumberExistsAsync(nonExistentFlightNumber);

            Assert.False(result);
            VerifyRepositoryFlightNumberExistsCalled(nonExistentFlightNumber);
        }

        #endregion

        #region Test Data Factory Methods

        private static List<Flight> CreateTestFlights()
        {
            return new List<Flight>
            {
                CreateTestFlight(1, "AA101", "New York", "A1"),
                CreateTestFlight(2, "BA202", "London", "B2")
            };
        }

        private static Flight CreateTestFlight(int id, string flightNumber, string destination, string gate = "A1")
        {
            return new Flight
            {
                Id = id,
                FlightNumber = flightNumber,
                Destination = destination,
                DepartureTime = TestDepartureTime,
                Gate = gate,
                CreatedAt = TestCreatedTime,
                UpdatedAt = TestCreatedTime
            };
        }

        private static CreateFlightDto CreateTestCreateFlightDto()
        {
            return new CreateFlightDto("AA101", "New York", TestDepartureTime, "A1");
        }

        private static Flight CreateTestSavedFlight()
        {
            return new Flight
            {
                Id = 1,
                FlightNumber = "AA101",
                Destination = "New York",
                DepartureTime = TestDepartureTime,
                Gate = "A1",
                CreatedAt = TestCreatedTime,
                UpdatedAt = TestCreatedTime
            };
        }

        #endregion

        #region Mock Setup Helper Methods

        private void SetupMockRepository(List<Flight> flights)
        {
            _mockFlightRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(flights);
        }

        private void SetupMockStatusService(FlightStatus status)
        {
            _mockFlightStatusService.Setup(s => s.CalculateStatus(It.IsAny<DateTime>()))
                .Returns(status);
        }

        #endregion

        #region Verification Helper Methods

        private void VerifyRepositoryGetAllCalled()
        {
            _mockFlightRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        private void VerifyRepositoryGetByIdCalled(int id)
        {
            _mockFlightRepository.Verify(r => r.GetByIdAsync(id), Times.Once);
        }

        private void VerifyRepositoryAddCalled()
        {
            _mockFlightRepository.Verify(r => r.AddAsync(It.IsAny<Flight>()), Times.Once);
        }

        private void VerifyRepositoryDeleteCalled(int id)
        {
            _mockFlightRepository.Verify(r => r.DeleteAsync(id), Times.Once);
        }
        
        private void VerifyRepositoryFlightNumberExistsCalled(string flightNumber)
        {
            _mockFlightRepository.Verify(r => r.FlightNumberExistsAsync(flightNumber, null), Times.Once);
        }

        private void VerifyNotificationServiceFlightAddedCalled()
        {
            _mockNotificationService.Verify(n => n.NotifyFlightAddedAsync(It.IsAny<Flight>()), Times.Once);
        }

        private void VerifyNotificationServiceFlightDeletedCalled(int id)
        {
            _mockNotificationService.Verify(n => n.NotifyFlightDeletedAsync(id), Times.Once);
        }

        private void VerifyNotificationServiceFlightDeletedNotCalled()
        {
            _mockNotificationService.Verify(n => n.NotifyFlightDeletedAsync(It.IsAny<int>()), Times.Never);
        }

        #endregion
    }
}