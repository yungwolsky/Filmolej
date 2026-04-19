import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Upload from "./pages/Upload";
import GetMovieCollection from "./pages/MovieCollection";
import MoviePage from "./pages/Movie";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/login" />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/upload" element={<Upload />} />
        <Route path="/movieCollection" element={<GetMovieCollection />}></Route>
        <Route path="/movie" element={<MoviePage />}></Route>
      </Routes>
    </BrowserRouter>
  ); 
}

export default App;