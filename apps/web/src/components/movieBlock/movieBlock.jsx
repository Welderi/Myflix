import React from "react";

function MovieBlock({movie}) {
    return (
        <div>
            <p>{movie.movieTitle}</p>
            <p>{movie.movieOverview}</p>
            <img
                src={`https://image.tmdb.org/t/p/w500${movie.moviePosterPath}`}
                alt={movie.movieTitle}
            />
        </div>
    );
}

export default MovieBlock;
