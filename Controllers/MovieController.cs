
using Lab4.Data;
using Lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieDbContext _context;

        public MovieController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: api/Movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Movie/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound();

            return movie;
        }

        // POST: api/Movie
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMovie),
                new { id = movie.MovieId },
                movie);
        }

        // PUT: api/Movie/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(
            int id, Movie movie)
        {
            if (id != movie.MovieId)
                return BadRequest();

            var existingMovie = await _context.Movies.FindAsync(id);

            if (existingMovie == null)
                return NotFound();

            existingMovie.MovieName = movie.MovieName;
            existingMovie.Genre = movie.Genre;
            existingMovie.Year = movie.Year;
            existingMovie.ImdbRating = movie.ImdbRating;
            existingMovie.DirectorId = movie.DirectorId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Movie/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
