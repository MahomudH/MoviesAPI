namespace MoviesAPI.Services.Genres
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(byte id);
        Task<Genre> Add(Genre genre);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);
    }
}
