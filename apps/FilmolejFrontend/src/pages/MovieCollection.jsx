import { useEffect, useState } from "react";
import { getMovieCollection } from "../api/movie";

function GetMovieCollection() {
    const [movies, setMovies] = useState([]);

    useEffect(() => {
        const fetchMovies = async () => {
            try{
                const res = await getMovieCollection();
                setMovies(res.items);
            } catch (err) {
                console.error(err);
            }
        };
        
        fetchMovies();
    }, []);

    return (
        <div>
            <h2>Your Movies</h2>

            {movies.length === 0 ? (
                <p>No movies found</p>
            ) : (
                movies.map((movie) => (
                    <div key={movie.id}>
                        <p>{movie.title}</p>
                    </div>
                ))
            )}
        </div>
    );
}

export default GetMovieCollection;