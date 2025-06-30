using MyflixAPI.Models;
using MyflixAPI.Services;

namespace MyflixAPI.TmdbServices
{
    public class MAGTranslationService
    {
        private string langTo = "uk";

        private readonly AzureTranslatorService _translator;
        private readonly MovieService _movie;
        private readonly ActorService _actor;
        private readonly GenreService _genre;
        private readonly MovieTranslationService _movieTranslationService;
        private readonly ActorTranslationService _actorTranslationService;
        private readonly GenreTranslationService _genreTranslationService;

        public MAGTranslationService(AzureTranslatorService translator,
            MovieService movie, ActorService actor, GenreService genre,
            MovieTranslationService movieTranslationService, ActorTranslationService actorTranslationService, GenreTranslationService genreTranslationService)
        {
            _translator = translator;
            _movie = movie;
            _actor = actor;
            _genre = genre;
            _movieTranslationService = movieTranslationService;
            _actorTranslationService = actorTranslationService;
            _genreTranslationService = genreTranslationService;
        }

        private async Task<string?> TranslateTextAsync(string text)
        {
            return await _translator.TranslateTextAsync(text);
        }

        public async Task MovieTranslation()
        {
            var movies = await _movie.GetAllMoviesAsync();

            foreach (var movie in movies)
            {
                if (string.IsNullOrEmpty(movie.MovieTitle)) continue;

                string? translatedTitle = await TranslateTextAsync(movie.MovieTitle);
                string? translatedOverview = string.IsNullOrEmpty(movie.MovieOverview)
                    ? null
                    : await TranslateTextAsync(movie.MovieOverview);

                var movieFromDb = await _movie.SearchMovieByNameAsync(movie.MovieTitle);

                if (movieFromDb == null || string.IsNullOrEmpty(translatedTitle)) continue;

                var existingTranslation = await _movieTranslationService.SearchMovieTranslationByNameAsync(translatedTitle, langTo);
                if (existingTranslation != null) continue;

                var movieTranslation = new MovieTranslation
                {
                    MtTitle = translatedTitle,
                    MtOverview = translatedOverview,
                    MtLanguage = langTo,
                    MtMovieIdRef = movieFromDb.FirstOrDefault()!.MovieId
                };

                await _movieTranslationService.AddMovieTranslation(movieTranslation);
            }
        }

        public async Task ActorTranslation()
        {
            var actors = await _actor.GetAllActorsAsync();

            foreach (var actor in actors)
            {
                if (string.IsNullOrEmpty(actor.ActorName)) continue;

                string? translatedName = await TranslateTextAsync(actor.ActorName);
                string? translatedBio = string.IsNullOrEmpty(actor.ActorBio)
                    ? null
                    : await TranslateTextAsync(actor.ActorBio);

                var actorFromDb = await _actor.SearchActorByNameAsync(actor.ActorName);

                if (actorFromDb == null || string.IsNullOrEmpty(translatedName)) continue;

                var existingTranslation = await _actorTranslationService.SearchActorTranslationByNameAsync(translatedName, langTo);
                if (existingTranslation != null) continue;

                var actorTranslation = new ActorTranslation
                {
                    AtName = translatedName,
                    AtBio = translatedBio,
                    AtLanguage = langTo,
                    AtWiki = "https://en.wikipedia.org/wiki/" + Uri.EscapeDataString(translatedName.Replace(' ', '_')),
                    AtActorIdRef = actorFromDb.FirstOrDefault()!.ActorId
                };

                await _actorTranslationService.AddActorTranslation(actorTranslation);
            }
        }

        public async Task GenreTranslation()
        {
            var genres = await _genre.GetAllGenresAsync();

            foreach (var genre in genres)
            {
                if (string.IsNullOrEmpty(genre.GenreName)) continue;

                string? translatedName = await TranslateTextAsync(genre.GenreName);

                var genreFromDb = await _genre.SearchGenreByNameAsync(genre.GenreName);

                if (genreFromDb == null || string.IsNullOrEmpty(translatedName)) continue;

                var existingTranslation = await _genreTranslationService.SearchGenreTranslationByNameAsync(translatedName);
                if (existingTranslation != null) continue;

                var genreTranslation = new GenreTranslation
                {
                    GtName = translatedName,
                    GtGenreIdRef = genreFromDb.FirstOrDefault()!.GenreId
                };

                await _genreTranslationService.AddGenreTranslation(genreTranslation);
            }
        }
    }
}
