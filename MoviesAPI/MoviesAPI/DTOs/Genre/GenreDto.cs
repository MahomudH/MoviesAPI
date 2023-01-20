namespace MoviesAPI.DTOs.Genre
{
    public class GenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
