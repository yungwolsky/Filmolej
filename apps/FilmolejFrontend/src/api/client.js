import axios from "axios";

const apiClient = axios.create({
   baseURL: "http://192.168.0.7:5000/api",
});

apiClient.interceptors.request.use((config) => {
   const token = localStorage.getItem("token");

   if (token) {
      config.headers.Authorization = `Bearer ${token}`;
   }
   
   return config;
});

export default apiClient;