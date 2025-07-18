using FlightBoard.Domain.DTOs;
using FlightBoard.Domain.Enums;
using FluentValidation;

namespace FlightBoard.Application.Validators
{
    public class FlightSearchValidator : AbstractValidator<FlightSearchDto>
    {
        public FlightSearchValidator()
        {
            RuleFor(x => x.Status)
                .Must(BeValidStatus)
                .WithMessage("Status must be one of: Scheduled, Boarding, Departed, Landed")
                .When(x => !string.IsNullOrEmpty(x.Status));

            RuleFor(x => x.Destination)
                .Length(2, 100)
                .WithMessage("Destination must be between 2 and 100 characters")
                .When(x => !string.IsNullOrEmpty(x.Destination));
        }

        private bool BeValidStatus(string? status)
        {
            if (string.IsNullOrEmpty(status))
                return true;

            return Enum.TryParse<FlightStatus>(status, true, out _);
        }
    }
}
