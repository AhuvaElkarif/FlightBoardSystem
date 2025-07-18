using FlightBoard.Domain.Interfaces;
using FlightBoard.Infrastructure.Data;
using FlightBoard.Infrastructure.Repositories;
using FlightBoard.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightBoard.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FlightBoardDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IFlightNotificationService, FlightNotificationService>();

            services.AddSignalR();

            return services;
        }
    }
}
