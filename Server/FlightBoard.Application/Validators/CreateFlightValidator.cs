using FlightBoard.Application.Interfaces;
using FlightBoard.Domain.DTOs;
using FlightBoard.Domain.Interfaces;
using FluentValidation;

namespace FlightBoard.Application.Validators
{
    public class CreateFlightValidator : AbstractValidator<CreateFlightDto>
    {
        private readonly IFlightService _flightService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateFlightValidator(IFlightService flightService, IDateTimeProvider dateTimeProvider)
        {
            _flightService = flightService;
            _dateTimeProvider = dateTimeProvider;

            RuleFor(x => x.FlightNumber)
                .NotEmpty()
                .WithMessage("Flight number is required")
                .Length(3, 10)
                .WithMessage("Flight number must be between 3 and 10 characters")
                .MustAsync(BeUniqueFlightNumber)
                .WithMessage("Flight number already exists");

            RuleFor(x => x.Destination)
                .NotEmpty()
                .WithMessage("Destination is required")
                .Length(1, 100)
                .WithMessage("Destination must be between 1 and 100 characters");

            RuleFor(x => x.Gate)
                .NotEmpty()
                .WithMessage("Gate is required")
                .Length(1, 10)
                .WithMessage("Gate must be between 1 and 10 characters");

            RuleFor(x => x.DepartureTime)
                .GreaterThan(_dateTimeProvider.Now)
                .WithMessage("Departure time must be in the future");
        }

        private async Task<bool> BeUniqueFlightNumber(string flightNumber, CancellationToken cancellationToken)
        {
            return !await _flightService.FlightNumberExistsAsync(flightNumber, null);
        }
    }
}
