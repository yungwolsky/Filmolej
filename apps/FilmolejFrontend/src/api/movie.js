import apiClient from "./client";

export const upload = async (title, file) => {
    const formData = new FormData();

    formData.append("file", file);
    formData.append("title", title);

    const res = await apiClient.post("/movie/upload", formData)

    return res.data;
}