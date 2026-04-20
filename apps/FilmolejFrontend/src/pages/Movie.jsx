import { useEffect, useState } from "react";
import { getMovie, getMovieCollection } from "../api/movie";
import { useNavigate, useParams } from "react-router-dom"; 
import "../styles/Global.css";

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

    return (
        <div className="page">
            <div>
                <h2>{movie.title}</h2>
                <img src={movie.posterUrl} alt={movie.title} />
            </div>
            <div>
                <p>{movie.plot}</p>
            </div>
        </div>
    );
}

export default GetMoviePage;