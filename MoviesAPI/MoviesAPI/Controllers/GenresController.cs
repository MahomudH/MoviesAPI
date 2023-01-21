using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Services.Genres;


namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genreService.GetAll();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto input)
        {
            var genre = new Genre { Name = input.Name };

            await _genreService.Add(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody]GenreDto input)
        {
            var genre = await _genreService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was found with id: {id}");

            genre.Name = input.Name;
            _genreService.Update(genre);

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genreService.GetById(id);

            if (genre == null)
                return NotFound($"No genre was found with id: {id}");

            _genreService.Delete(genre);

            return Ok(genre);
        }
    }
}
