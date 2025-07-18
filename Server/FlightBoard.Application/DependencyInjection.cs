using FlightBoard.Application.Interfaces;
using FlightBoard.Application.Services;
using FlightBoard.Application.Validators;
using FlightBoard.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace FlightBoard.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IFlightStatusService, FlightStatusService>();
            services.AddValidatorsFromAssemblyContaining<CreateFlightValidator>();

            return services;
        }
    }
}
