import { NavLink } from "react-router-dom";
import "../styles/Navbar.css";

function clearToken () {
    localStorage.clear("token");
}

function Navbar() {
    const token = localStorage.getItem("token");

    return (
        <div className="navbar">
            <h2>Filmolej</h2>

            <div className="nav-links">
                <NavLink to="/movieCollection">Collection</NavLink>
                <NavLink to="/upload">Upload</NavLink>
                {token ? (
                    <>
                        <NavLink to="/profile">Profile</NavLink>
                        <NavLink to="/login" onClick={clearToken}>Sign Out</NavLink>
                    </>
                ) : (
                    <NavLink to="/login">Login</NavLink>
                )}
                
            </div>
        </div>
    )
}

export default Navbar;