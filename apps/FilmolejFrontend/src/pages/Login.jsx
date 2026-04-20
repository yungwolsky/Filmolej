import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../api/auth";
import { Link } from "react-router-dom";
import "../styles/Login.css";
import "../styles/Global.css"

function Login(){
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();

        try{
            const data = await login(email, password);

            localStorage.setItem("token", data.token);

            console.log("Logged in:", data);
            navigate("/movieCollection");
        } catch (err) {
            console.log(err);
            
            const message =
                err.response?.data?.message ||
                err.response?.data ||
                "Login failed";

            console.log(message);
        }
    };

    return (
        <div className="page-center">
            <div className="loginBox">
                <h2>Log In</h2>
                {error && <p style={{ color: "red" }}>{error}</p>}
                
                <form onSubmit={handleLogin}>
                    <input 
                        type="email" 
                        placeholder="Email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />

                    <br />

                    <input 
                        type="password" 
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />

                    <br />

                    <Link className="link" to="/register">Doesn't have an account?</Link>
                    <button className="button" type="submit">Log In</button>
                </form>
            </div>
        </div>
    );
}

export default Login;