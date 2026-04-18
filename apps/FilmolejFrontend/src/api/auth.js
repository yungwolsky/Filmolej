import apiClient from "./client";

export const login = async (email, password) => {
    const res = await apiClient.post(`/user/login`, {
        email,
        password
    });

    return res.data;
}

export const register = async (username, email, password) => {
    const res = await apiClient.post(`/user/register`, {
        username,
        email,
        password
    });

    return res.data;
}