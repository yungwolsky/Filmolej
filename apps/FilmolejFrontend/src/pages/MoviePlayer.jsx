import { useParams } from "react-router-dom";
import VideoPlayer from "../components/Player";

function MoviePage() {
    const { id } = useParams();

    return (
        <div>
            <h1>Watch Movie</h1>
            <VideoPlayer movieId={id}/>
        </div>
    );
}

export default MoviePage;