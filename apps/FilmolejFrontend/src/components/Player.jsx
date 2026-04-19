import Hls from "hls.js";
import { useEffect, useRef } from "react";

function VideoPlayer({ movieId }) {
    const VideoRef = useRef(null);

    useEffect(() => {
        const video = VideoRef.current;
        const url = `http://localhost:5000/storage/streams/${movieId}/stream.m3u8`;
    
        let hls;

        if(Hls.isSupported()) {
            hls = new Hls();
            hls.loadSource(url);
            hls.attachMedia(video);
        } else if (video?.canPlayType("application/vnd.apple.mpegurl")) {
            video.src = url;
        }

        return () => {
            if (hls) {
                hls.destroy();
            }
        };
    }, [movieId]);

    return <video ref={VideoRef} controls width="800" />;
}

export default VideoPlayer;