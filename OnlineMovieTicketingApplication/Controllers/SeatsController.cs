using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineMovieTicketingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineMovieTicketingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly ILogger<SeatsController> _logger;

        public SeatsController(MovieContext context, ILogger<SeatsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST api/seats/updateSeats
        [HttpPost("updateSeats")]
        public IActionResult UpdateSeats([FromBody] UpdateSeatsRequests request)
        {
            try
            {
                _logger.LogInformation("Received request to update seats: {Request}", request);

                var seatsToUpdate = _context.Seats.Where(s => request.SeatIds.Contains(s.Id) &&
                                                         s.TheatreId == request.TheatreId &&
                                                         s.ShowTime == request.ShowTime).ToList();

                if (seatsToUpdate.Count == 0)
                {
                    _logger.LogWarning("No seats found with provided IDs: {SeatIds}", request.SeatIds);
                    return NotFound(new { success = false, message = "No seats found to update." });
                }

                foreach (var seat in seatsToUpdate)
                {
                    _logger.LogInformation("Updating seat: {SeatId}", seat.Id);
                    seat.Available = request.Status;
                    seat.MovieId = request.MovieId;
                    
                }

                _context.SaveChanges();
                _logger.LogInformation("Seats updated successfully.");

                return Ok(new { success = true, message = "Seats updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating seats");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("getBookedSeats")]
        public IActionResult GetBookedSeats(int theatreId, DateTime showTime)
        {
            try
            {
                _logger.LogInformation("Fetching booked seats for TheatreId: {TheatreId} and ShowTime: {ShowTime}", theatreId, showTime);

                var bookedSeats = _context.Seats
                                   .Where(s => s.TheatreId == theatreId && s.ShowTime == showTime && !s.Available)
                                   .Select(s => s.Id)
                                   .ToList();
                return Ok(bookedSeats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving booked seats");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    public class UpdateSeatsRequests
    {
        public List<string> SeatIds { get; set; }
        public bool Status { get; set; }
        public int MovieId { get; set; }
        public int TheatreId { get; set; }
        public DateTime ShowTime { get; set; }
    }
}
