using FlightBoard.Application.Interfaces;
using FlightBoard.Application.Validators;
using FlightBoard.Domain.DTOs;
using Moq;

namespace FlightBoard.Tests.Validators
{
    public class CreateFlightValidatorTests
    {
        private readonly Mock<IFlightService> _mockFlightService;
        private readonly CreateFlightValidator _validator;

        public CreateFlightValidatorTests()
        {
            _mockFlightService = new Mock<IFlightService>();
            _validator = new CreateFlightValidator(_mockFlightService.Object);
        }

        [Fact]
        public async Task Validate_ValidFlight_ReturnsValid()
        {
            // Arrange
            var createFlightDto = new CreateFlightDto("AA101", "New York", DateTime.UtcNow.AddHours(2), "A1");
            _mockFlightService.Setup(s => s.FlightNumberExistsAsync("AA101")).ReturnsAsync(false);

            // Act
            var result = await _validator.ValidateAsync(createFlightDto);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Validate_EmptyFlightNumber_ReturnsInvalid()
        {
            // Arrange
            var createFlightDto = new CreateFlightDto("", "New York", DateTime.UtcNow.AddHours(2), "A1");

            // Act
            var result = await _validator.ValidateAsync(createFlightDto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Flight number is required"));
        }

        [Fact]
        public async Task Validate_DuplicateFlightNumber_ReturnsInvalid()
        {
            // Arrange
            var createFlightDto = new CreateFlightDto("AA101", "New York", DateTime.UtcNow.AddHours(2), "A1");
            _mockFlightService.Setup(s => s.FlightNumberExistsAsync("AA101")).ReturnsAsync(true);

            // Act
            var result = await _validator.ValidateAsync(createFlightDto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Flight number already exists"));
        }

        [Fact]
        public async Task Validate_PastDepartureTime_ReturnsInvalid()
        {
            // Arrange
            var createFlightDto = new CreateFlightDto("AA101", "New York", DateTime.UtcNow.AddHours(-1), "A1");
            _mockFlightService.Setup(s => s.FlightNumberExistsAsync("AA101")).ReturnsAsync(false);

            // Act
            var result = await _validator.ValidateAsync(createFlightDto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Departure time must be in the future"));
        }

        [Theory]
        [InlineData("AB", "Flight number must be between 3 and 10 characters")]
        [InlineData("ABCDEFGHIJK", "Flight number must be between 3 and 10 characters")]
        [InlineData("", "Flight number is required")]
        public async Task Validate_InvalidFlightNumberLength_ReturnsInvalid(string flightNumber, string expectedError)
        {
            // Arrange
            var createFlightDto = new CreateFlightDto(flightNumber, "New York", DateTime.UtcNow.AddHours(2), "A1");
            _mockFlightService.Setup(s => s.FlightNumberExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _validator.ValidateAsync(createFlightDto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(expectedError));
        }
    }
}
