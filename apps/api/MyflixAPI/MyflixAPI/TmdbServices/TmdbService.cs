using Microsoft.EntityFrameworkCore;
using MyflixAPI.Models;
using System.Text.Json;
using TMDbLib.Client;

namespace MyflixAPI.TmdbServices
{
    public class TmdbService
    {
        private readonly TMDbClient _service;
        private readonly MyflixContext _context;

        public TmdbService(TMDbClient service, MyflixContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task SyncMovieAsync()
        {
            var popularMovies = await _service.GetMoviePopularListAsync();

            foreach (var tm in popularMovies.Results)
            {
                var exists = await _context.Movies.AnyAsync(a => a.MovieTitle == tm.OriginalTitle);
                if (exists)
                    continue;

                var movie = new Movie
                {
                    MovieTitle = tm.Title,
                    MovieOverview = tm.Overview,
                    MovieReleaseDate = tm.ReleaseDate.HasValue
                                            ? DateOnly.FromDateTime(tm.ReleaseDate.Value)
                                            : null,
                    MoviePosterPath = tm.PosterPath,
                    MovieBackdropPath = tm.BackdropPath,
                    MoviePopularity = tm.Popularity,
                    MovieVoteAverage = tm.VoteAverage,
                    MovieVoteCount = tm.VoteCount
                };

                _context.Movies.Add(movie);

                foreach (var genreId in tm.GenreIds)
                {
                    var genre = await _context.Genres.FindAsync(genreId);
                    if (genre != null)
                    {
                        movie.MgGenreIdRefs.Add(genre);
                    }
                }

            }
            await _context.SaveChangesAsync();
        }

        public async Task SyncActorAsync()
        {
            var popularActors = await _service.GetPersonPopularListAsync();

            foreach (var ac in popularActors.Results)
            {
                var exists = await _context.Actors.AnyAsync(a => a.ActorName == ac.Name);
                if (exists)
                    continue;

                var actor = new Actor
                {
                    ActorName = ac.Name,
                    ActorBio = ac.KnownFor?.ToString(),
                    ActorProfilePath = JsonSerializer.Serialize(ac.KnownFor),
                    ActorWiki = "https://en.wikipedia.org/wiki/" + Uri.EscapeDataString(ac.Name.Replace(' ', '_'))
                };
                _context.Actors.Add(actor);
            }
            await _context.SaveChangesAsync();
        }
        public async Task SyncGenresAsync()
        {
            var tmdbGenres = await _service.GetMovieGenresAsync();

            foreach (var g in tmdbGenres)
            {
                var exists = await _context.Genres.AnyAsync(x => x.GenreName == g.Name);
                if (!exists)
                {
                    var genre = new Genre
                    {
                        GenreName = g.Name
                    };
                    _context.Genres.Add(genre);
                }
            }

            await _context.SaveChangesAsync();
        }

    }
}

