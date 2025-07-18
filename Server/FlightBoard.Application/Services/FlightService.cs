using FlightBoard.Application.Interfaces;
using FlightBoard.Domain.DTOs;
using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FlightBoard.Application.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IFlightStatusService _flightStatusService;
        private readonly IFlightNotificationService _notificationService;
        private readonly ILogger<FlightService> _logger;

        public FlightService(
            IFlightRepository flightRepository,
            IFlightStatusService flightStatusService,
            IFlightNotificationService notificationService,
            ILogger<FlightService> logger)
        {
            _flightRepository = flightRepository;
            _flightStatusService = flightStatusService;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task<IEnumerable<FlightDto>> GetAllFlightsAsync()
        {
            _logger.LogInformation("Retrieving all flights");

            var flights = await _flightRepository.GetAllAsync();
            return flights.Select(MapToDto);
        }

        public async Task<FlightDto?> GetFlightByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving flight with ID: {FlightId}", id);

            var flight = await _flightRepository.GetByIdAsync(id);
            return flight != null ? MapToDto(flight) : null;
        }

        public async Task<IEnumerable<FlightDto>> SearchFlightsAsync(FlightSearchDto searchDto)
        {
            _logger.LogInformation("Searching flights with filters - Status: {Status}, Destination: {Destination}",
                searchDto.Status, searchDto.Destination);

            var flights = await _flightRepository.SearchAsync(searchDto.Status, searchDto.Destination);
            return flights.Select(MapToDto);
        }

        public async Task<FlightDto> AddFlightAsync(CreateFlightDto createFlightDto)
        {
            _logger.LogInformation("Adding new flight: {FlightNumber}", createFlightDto.FlightNumber);

            var flight = new Flight
            {
                FlightNumber = createFlightDto.FlightNumber,
                Destination = createFlightDto.Destination,
                DepartureTime = createFlightDto.DepartureTime,
                Gate = createFlightDto.Gate,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var savedFlight = await _flightRepository.AddAsync(flight);
            var flightDto = MapToDto(savedFlight);

            await _notificationService.NotifyFlightAddedAsync(savedFlight);

            _logger.LogInformation("Flight added successfully: {FlightNumber} with ID: {FlightId}",
                savedFlight.FlightNumber, savedFlight.Id);

            return flightDto;
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            _logger.LogInformation("Deleting flight with ID: {FlightId}", id);

            var result = await _flightRepository.DeleteAsync(id);

            if (result)
            {
                await _notificationService.NotifyFlightDeletedAsync(id);
                _logger.LogInformation("Flight deleted successfully: {FlightId}", id);
            }
            else
            {
                _logger.LogWarning("Flight not found for deletion: {FlightId}", id);
            }

            return result;
        }

        public async Task<bool> FlightNumberExistsAsync(string flightNumber, int? excludeId = null)
        {
            return await _flightRepository.FlightNumberExistsAsync(flightNumber, excludeId);
        }

        private FlightDto MapToDto(Flight flight)
        {
            return new FlightDto(
                flight.Id,
                flight.FlightNumber,
                flight.Destination,
                flight.DepartureTime,
                flight.Gate,
                _flightStatusService.CalculateStatus(flight.DepartureTime),
                flight.CreatedAt,
                flight.UpdatedAt
            );
        }
    }

}
