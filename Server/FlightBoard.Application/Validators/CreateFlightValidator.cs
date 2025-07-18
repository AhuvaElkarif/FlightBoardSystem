using FlightBoard.Application.Interfaces;
using FlightBoard.Domain.DTOs;
using FluentValidation;

namespace FlightBoard.Application.Validators
{
    public class CreateFlightValidator : AbstractValidator<CreateFlightDto>
    {
        private readonly IFlightService _flightService;

        public CreateFlightValidator(IFlightService flightService)
        {
            _flightService = flightService;

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
                .Length(2, 100)
                .WithMessage("Destination must be between 2 and 100 characters");

            RuleFor(x => x.Gate)
                .NotEmpty()
                .WithMessage("Gate is required")
                .Length(1, 10)
                .WithMessage("Gate must be between 1 and 10 characters");

            RuleFor(x => x.DepartureTime)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Departure time must be in the future");
        }

        private async Task<bool> BeUniqueFlightNumber(string flightNumber, CancellationToken cancellationToken)
        {
            return !await _flightService.FlightNumberExistsAsync(flightNumber);
        }
    }
}
