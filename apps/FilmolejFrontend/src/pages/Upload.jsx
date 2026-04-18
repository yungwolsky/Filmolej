import { useState } from "react";
import { upload } from "../api/movie";

function Upload(){
    const [title, setTitle] = useState("");
    const [file, setFile] = useState(null);

    const handleUpload = async () => {
        try {
            const res = await upload(title, file);
            console.log("Uploaded:", res);
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <div>
            <input 
                type="text" 
                placeholder="Title"
                onChange={(e) => setTitle(e.target.value)}
            />

            <input 
                type="file" 
                onChange={(e) => setFile(e.target.files[0])}
            />

            <button onClick={handleUpload}>
                Upload
            </button>
        </div>
    )
}

export default Upload;