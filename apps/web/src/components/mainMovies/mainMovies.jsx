import React, {useEffect, useState} from "react";
import MovieBlock from "../movieBlock/movieBlock";

function MainMovies(){
    const [movies, setMovies] = useState([]);

    useEffect(() => {
        async function fetchData() {
            try {
                const response = await fetch("http://localhost:5000/api/movie/all", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json"
                    }
                });

                const result = await response.json();
                setMovies(result);
            } catch (err) {
                console.error(err);
            }
        }

        fetchData();
    }, []);

    return(
        <div>
            <h1>Movies:</h1>
            {movies.map((movie) => (
                <MovieBlock key={movie.movieId} movie={movie} />
            ))}
        </div>
    );
}

export default MainMovies;