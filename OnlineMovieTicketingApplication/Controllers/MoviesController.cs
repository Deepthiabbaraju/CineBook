using Microsoft.AspNetCore.Mvc;
using OnlineMovieTicketingApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Azure;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.PortableExecutable;
using System.Threading.Channels;
using System.Threading.Tasks;
using System;


[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieContext _context;
    private string language;

    public MoviesController(MovieContext context)
    {
        _context = context;
    }

    // GET: api/movies
    [HttpGet]

    public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByLocation([FromQuery] string location)
    {
        if (string.IsNullOrEmpty(location))
        {
            return BadRequest("Location parameter is missing.");
        }

        var movies = await _context.Movies
            .Where(m => m.Location == location)
            .ToListAsync();

        if (movies == null || movies.Count == 0)
        {
            return NotFound($"No movies found for location: {location}");
        }

        return Ok(movies);
    }
    // GET: api/movies/title
    [HttpGet("title")]

    public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByName([FromQuery] string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            return BadRequest("title parameter is missing.");
        }

        var movies = await _context.Movies
            .Where(m => m.Title == title)
            .ToListAsync();

        if (movies == null || movies.Count == 0)
        {
            return NotFound($"No movies found for : {title}");
        }

        return Ok(movies);
    }

    // GET: api/movies/{}
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        return movie;
    }
    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
        movie.Id = 0; 

        _context.Movies.Add(movie);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (MovieExists(movie.Id))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
    }

    private bool MovieExists(int id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
    

    // GET: api/movies/{movieId}/theatres
    [HttpGet("{movieId}/theatres")]
    public async Task<IActionResult> GetTheatresForMovie(int movieId, [FromQuery] DateTime date)
    {
        var theatres = await _context.Shows
            .Where(s => s.MovieId == movieId && s.ShowTime.Date == date.Date)
            .GroupBy(s => s.TheatreId)
            .Select(group => new
            {
                TheatreId = group.Key,
                TheatreName = _context.Theatres
                                       .Where(t => t.Id == group.Key)
                                       .Select(t => t.Name)
                                       .FirstOrDefault(),
                ShowTimes = group.Select(g => new { g.ShowTime, g.AvailableSeats }).ToList()
            })
            .ToListAsync();

        return Ok(theatres);
    }
    [HttpGet("movies")]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _context.Movies.ToListAsync();
        return Ok(movies);
    }
    [HttpPut("editmovie/{id}")]
    public async Task<IActionResult> EditMovie(int id, [FromBody] MovieDto updatedMovie)
    {
        var movieToUpdate = await _context.Movies.FindAsync(id);

        if (movieToUpdate == null)
        {
            return NotFound();
        }

        
        movieToUpdate.Title = updatedMovie.Title;
        movieToUpdate.Genre = updatedMovie.Genre;
        movieToUpdate.ReleaseDate = updatedMovie.ReleaseDate;
        movieToUpdate.Language = updatedMovie.Language;
        movieToUpdate.Location = updatedMovie.Location;
        movieToUpdate.ImageUrl = updatedMovie.ImageUrl;
        movieToUpdate.duration = updatedMovie.Duration;

        _context.Entry(movieToUpdate).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(movieToUpdate); 
        }
        catch (DbUpdateConcurrencyException)
        {
            
            if (!MovieExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        
    }
    // DELETE: api/movies/deletemovie/{id}
    [HttpDelete("deletemovie/{id}")]
    public IActionResult DeleteMovie(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        _context.SaveChanges();

        return Ok();
    }

    [HttpGet("movietitles")]
    public async Task<ActionResult<IEnumerable<MovieTitleDto>>> GetMovieTitles()
    {
        var movieTitles = await _context.Movies
            .Select(m => new MovieTitleDto { Id = m.Id, Title = m.Title })
            .ToListAsync();

        return Ok(movieTitles);
    }
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Movie>>> SearchMovies([FromQuery] string query, [FromQuery] string location)
    {
        var movies = await _context.Movies
            .Where(m =>
                (EF.Functions.Like(m.Title, $"%{query}%") ||
                EF.Functions.Like(m.Genre, $"%{query}%") ||
                EF.Functions.Like(m.Language, $"%{query}%") ||
                m.ReleaseDate.ToString().Contains(query)) &&
                m.Location == location)  
            .ToListAsync();

        return Ok(movies);
    }





    public class BookingRequest
    {
        public int ShowId { get; set; }
        public string UserId { get; set; }
        public string SeatNumber { get; set; }
    }
}
    



