import { NavLink } from "react-router-dom";
import "../styles/Navbar.css";

function Navbar() {
    return (
        <div className="navbar">
            <h2>Filmolej</h2>

            <div className="nav-links">
                <NavLink to="/movieCollection">Collection</NavLink>
                <NavLink to="/upload">Upload</NavLink>
                <NavLink to="/login">Login</NavLink>
            </div>
        </div>
    )
}

export default Navbar;