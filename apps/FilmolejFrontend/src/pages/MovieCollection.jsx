import { useEffect, useState } from "react";
import { getMovieCollection } from "../api/movie";
import { useNavigate } from "react-router-dom";
import "../styles/Collection.css";
import "../styles/Global.css";

function GetMovieCollection() {
    const [movies, setMovies] = useState([]);
    const navigate = useNavigate();

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
        <div className="page">
            <h2>Your Movies</h2>

            {movies.length === 0 ? (
                <p>No movies found</p>
            ) : (
                <div className="movie-grid">
                    {movies.map((movie) => (
                        <div 
                            className="movie-card" 
                            key={movie.id}
                            onClick={() => navigate(`/movie/${movie.id}`)}>
                            <div className="poster">
                                <img src={movie.posterUrl} alt={movie.title}></img>
                            </div>
                            <p>{movie.title}</p>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}

export default GetMovieCollection;