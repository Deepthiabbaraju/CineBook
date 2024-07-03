using Microsoft.AspNetCore.Mvc;
using OnlineMovieTicketingApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMovieTicketingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheatreController : Controller
    {
        private readonly MovieContext _context;

        public TheatreController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet("GetTheatreIdByName")]
        public async Task<ActionResult<int?>> GetTheatreIdByName(string theatreName)
        {
            var theatre = await _context.Theatres.FirstOrDefaultAsync(t => t.Name == theatreName);
            if (theatre == null)
            {
                return NotFound();
            }
            return theatre.Id;
        }
        [HttpPost("addtheatre")]
        public async Task<IActionResult> AddTheatre([FromBody] Theatre theatre)
        {
            if (theatre == null)
            {
                return BadRequest("Theatre is null.");
            }

            _context.Theatres.Add(theatre);
            await _context.SaveChangesAsync();

            return Ok(theatre);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Theatre>>> GetTheatres()
        {
            var theatres = await _context.Theatres.ToListAsync();
            return Ok(theatres);
        }
        [HttpDelete("deletetheatre/{id}")]
        public async Task<IActionResult> DeleteTheatre(int id)
        {
            var theatre = await _context.Theatres.FindAsync(id);
            if (theatre == null)
            {
                return NotFound();
            }

            _context.Theatres.Remove(theatre);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Theatre deleted successfully." });
        }

    }
}
