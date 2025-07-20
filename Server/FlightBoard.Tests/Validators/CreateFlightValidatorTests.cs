using FlightBoard.Application.Interfaces;
using FlightBoard.Application.Validators;
using FlightBoard.Domain.DTOs;
using FlightBoard.Domain.Interfaces;
using Moq;

namespace FlightBoard.Tests.Validators
{
    public class CreateFlightValidatorTests
    {
        #region Fields and Constructor

        private readonly Mock<IFlightService> _mockFlightService;
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
        private readonly CreateFlightValidator _validator;
        private static readonly DateTime BaseTestTime = new(2024, 12, 25, 14, 30, 0);

        public CreateFlightValidatorTests()
        {
            _mockFlightService = new Mock<IFlightService>();
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();

            SetupDefaultMocks();

            _validator = new CreateFlightValidator(
                _mockFlightService.Object,
                _mockDateTimeProvider.Object
            );
        }
        private void SetupDefaultMocks()
        {
            _mockFlightService
                .Setup(s => s.FlightNumberExistsAsync(It.IsAny<string>(), It.IsAny<int?>()))
                .ReturnsAsync(false);

            _mockDateTimeProvider
                .SetupGet(p => p.Now)
                .Returns(BaseTestTime);
        }

        #endregion

        #region Required Field Validation Tests

        [Fact]
        public async Task ValidateAsync_ValidFlight_ReturnsValidResult()
        {
            var flightDto = CreateValidFlightDto();

            var result = await _validator.ValidateAsync(flightDto);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ValidateAsync_EmptyFlightNumber_ReturnsInvalidWithRequiredError()
        {
            var flightDto = CreateFlightDto(flightNumber: "");

            var result = await _validator.ValidateAsync(flightDto);

            AssertValidationFailed(result, "Flight number is required");
        }

        [Fact]
        public async Task ValidateAsync_EmptyDestination_ReturnsInvalidWithRequiredError()
        {
            var flightDto = CreateFlightDto(destination: "");

            var result = await _validator.ValidateAsync(flightDto);

            AssertValidationFailed(result, "Destination is required");
        }

        [Fact]
        public async Task ValidateAsync_EmptyGate_ReturnsInvalidWithRequiredError()
        {
            var flightDto = CreateFlightDto(gate: "");

            var result = await _validator.ValidateAsync(flightDto);

            AssertValidationFailed(result, "Gate is required");
        }

        #endregion

        #region Business Logic Validation Tests

        [Fact]
        public async Task ValidateAsync_DuplicateFlightNumber_ReturnsInvalidWithDuplicateError()
        {
            const string flightNumber = "AA101";
            var flightDto = CreateFlightDto(flightNumber: flightNumber);

            _mockFlightService
                .Setup(s => s.FlightNumberExistsAsync(flightNumber, null))
                .ReturnsAsync(true);

            var result = await _validator.ValidateAsync(flightDto);

            AssertValidationFailed(result, "Flight number already exists");
        }

        [Fact]
        public async Task ValidateAsync_PastDepartureTime_ReturnsInvalidWithFutureTimeError()
        {
            var pastTime = BaseTestTime.AddHours(-1);
            var flightDto = CreateFlightDto(departureTime: pastTime);

            var result = await _validator.ValidateAsync(flightDto);

            AssertValidationFailed(result, "Departure time must be in the future");
        }

        #endregion

        #region Flight Number Format Validation Tests

        [Theory]
        [InlineData("AB", "Flight number must be between 3 and 10 characters")]
        [InlineData("ABCDEFGHIJK", "Flight number must be between 3 and 10 characters")]
        [InlineData("", "Flight number is required")]
        [InlineData(null, "Flight number is required")]
        public async Task ValidateAsync_InvalidFlightNumberLength_ReturnsInvalidWithLengthError(
            string flightNumber, string expectedError)
        {
            var flightDto = CreateFlightDto(flightNumber: flightNumber);

            var result = await _validator.ValidateAsync(flightDto);

            AssertValidationFailed(result, expectedError);
        }

        #endregion

        #region Complex Validation Scenarios

        [Fact]
        public async Task ValidateAsync_MultipleValidationErrors_ReturnsAllErrors()
        {
            var flightDto = CreateFlightDto(
                flightNumber: "",
                destination: "",
                departureTime: BaseTestTime.AddHours(-1)
            );

            var result = await _validator.ValidateAsync(flightDto);

            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count >= 3);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Flight number is required"));
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Destination is required"));
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Departure time must be in the future"));
        }

        #endregion

        #region Exception Handling Tests

        [Fact]
        public async Task ValidateAsync_FlightNumberExistsServiceThrows_PropagatesException()
        {
            var flightDto = CreateValidFlightDto();

            _mockFlightService
                .Setup(s => s.FlightNumberExistsAsync(It.IsAny<string>(), It.IsAny<int?>()))
                .ThrowsAsync(new InvalidOperationException("Database error"));

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _validator.ValidateAsync(flightDto));

            Assert.Equal("Database error", exception.Message);
        }

        #endregion

        #region Helper Methods

        private static CreateFlightDto CreateValidFlightDto() =>
            CreateFlightDto("AA101", "New York", BaseTestTime.AddHours(2), "A1");

        private static CreateFlightDto CreateFlightDto(
            string flightNumber = "AA101",
            string destination = "New York",
            DateTime? departureTime = null,
            string gate = "A1") =>
            new(flightNumber, destination, departureTime ?? BaseTestTime.AddHours(2), gate);

        private static void AssertValidationFailed(FluentValidation.Results.ValidationResult result, string expectedError)
        {
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains(expectedError));
        }

        #endregion
    }
}