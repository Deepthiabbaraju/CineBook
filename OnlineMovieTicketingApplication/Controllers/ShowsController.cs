using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMovieTicketingApplication.Models;

namespace OnlineMovieTicketingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : Controller
    {
        private readonly MovieContext _context;

        public ShowsController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/shows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shows>>> GetShows()
        {
            var shows = await _context.Shows.ToListAsync();
            return Ok(shows);
        }

        // GET: api/shows/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Shows>> GetShow(int id)
        {
            var show = await _context.Shows.FindAsync(id);

            if (show == null)
            {
                return NotFound();
            }

            return show;
        }

        // POST: api/shows/addshow
        [HttpPost("addshow")]
        public async Task<IActionResult> AddShow([FromBody] Shows show)
        {
            if (show == null)
            {
                return BadRequest("Show data is null.");
            }

            _context.Shows.Add(show);
            await _context.SaveChangesAsync();
            var seats = GenerateSeats(show);
            _context.Seats.AddRange(seats);
            await _context.SaveChangesAsync();


            return Ok(show);
        }

        // PUT: api/shows/editshow/{id}
        [HttpPut("editshow/{id}")]
        public async Task<IActionResult> EditShow(int id, [FromBody] Shows updatedShow)
        {
            var showToUpdate = await _context.Shows.FindAsync(id);

            if (showToUpdate == null)
            {
                return NotFound();
            }

            
            showToUpdate.TheatreId = updatedShow.TheatreId;
            showToUpdate.MovieId = updatedShow.MovieId;
            showToUpdate.ShowTime = updatedShow.ShowTime;
            showToUpdate.AvailableSeats = updatedShow.AvailableSeats;

            _context.Entry(showToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(showToUpdate); // Return updated show object if needed
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception
                if (!ShowExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/shows/deleteshow/{id}
        [HttpDelete("deleteshow/{id}")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }

            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Show deleted successfully." });
        }

        private bool ShowExists(int id)
        {
            return _context.Shows.Any(e => e.Id == id);
        }
        private List<Seat> GenerateSeats(Shows show)
        {
            var seats = new List<Seat>();
            var rows = new[] { "A", "B", "C", "D", "E", "F" };
            var seatCountPerRow = 16;

            foreach (var row in rows)
            {
                for (int i = 1; i <= seatCountPerRow; i++)
                {
                    var seatId = $"{row}{i}";
                    seats.Add(new Seat
                    {
                        ShowTime = show.ShowTime,
                        TheatreId = show.TheatreId,
                        Id = seatId,
                        Available = true,
                        MovieId = show.MovieId,
                        
                    });
                }
            }

            return seats;
        }
    }
}


