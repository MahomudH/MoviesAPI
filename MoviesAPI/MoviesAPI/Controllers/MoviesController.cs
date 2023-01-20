using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private new List<string> _allowedExtentions = new List<string>() {".png",".jpg",".jfif" };
        private long _maxAllowedPosterSize = 1048576;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto input)
        {
            if (!_allowedExtentions.Contains(Path.GetExtension(input.Poster.FileName).ToLower()))
                return BadRequest("Only .png, .jpg and .jfif images are allowed!");

            if (input.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            var isValidGenre = await _context.Genres.AnyAsync(g =>g.Id == input.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid genre ID!");

            using var dataStream = new MemoryStream();

            await input.Poster.CopyToAsync(dataStream);

            var movie = new Movie
            {
                Title = input.Title,
                Year = input.Year,
                Rate = input.Rate,
                StoreLine = input.StoreLine,
                GenreId = input.GenreId,
                Poster = dataStream.ToArray()
            };

            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();

            return Ok(movie);
        }
    }
}
