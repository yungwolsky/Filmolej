import { useEffect, useState } from "react";
import { getMovie } from "../api/movie";
import { useNavigate, useParams } from "react-router-dom"; 
import "../styles/Global.css";
import "../styles/Movie.css";

function GetMoviePage() {
    const [movie, setMovie] = useState();
    const navigate = useNavigate();
    const { id } = useParams();

    useEffect(() => {
        const fetchMovie = async () => {
            try {
                const res = await getMovie(id);
                console.log(res.items);
                setMovie(res.items);
            } catch (err) {
                console.error(err);
            }
        }

        fetchMovie();
    }, [id]);

    if (!movie) return <div className="page">Loading...</div>;

    return (
        <div className="page">
            <div className="movie-container">
                <div className="poster">
                    <img src={movie.posterUrl} alt={movie.title} />
                </div>

                <div className="information">
                    <h2 className="movie-title">{movie.title}</h2>
                    <div className="movie-info">
                        <span>Director: {movie.director}</span>
                        <span>Year: {movie.releaseDate}</span>
                        <span>Genre: {movie.genre}</span>
                        <span>Duration: {movie.duration}</span>
                    </div>

                    <div className="plot">
                        <p>{movie.plot}</p>
                    </div>
                    <div>
                    <button onClick={() => navigate(`/moviePlayer/${movie.id}`)}>Watch Now</button>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default GetMoviePage;