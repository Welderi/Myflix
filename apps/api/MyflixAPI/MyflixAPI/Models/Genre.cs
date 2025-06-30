namespace MyflixAPI.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public virtual ICollection<Movie> MgMovieIdRefs { get; set; } = new List<Movie>();
}
