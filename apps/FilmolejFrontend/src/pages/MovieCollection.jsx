import { useEffect, useState, useRef } from "react";
import { getMovieCollection } from "../api/movie";
import { useNavigate } from "react-router-dom";
import "../styles/MovieCollection.css";
import "../styles/Global.css";

function GetMovieCollection() {
    const [movies, setMovies] = useState([]);
    const [search, setSearch] = useState("");
    const [genres, setGenres] = useState([]);
    const [displayFilters, setDisplayFilters] = useState(false);
    const filterRef = useRef(null);
    const navigate = useNavigate();

    function handleSearch(e) {
        setSearch(e.target.value);
    }

    function handleDisplayFilters() {
        setDisplayFilters(!displayFilters);
    }

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const res = await getMovieCollection();
                setMovies(res.items);

                const allGenres = res.items
                    .flatMap(movie => movie.genre.split(","))
                    .map(g => g.trim());

                const uniqueGenres = [...new Set(allGenres)];
                setGenres(uniqueGenres);
            } catch (err) {
                console.error(err);
            }
        };

        fetchMovies();
    }, []);

    useEffect(() => {
        const handleClickOutside = (e) => {
            if (filterRef.current && !filterRef.current.contains(e.target)) {
                setDisplayFilters(false);
            }
        };

        if (displayFilters) {
            document.addEventListener("click", handleClickOutside);
        }

        return () => document.removeEventListener("click", handleClickOutside);
    }, [displayFilters]);

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
                <h2 className="collection-header">Your Movie Collection</h2>

                <input
                    type="text"
                    placeholder="Search"
                    className="search-input"
                    onChange={handleSearch}
                />

                <div 
                    className="filter-wrapper"
                    ref={filterRef}
                >
                    <button
                        onClick={handleDisplayFilters}
                        className="filter-button"
                    >
                        Filter
                    </button>

                    {displayFilters && (
                        <div
                            className="filters-container"
                        >
                            <div className="filters-labels">Genres:</div>
                            <div className="filters-checkboxes">
                                {genres.map((genre) => (
                                    <label key={genre} className="checkbox-label">
                                        <input type="checkbox" />
                                        {genre}
                                    </label>
                                ))}
                            </div>
                            <div className="filters-labels">Released (Year):</div>
                            <div>
                                <input
                                    type="text"
                                    placeholder="After"
                                    className="year-input"
                                />
                                -
                                <input
                                    type="text"
                                    placeholder="Befor"
                                    className="year-input"
                                />
                            </div>
                        </div>
                    )}
                </div>

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