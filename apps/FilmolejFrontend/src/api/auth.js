import axios from "axios";

const API = "http://192.168.0.7:5000/api/user";

export const login = async (email, password) => {
    const res = await axios.post(`${API}/login`, {
        email,
        passowrd
    });

    return res.data;
}

export const register = async (username, email, password) => {
    const res = await axios.post(`${API}/register`, {
        username,
        email,
        password
    });
}