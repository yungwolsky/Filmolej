import { useState } from "react";
import { register } from "../api/auth";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import "../styles/Login.css";
import "../styles/Global.css";

function Register(){
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();

    const handleRegister = async (e) => {
        e.preventDefault();

        try {
            const data = await register(username, email, password);

            localStorage.setItem("token", data.token);
            
            navigate("/movieCollection");
        } catch (err) {
            const message =
                err.response?.data?.message ||
                err.response?.data ||
                "Registration failed";

            setError(message);
        }
    }

    return (
        <>
            <title>Register</title>
            <div className="page-center">
                <div className="loginBox">
                    <h2>Register</h2>
                    {error && <p style={{ color: "red" }}>{error}</p>}

                    <form onSubmit={handleRegister}>
                        <input 
                            type="username" 
                            placeholder="Username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                        />

                        <br />

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
                
                        <Link className="link" to="/login">Already have an account?</Link>
                        <button className="button" type="submit">Register</button>
                    </form>
                </div>
            </div>
        </>
    )
}

export default Register;