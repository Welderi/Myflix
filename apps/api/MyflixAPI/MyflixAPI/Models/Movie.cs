namespace MyflixAPI.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string MovieTitle { get; set; } = null!;

    public string? MovieOverview { get; set; }

    public DateOnly? MovieReleaseDate { get; set; }

    public string? MoviePosterPath { get; set; }

    public string? MovieBackdropPath { get; set; }

    public double? MoviePopularity { get; set; }

    public double? MovieVoteAverage { get; set; }

    public int? MovieVoteCount { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    //public virtual ICollection<MovieTranslation> MovieTranslations { get; set; } = new List<MovieTranslation>();

    public virtual ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();

    public virtual ICollection<Actor> MaActorIdRefs { get; set; } = new List<Actor>();

    public virtual ICollection<Genre> MgGenreIdRefs { get; set; } = new List<Genre>();
}
