using FlightBoard.Application.Interfaces;
using FlightBoard.Domain.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FlightBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IValidator<CreateFlightDto> _createFlightValidator;
        private readonly IValidator<FlightSearchDto> _searchValidator;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(
            IFlightService flightService,
            IValidator<CreateFlightDto> createFlightValidator,
            IValidator<FlightSearchDto> searchValidator,
            ILogger<FlightsController> logger)
        {
            _flightService = flightService;
            _createFlightValidator = createFlightValidator;
            _searchValidator = searchValidator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightDto>>> GetFlights()
        {
            try
            {
                var flights = await _flightService.GetAllFlightsAsync();
                _logger.LogInformation("Successfully retrieved {FlightCount} flights", flights.Count());
                return Ok(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flights");
                return StatusCode(500, "An error occurred while retrieving flights");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FlightDto>> GetFlight(int id)
        {
            try
            {
                var flight = await _flightService.GetFlightByIdAsync(id);

                if (flight == null)
                {
                    _logger.LogWarning("Flight with ID {FlightId} not found", id);
                    return NotFound($"Flight with ID {id} not found");
                }

                _logger.LogInformation("Successfully retrieved flight {FlightId}", id);
                return Ok(flight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flight {FlightId}", id);
                return StatusCode(500, "An error occurred while retrieving the flight");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FlightDto>>> SearchFlights([FromQuery] string? status = null, [FromQuery] string? destination = null)
        {
            try
            {
                var searchDto = new FlightSearchDto(status, destination);
                var validationResult = await _searchValidator.ValidateAsync(searchDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    _logger.LogWarning("Validation failed for flight search: {ValidationErrors}",
                        string.Join(", ", errors));
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var flights = await _flightService.SearchFlightsAsync(searchDto);
                _logger.LogInformation("Flight search returned {FlightCount} results", flights.Count());
                return Ok(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching flights");
                return StatusCode(500, "An error occurred while searching flights");
            }
        }

        [HttpPost]
        public async Task<ActionResult<FlightDto>> CreateFlight(CreateFlightDto createFlightDto)
        {
            try
            {
                var validationResult = await _createFlightValidator.ValidateAsync(createFlightDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    _logger.LogWarning("Validation failed for flight creation: {ValidationErrors}",
                        string.Join(", ", errors));
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var flight = await _flightService.AddFlightAsync(createFlightDto);
                _logger.LogInformation("Successfully created flight with ID: {FlightId}", flight.Id);
                return CreatedAtAction(nameof(GetFlight), new { id = flight.Id }, flight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating flight");
                return StatusCode(500, "An error occurred while creating the flight");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            try
            {
                var result = await _flightService.DeleteFlightAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Flight with ID {FlightId} not found for deletion", id);
                    return NotFound($"Flight with ID {id} not found");
                }
                _logger.LogInformation("Successfully deleted flight with ID: {FlightId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting flight {FlightId}", id);
                return StatusCode(500, "An error occurred while deleting the flight");
            }
        }
    }
}
