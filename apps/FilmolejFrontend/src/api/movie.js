import apiClient from "./client";

const CHUNK_SIZE = 5 * 1024 * 1024; // 5 MB

export const upload = async (title, file) => {
    const formData = new FormData();

    formData.append("file", file);
    formData.append("title", title);

    const res = await apiClient.post("/movie/upload", formData)

    return res.data;
}

export const uploadInChunks = async (title, file) => {
    const totalChunks = Math.ceil(file.size / CHUNK_SIZE);
    const uploadId = crypto.randomUUID();

    for (let chunkIndex = 0; chunkIndex < totalChunks; chunkIndex++)
    {
        const start = chunkIndex * CHUNK_SIZE;
        const end = Math.min(start + CHUNK_SIZE, file.size);

        const chunk = file.slice(start, end);

        const formData = new FormData();
        formData.append("file", chunk);
        formData.append("title", title);
        formData.append("uploadId", uploadId);
        formData.append("chunkIndex", chunkIndex);
        formData.append("totalChunks", totalChunks);
        formData.append("fileName", file.name);

        await apiClient.post("movie/upload-chunk", formData);
    }

    const res = await apiClient.post("/movie/complete-upload", {
        uploadId,
        fileName: file.name,
        title
    });

    return res.data;
}

export const getMovieCollection = async () => {
    const res = await apiClient.get("/movie/users-movies");
    return res.data;
}

export const getMovie = async (movieId) => {
    const res = await apiClient.get(`/movie/get-movie?movieId=${movieId}`)
    return res.data;
}