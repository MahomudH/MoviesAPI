using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Services.Genres
{
    public class GenreService: IGenreService
    {
        private readonly ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<Genre> GetById(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return genre;
        }

        public Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();

            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();

            return genre;
        }
    }
}
