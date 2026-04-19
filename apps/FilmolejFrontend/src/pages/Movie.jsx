import VideoPlayer from "../components/Player";

function MoviePage() {
    const movieId = 1;

    return (
        <div>
            <h1>Watch Movie</h1>
            <VideoPlayer movieId={movieId}/>
        </div>
    );
}

export default MoviePage;