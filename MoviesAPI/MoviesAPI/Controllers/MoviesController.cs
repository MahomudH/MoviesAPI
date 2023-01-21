using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Services.Genres;
using MoviesAPI.Services.Movies;


namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenreService _genreService;

        private List<string> _allowedExtentions = new List<string> { ".png", ".jpg", ".jfif" };
        private long _maxAllowedPosterSize = 1048576;

        public MoviesController(
            IMoviesService moviesService,
            IGenreService genreService
            )
        {
            _moviesService = moviesService;
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _moviesService.GetAll();
            //todo map movies to dto

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
                return NotFound();

            var dto = new MovieDetailsDto()
            {
                Id = movie.Id,
                Title = movie.Title,
                Rate = movie.Rate,
                Poster = movie.Poster,
                GenreId = movie.GenreId,
                StoreLine = movie.StoreLine,
                Year = movie.Year,
                GenreName = movie.Genre.Name
            };

            return Ok(dto);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var movies = await _moviesService.GetAll(genreId);
            //todo map movies to dto

            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto input)
        {
            if (!_allowedExtentions.Contains(Path.GetExtension(input.Poster.FileName).ToLower()))
                return BadRequest("Only .png, .jpg and .jfif images are allowed!");

            if (input.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            var isValidGenre = await _genreService.IsValidGenre(input.GenreId);
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

            await _moviesService.Add(movie);

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateMovieDto input)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
                return NotFound($"No movie was found with id: {id}");

            var isValidGenre = await _genreService.IsValidGenre(input.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid genre ID!");

            if (input.Poster != null)
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(input.Poster.FileName).ToLower()))
                    return BadRequest("Only .png, .jpg and .jfif images are allowed!");

                if (input.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");

                using var dataStream = new MemoryStream();

                await input.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

            movie.Title = input.Title;
            movie.Year = input.Year;
            movie.Rate = input.Rate;
            movie.StoreLine = input.StoreLine;
            movie.GenreId = input.GenreId;

            _moviesService.Update(movie);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
                return NotFound($"No movie was found with id: {id}");

            _moviesService.Delete(movie);

            return Ok(movie);
        }
    }
}
