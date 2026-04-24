import { useState } from "react";
import { upload, uploadInChunks } from "../api/movie";
import "../styles/Global.css"
import "../styles/Upload.css"

function Upload(){
    const [title, setTitle] = useState("");
    const [file, setFile] = useState(null);

    const handleUpload = async () => {
        try {
            const res = await uploadInChunks(title, file);
            console.log("Uploaded:", res);
        } catch (err) {
            console.error(err);
        }
    };

    return (
        <div className="page-center">
            <div className="formBox">
                <h2>Upload a movie</h2>
                <input 
                    type="text" 
                    placeholder="Title"
                    onChange={(e) => setTitle(e.target.value)}
                />

                <input 
                    type="file" 
                    onChange={(e) => setFile(e.target.files[0])}
                />

                <div className="button-container">
                    <button className="button" onClick={handleUpload}>
                        Upload
                    </button>
                </div>
            </div>
        </div>
    )
}

export default Upload;