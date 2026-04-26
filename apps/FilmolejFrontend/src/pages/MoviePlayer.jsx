import { useParams } from "react-router-dom";
import VideoPlayer from "../components/Player";

function MoviePage() {
    const { id } = useParams();

    return (
        <>
            <title>Player</title>
            <div>
                <VideoPlayer movieId={id}/>
            </div>
        </>
    );
}

export default MoviePage;