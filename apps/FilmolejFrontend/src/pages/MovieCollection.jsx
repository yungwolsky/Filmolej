import { useEffect, useState } from "react";
import { getMovieCollection } from "../api/movie";
import { useNavigate } from "react-router-dom";
import "../styles/Collection.css";
import "../styles/Global.css";

function GetMovieCollection() {
    const [movies, setMovies] = useState([]);
    const [search, setSearch] = useState("");
    const navigate = useNavigate();

    function handleSearch(e) {
        setSearch(e.target.value);
    }

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const res = await getMovieCollection();
                setMovies(res.items);
            } catch (err) {
                console.error(err);
            }
        };

        fetchMovies();
    }, []);

    const moviesToDisplay = movies.filter(movie => {
        return movie.title.toLowerCase()
            .startsWith(search
                .trim()
                .toLowerCase());
    });

    const userMovies = moviesToDisplay.map(movie => (
        <div
            className="movie-card"
            key={movie.id}
            onClick={() => navigate(`/movie/${movie.id}`)}>
            <div className="poster">
                <img src={movie.posterUrl} alt={movie.title}></img>
            </div>
            <p>{movie.title}</p>
        </div>
    ));

    return (
        <>
            <title>Collection</title>
            <div className="page">
                <h2>Your Movie Collection</h2>

                <input
                    type="text"
                    placeholder="Search"
                    onChange={handleSearch}
                />
                <button>Filter</button>

                {movies.length === 0 ? (
                    <p>No movies found</p>
                ) : (
                    <div className="movie-grid">
                        {userMovies}
                    </div>
                )}
            </div>
        </>
    );
}

export default GetMovieCollection;