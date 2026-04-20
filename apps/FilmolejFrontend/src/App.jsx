import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Register";
import Upload from "./pages/Upload";
import GetMovieCollection from "./pages/MovieCollection";
import MoviePlayer from "./pages/MoviePlayer";
import Layout from "./components/Layout";
import GetMoviePage from "./pages/Movie";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/login" />} />
        
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        
        <Route element={<Layout />}>
          <Route path="/upload" element={<Upload />} />
          <Route path="/movie/:id" element={<GetMoviePage />}></Route>
          <Route path="/movieCollection" element={<GetMovieCollection />}></Route>
          <Route path="/moviePlayer/:id" element={<MoviePlayer />} />
        </Route>
      </Routes>
    </BrowserRouter>
  ); 
}

export default App;